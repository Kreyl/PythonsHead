using System;
using System.Windows.Forms;
using System.Drawing;


namespace PythonControl {
    public enum Rslt { OK = 0, Failure = 1, CmdError = 2 };

    
    public partial class MainForm : Form {
        private Transmitter_t Transmitter;
        public int TTop = 45, TBottom = 18;
        #region ============================= Init / deinit ============================
        public MainForm() {
            InitializeComponent();
            Transmitter = new Transmitter_t();
            Transmitter.ComPort = serialPort1;

            chart1.ChartAreas[0].AxisY.Minimum = TBottom;
            chart1.ChartAreas[0].AxisY.Maximum = TTop;
            txtbTTopLeds.Text = TTop.ToString();
            txtbTBottomLeds.Text = TBottom.ToString();
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
        private void TimerHeartBeat_Tick(object sender, EventArgs e) {
            // Check if device connected
            if(Transmitter.IsConnected) {
                // Ping device to check if it is still ok
                if(Transmitter.Ping() == Rslt.OK) return;   // Nothing to do if it's ok
                else {
                    Transmitter.IsConnected = false;
                    // Show "Not connected"
                    StatusLabel.Text = "Передатчик не подключен";
                    return; // Try to connect next time
                } //else
            } //if is connected
            else { // Not connected, try to connect
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
                            if(Transmitter.Ping() == Rslt.OK) { // Device found
                                Transmitter.IsConnected = true;
                                StatusLabel.Text = "Передатчик подключен";
                                //labelHelp.Text = "Стенд подключен и готов к работе." + Environment.NewLine + "Выберите нужный тип фильтра, задайте его порядок, введите коэффициенты. После этого нажмите кнопку 'Применить настройки' или 'Enter', чтобы передать данные стенду и включить фильтр.";
                                return; // All right, nothing to do here
                            }
                            else serialPort1.Close(); // Try next port
                        } // try
                        catch(System.Exception ex) {
                            //labelHelp.Text = ex.Message;
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
        private int Checkbox2Channel(CheckBox cb) {
            string S = cb.Tag.ToString();
            int Channel;
            Int32.TryParse(S, out Channel);
            return Channel;
        }

        private void CbChLeds_CheckedChanged(object sender, EventArgs e) {
            int Channel = Checkbox2Channel((CheckBox)sender);
            bool IsOn = ((CheckBox)sender).Checked;
            // TODO: send command
            // Change color
            if(IsOn) chart1.Series[0].Points[Channel - 1].Color = SystemColors.Highlight;
            else chart1.Series[0].Points[Channel - 1].Color = SystemColors.InactiveCaption;

        }

        private void btnTTopLeds_Click(object sender, EventArgs e) {
            int NewT;
            if(Int32.TryParse(txtbTTopLeds.Text, out NewT)) {
                if(NewT > TBottom) {
                    // TODO: send cmd
                    chart1.ChartAreas[0].AxisY.Maximum = NewT;
                    TTop = NewT;
                }
            }
        }
        private void btnTBottomLeds_Click(object sender, EventArgs e) {
            int NewT;
            if(Int32.TryParse(txtbTBottomLeds.Text, out NewT)) {
                if(NewT < TTop) {
                    // TODO: send cmd
                    chart1.ChartAreas[0].AxisY.Minimum = NewT;
                    TBottom = NewT;
                }
            }
        }

        #endregion


    } // Mainform


    #region ========================= Classes =========================
    public class Transmitter_t {
        // ==== Private ====
        Rslt SendAndGetReply(string SCmd, out string SReply) {
            SReply = "";
            if(!ComPort.IsOpen) return Rslt.Failure;
            // Send string
            try {
                ComPort.WriteLine(SCmd);
                SReply = ComPort.ReadLine().Trim();
                return Rslt.OK;
            }
            catch(System.Exception ex) {
                IsConnected = false;
                LastError = ex.Message;
                try {
                    ComPort.Close();
                }
                catch(System.Exception ex2) {
                    LastError = ex2.Message;
                }
            }
            return Rslt.Failure;
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
        public Rslt Ping() {
            string SReply;
            if(SendAndGetReply("Ping", out SReply) == Rslt.OK) {
                if(SReply.Equals("Ack 0", StringComparison.OrdinalIgnoreCase)) return Rslt.OK;
            }
            return Rslt.Failure;
        }

        public Rslt Command(string SCmd) {
            string SReply;
            if(SendAndGetReply(SCmd, out SReply) == Rslt.OK) {
                if(SReply.Equals("Ack 0", StringComparison.OrdinalIgnoreCase)) return Rslt.OK;
                else return Rslt.CmdError;
            }
            return Rslt.Failure;
        }
    } // CrystalDevice_t
    #endregion

}