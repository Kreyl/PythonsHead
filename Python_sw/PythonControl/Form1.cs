using System;
using System.Windows.Forms;
using System.Drawing;

namespace PythonControl {
    public enum ParamIDs { 
        SetChannels = 0,
        SetTTop = 1, SetTBottom = 2,
        SetBlinkFTop = 3, SetBlinkFBottom = 4,
    };

    public partial class MainForm : Form {
        private Transmitter_t Transmitter;
        private int TTop = 45, TBottom = 18;
        private int FreqTop = 36, FreqBottom = 1;
        private byte ChnlMsk = 0xF7;
        public CmdQ_t CmdQ;

        #region ============================= Init / deinit ============================
        public MainForm() {
            InitializeComponent();
            Transmitter = new Transmitter_t() {
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
                Transmitter.ComPort.Close();
                Transmitter.ComPort = null;
                serialPort1.Dispose();
            }
            catch(System.Exception ex) {
                Transmitter.LastError = ex.Message;
            }
            Transmitter.IsConnected = false;
        }
        #endregion

        #region ============================= Events ============================
        private void IndicateFailure() {
            if(Transmitter.IsConnected) {
                lblPythonReplying.Visible = true;  // Transmitter connected, but no reply
                lblPythonReplying.Text = "Питон не отвечает";
            }
            else {  // Transmitter disconnected
                lblPythonReplying.Visible = false;
                StatusLabel.Text = "Передатчик не подключен";
                timerHeartBeat.Interval = 1000;
            }
        }

        private void TimerHeartBeat_Tick(object sender, EventArgs e) {
            // Check if device connected
            if(Transmitter.IsConnected) {
                // Send commands until q is empty
                while(CmdQ.Get(out string Cmd)) {
                    if(Transmitter.SendCmd(Cmd) == false) { // Some failure
                        IndicateFailure();
                        return;
                    }
                }
                // Q is empty, Request info
                if(Transmitter.GetInfo()) {
                    // Show reply
                    for(int i=0; i<8; i++) {
                        int src = Transmitter.LastT[i + 1];
                        chart1.Series[0].Points[i].YValues[0] = (float)src / 10;
                    }
                    chart1.Refresh();
                    // Show that it is ok
                    if(!lblPythonReplying.Visible) {
                        lblPythonReplying.Visible = true;
                        lblPythonReplying.Text = "Питон на связи";
                        timerHeartBeat.Interval = 100;
                    }
                }
                else { // Some failure
                    IndicateFailure();
                }
            } //if is connected
            // Not connected, try to connect
            else { 
                try {
                    serialPort1.Close();
                }
                catch { }
                // First, get port names
                string[] SPorts = System.IO.Ports.SerialPort.GetPortNames();
                int NPorts = SPorts.Length;
                if(NPorts == 0) {
                    StatusLabel.Text = "Передатчик не подключен";
                    //labelHelp.Text = "В системе нет ни одного последовательного порта. Если стенд подключен, то, вероятно, не установлены драйверы USB-Serial.";
                }
                else {
                    // Iterate all discovered ports
                    for(int i = 0; i < NPorts; i++) {
                        serialPort1.PortName = SPorts[i];
                        try {
                            serialPort1.Open();
                            // Try to ping device
                            if(Transmitter.Ping()) { // Device found
                                Transmitter.IsConnected = true;
                                StatusLabel.Text = "Передатчик подключен";
                                return; // All right, nothing to do here
                            }
                            else serialPort1.Close(); // Try next port
                        } // try
                        catch {
                            serialPort1.Close();
                        }
                    } //for
                    // Silence answers our cries
                    //labelHelp.Text = "Ни на одном из имеющихся в системе последовательных портов стенд не обнаружен.";
                    StatusLabel.Text = "Передатчик не подключен";
                    //StatusLabel.Image = imageList1.Images[1];
                } // if(NPorts != 0) 
            } //else
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
            if(Transmitter.IsConnected) {
                CmdQ.Put(CmdSetParam(ParamIDs.SetChannels, ChnlMsk));
            }
            // Change color
            if(IsOn) chart1.Series[0].Points[Channel - 1].Color = SystemColors.Highlight;
            else chart1.Series[0].Points[Channel - 1].Color = SystemColors.InactiveCaption;

        }

        #region ==== Top/Bottom temperature selection ====
        private void BtnTTopLeds_Click(object sender, EventArgs e) {
            if(Int32.TryParse(txtbTTopLeds.Text, out int NewT)) {
                if(NewT > TBottom) {
                    // Send command
                    if(Transmitter.IsConnected) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetTTop, NewT));
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
                    if(Transmitter.IsConnected) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetTBottom, NewT));
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

        #region ==== Top/Bottom blink frequency selection ====
        private void BtnTopBlinkFreq_Click(object sender, EventArgs e) {
            if(Int32.TryParse(txtbTopBlinkFreq.Text, out int NewF)) {
                if(NewF > FreqBottom) {
                    // Send command
                    if(Transmitter.IsConnected) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetBlinkFTop, NewF));
                    }
                    // Change variable
                    FreqTop = NewF;
                }
            }
        }

        private void BtnBottomBlinkFreq_Click(object sender, EventArgs e) {
            if(Int32.TryParse(txtbBottomBlinkFreq.Text, out int NewF)) {
                if(NewF < FreqTop) {
                    // Send command
                    if(Transmitter.IsConnected) {
                        CmdQ.Put(CmdSetParam(ParamIDs.SetBlinkFBottom, NewF));
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

        #endregion
    } // Mainform


    #region ========================= Classes =========================
    public class Transmitter_t {
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
                IsConnected = false;
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
        public bool IsConnected;
        public System.IO.Ports.SerialPort ComPort;
        public string LastError;

        public Transmitter_t() {
            IsConnected = false;
            LastError = "";
        }

        // ==== Commands ====
        public bool Ping() {
            if(SendAndGetReply("Ping", out string SReply)) {
                return SReply.Equals("Ack 0", StringComparison.OrdinalIgnoreCase);
            }
            else return false;
        }

        public bool SendCmd(string SCmd) {
            //string SCmd = "SetParam " + Cmd.Cmd.ToString() + ' ' + Cmd.Data1.ToString() + ' ' + Cmd.Data2.ToString() + "\r\n";
            if(SendAndGetReply(SCmd, out string SReply)) {
                return SReply.Equals("Ack 0", StringComparison.OrdinalIgnoreCase);
            }
            else return false;
        }

        public int[] LastT = new int[9];
        public bool GetInfo() {
            if(SendAndGetReply("GetInfo", out string SReply)) {
                string[] Tokens = SReply.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if(Tokens[0].Equals("Info", StringComparison.OrdinalIgnoreCase) && Tokens.Length == 9) {
                    for(int i = 1; i <= 8; i++) {
                        if(!int.TryParse(Tokens[i], out LastT[i])) return false;    // parse failed
                    }
                    return true;    // everything parsed properly
                }
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