using System;
using System.Windows.Forms;
using System.Drawing;

namespace PythonControl {
    public enum ParamIDs { 
        SetChannels = 0,
        SetTTopLeds = 1, SetTBottomLeds = 2,
        SetLedsTop = 3, SetLedsBottom = 4,
        SetFreq = 5,
        EnableLeds = 6, EnableLRs = 7,
        LRPoint1T = 8, LRPoint1Pwr = 9, LRPoint2T = 10, LRPoint2Pwr = 11,
    };

    public partial class MainForm : Form {
        private Periphery_t Periph;
        private int TTop = 45, TBottom = 18;
        private int PwrBottom = -2000, PwrTop = 2000;
        private int FreqTop = 36, FreqBottom = 1;
        private byte ChnlMsk = 0xF7;
        public CmdQ_t CmdQ;

        #region ============================= Init / deinit ============================
        public MainForm() {
            InitializeComponent();
            Periph = new Periphery_t() {
                ComPort = serialPort1
            };
            CmdQ = new CmdQ_t();

            chart1.ChartAreas[0].AxisY.Minimum = TBottom;
            chart1.ChartAreas[0].AxisY.Maximum = TTop;
            txtbTTopLeds.Text = TTop.ToString();
            txtbTBottomLeds.Text = TBottom.ToString();

            cbCh8Leds.Checked = (ChnlMsk & (1 << 0)) != 0;
            cbCh7Leds.Checked = (ChnlMsk & (1 << 1)) != 0;
            cbCh6Leds.Checked = (ChnlMsk & (1 << 2)) != 0;
            cbCh5Leds.Checked = (ChnlMsk & (1 << 3)) != 0;
            cbCh4Leds.Checked = (ChnlMsk & (1 << 4)) != 0;
            cbCh3Leds.Checked = (ChnlMsk & (1 << 5)) != 0;
            cbCh2Leds.Checked = (ChnlMsk & (1 << 6)) != 0;
            cbCh1Leds.Checked = (ChnlMsk & (1 << 7)) != 0;

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            timerHeartBeat.Enabled = false;
            try {
                Periph.ComPort.Close();
                Periph.ComPort = null;
                serialPort1.Dispose();
            }
            catch(System.Exception ex) {
                Periph.LastError = ex.Message;
            }
            Periph.TransmitterConnected = false;
        }
        #endregion

        private bool CheckTextBox(TextBox Txtb, int MinValue, int MaxValue, out int Value) {
            if(int.TryParse(Txtb.Text, out Value)) {
                if(Value >= MinValue && Value <= MaxValue) {
                    Txtb.BackColor = SystemColors.Window;
                    return true;
                }
            }
            Txtb.BackColor = Color.Red;
            return false;
        }

        private void TxtbChecker(object sender, EventArgs e) {
            TextBox Txtb = sender as TextBox;
            if(int.TryParse(Txtb.Text, out int Value)) {
                Txtb.BackColor = SystemColors.Window;
            }
            else Txtb.BackColor = Color.Red;
        }

        #region ============================= Events ============================
        private void ProcessState() {
            if(Periph.TransmitterConnected) {
                StatusLabel.Text = "Передатчик подключен";
                lblPythonReplying.Visible = true;  // Transmitter connected, but no reply
                if(Periph.PythonOnline) {
                    lblPythonReplying.Text = "Питон на связи";
                    timerHeartBeat.Interval = 100;
                }
                else {
                    lblPythonReplying.Text = "Питон не отвечает";
                    timerHeartBeat.Interval = 1000;
                }
            }
            else {  // Transmitter disconnected
                lblPythonReplying.Visible = false;
                StatusLabel.Text = "Передатчик не подключен";
                timerHeartBeat.Interval = 1000;
            }
        }

        private void TimerHeartBeat_Tick(object sender, EventArgs e) {
            // Check if device connected
            if(Periph.TransmitterConnected) {
                // Send commands until q is empty
                while(CmdQ.Get(out string Cmd)) {
                    if(Periph.SendCmd(Cmd) == false) goto EndOfTick; // Some failure
                }
                // Q is empty, Request info
                if(Periph.GetInfo()) {
                    // Show reply
                    for(int i=0; i<8; i++) {
                        int src = Periph.LastT[i + 1];
                        chart1.Series[0].Points[i].YValues[0] = (float)src / 10;
                    }
                    chart1.Refresh();
                }
            } //if connected
            // Not connected, try to connect
            else { 
                try { serialPort1.Close(); }
                catch { }
                // First, get port names
                string[] SPorts = System.IO.Ports.SerialPort.GetPortNames();
                int NPorts = SPorts.Length;
                if(NPorts != 0) {
                    // Iterate all discovered ports
                    for(int i = 0; i < NPorts; i++) {
                        serialPort1.PortName = SPorts[i];
                        try {
                            serialPort1.Open();
                            // Try to ping device
                            if(Periph.PingTransmitter()) goto EndOfTick; // Device found
                            else serialPort1.Close(); // Try next port
                        } // try
                        catch { serialPort1.Close(); }
                    } //for
                    // Silence answers our cries
                    //labelHelp.Text = "Ни на одном из имеющихся в системе последовательных портов стенд не обнаружен.";
                    //StatusLabel.Image = imageList1.Images[1];
                } // if(NPorts != 0) 
                else { 
                    //labelHelp.Text = "В системе нет ни одного последовательного порта. Если стенд подключен, то, вероятно, не установлены драйверы USB-Serial.";
                }
            } // not connected
            EndOfTick:
            ProcessState();
        } //timerHeartBeat_Tick

        // ==== Checkboxes ====
        private byte Checkbox2Channel(CheckBox cb) {
            string S = cb.Tag.ToString();
            byte.TryParse(S, out byte Channel);
            return Channel;
        }

        string CmdSetParam(ParamIDs ParamID, int Value) {
            return "SetParam " + ((byte)ParamID).ToString() + ' ' + Value.ToString() + "\r\n";
        }

        // ==== Channel selection ====
        private void CbChLeds_CheckedChanged(object sender, EventArgs e) {
            byte Channel = Checkbox2Channel((CheckBox)sender);
            bool IsOn = ((CheckBox)sender).Checked;
            // Change state
            byte Msk = (byte)(1 << (8 - Channel));
            if(IsOn) ChnlMsk |= Msk;
            else ChnlMsk &= (byte)~Msk;
            // Send command
            if(Periph.TransmitterConnected && Periph.PythonOnline) {
                CmdQ.Put(CmdSetParam(ParamIDs.SetChannels, ChnlMsk));
            }
            // Change color
            if(IsOn) chart1.Series[0].Points[Channel - 1].Color = SystemColors.Highlight;
            else chart1.Series[0].Points[Channel - 1].Color = SystemColors.InactiveCaption;
        }

        #region ==== Enable/Disable ====
        private void ChbEnableLeds_CheckedChanged(object sender, EventArgs e) {
            if(Periph.TransmitterConnected && Periph.PythonOnline) {
                CmdQ.Put(CmdSetParam(ParamIDs.EnableLeds, chbEnableLeds.Checked? 1 : 0));
            }
        }

        private void ChbEnableLR_CheckedChanged(object sender, EventArgs e) {
            if(Periph.TransmitterConnected && Periph.PythonOnline) {
                CmdQ.Put(CmdSetParam(ParamIDs.EnableLRs, chbEnableLR.Checked ? 1 : 0));
            }
        }
        #endregion

        #region ==== Frequency ====
        private void BtnFreqLeds_Click(object sender, EventArgs e) {
            if(Int32.TryParse(TxtbFreqLeds.Text, out int NewF)) {
                if(NewF > 0 && NewF <= 255) {
                    // Send command
                    if(Periph.TransmitterConnected && Periph.PythonOnline) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetFreq, NewF));
                    }
                }
            }
        }

        private void TxtbFreqLeds_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '\r') BtnFreqLeds_Click(null, null);
        }
        #endregion

        #region ==== Top/Bottom temperature selection ====
        private void BtnTTopLeds_Click(object sender, EventArgs e) {
            if(Int32.TryParse(txtbTTopLeds.Text, out int NewT)) {
                if(NewT > TBottom) {
                    // Send command
                    if(Periph.TransmitterConnected && Periph.PythonOnline) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetTTopLeds, NewT));
                    }
                    // Change chart
                    chart1.ChartAreas[0].AxisY.Maximum = NewT;
                    TTop = NewT;
                }
            }
        }

        private void BtnTBottomLeds_Click(object sender, EventArgs e) {
            if(Int32.TryParse(txtbTBottomLeds.Text, out int NewT)) {
                if(NewT < TTop) {
                    // Send command
                    if(Periph.TransmitterConnected && Periph.PythonOnline) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetTBottomLeds, NewT));
                    }
                    // Change chart
                    chart1.ChartAreas[0].AxisY.Minimum = NewT;
                    TBottom = NewT;
                }
            }
        }

        private void TxtbTTopLeds_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '\r') BtnTTopLeds_Click(null, null);
        }

        private void TxtbTBottomLeds_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '\r') BtnTBottomLeds_Click(null, null);
        }
        #endregion

        #region ==== Top/Bottom brightness selection ====
        private void BtnTopBlinkFreq_Click(object sender, EventArgs e) {
            if(Int32.TryParse(txtbTopLeds.Text, out int NewF)) {
                if(NewF > FreqBottom) {
                    // Send command
                    if(Periph.TransmitterConnected && Periph.PythonOnline) {
                        CmdQ.Put(CmdSetParam(ParamIDs.EnableLeds, NewF));
                    }
                    // Change variable
                    FreqTop = NewF;
                }
            }
        }

        private void BtnBottomBlinkFreq_Click(object sender, EventArgs e) {
            if(Int32.TryParse(txtbBottomLeds.Text, out int NewF)) {
                if(NewF < FreqTop) {
                    // Send command
                    if(Periph.TransmitterConnected && Periph.PythonOnline) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetLedsBottom, NewF));
                    }
                    // Change variable
                    FreqBottom = NewF;
                }
            }
        }

        private void TxtbTopBlinkFreq_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '\r') BtnTopBlinkFreq_Click(null, null);
        }
        
        private void TxtbBottomBlinkFreq_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '\r') BtnBottomBlinkFreq_Click(null, null);
        }
        #endregion

        #region ======= Laucaringi  =======
        private void BtnLRPoint1_Click(object sender, EventArgs e) {
            if(CheckTextBox(TxtbLRTPoint1, TBottom, TTop, out int T) && CheckTextBox(TxtbLRPwrPoint1, PwrBottom, PwrTop, out int Pwr)) {
                if(Periph.TransmitterConnected && Periph.PythonOnline) {
                    CmdQ.Put(CmdSetParam(ParamIDs.LRPoint1T, T));
                    CmdQ.Put(CmdSetParam(ParamIDs.LRPoint1Pwr, Pwr));
                }
            }
        }

        private void BtnLRPoint2_Click(object sender, EventArgs e) {
            if(CheckTextBox(TxtbLRTPoint2, TBottom, TTop, out int T) && CheckTextBox(TxtbLRPwrPoint2, PwrBottom, PwrTop, out int Pwr)) {
                if(Periph.TransmitterConnected && Periph.PythonOnline) {
                    CmdQ.Put(CmdSetParam(ParamIDs.LRPoint2T, T));
                    CmdQ.Put(CmdSetParam(ParamIDs.LRPoint2Pwr, Pwr));
                }
            }
        }
        #endregion

        #endregion
    } // Mainform


    #region ========================= Classes =========================
    public class Periphery_t {
        // ==== Private ====
        bool SendAndGetReply(string SCmd, out string SReply) {
            SReply = "";
            if(!ComPort.IsOpen) return false;
            // Send string
            try {
                ComPort.WriteLine(SCmd);
                SReply = ComPort.ReadLine().Trim();
                return true;
            }
            catch(Exception ex) {
                TransmitterConnected = false;
                LastError = ex.Message;
                try {
                    ComPort.Close();
                }
                catch(Exception ex2) {
                    LastError = ex2.Message;
                }
            }
            return false;
        }

        // ==== Public ====
        public bool TransmitterConnected = false, PythonOnline = false;
        public System.IO.Ports.SerialPort ComPort;
        public string LastError = "";

        // ==== Commands ====
        public bool PingTransmitter() {
            if(SendAndGetReply("Ping", out string SReply)) {
                TransmitterConnected = SReply.Equals("Ack 0", StringComparison.OrdinalIgnoreCase);
                return TransmitterConnected;
            }
            else return false;
        }

        public bool SendCmd(string SCmd) {
            if(SendAndGetReply(SCmd, out string SReply)) {
                PythonOnline = SReply.Equals("Ack 0", StringComparison.OrdinalIgnoreCase);
                return PythonOnline;
            }
            else return false;
        }

        public int[] LastT = new int[9];
        public bool GetInfo() {
            if(SendAndGetReply("GetInfo", out string SReply)) {
                string[] Tokens = SReply.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                // Will receive either Info, or not Info. In last case, python is not online.
                PythonOnline = (Tokens[0].Equals("Info", StringComparison.OrdinalIgnoreCase) && Tokens.Length == 9);    // "Info" and 8 numbers
                if(PythonOnline) {
                    for(int i = 1; i <= 8; i++) {
                        if(!int.TryParse(Tokens[i], out LastT[i])) {
                            PythonOnline = false;
                            return false;    // parse failed
                        }
                    } // for
                    return true;    // everything parsed properly
                } // if online
            }
            return false;
        }
    } // CrystalDevice_t

    public class CmdQ_t {
        public const int QSize = 18;
        private string[] Q = new string[QSize];
        private int Indx = 0;

        public int Sz { get { return Indx; } }
        public bool Put(string SCmd) {
            if(Indx < QSize - 1) {
                Q[Indx] = SCmd;
                Indx++;
                return true;
            }
            else return false;
        }

        public bool Get(out string SCmd) {
            if(Indx == 0) { // Empty Q
                SCmd = "";
                return false; 
            }
            else {
                Indx--;
                SCmd = Q[Indx];
                return true;
            }
        }
    }
    #endregion
}