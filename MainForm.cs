using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TVSimulator
{
    public partial class MainForm : Form
    {
        private SerialPort serialPort;
        private ComboBox portCombo;
        private ComboBox baudCombo;
        private ComboBox dataBitsCombo;
        private ComboBox stopBitsCombo;
        private ComboBox parityCombo;
        private Button connectButton;
        private NumericUpDown tvIdField;
        private ComboBox tvMfrCombo;
        private TextBox tvmodelsTextBox;
        private Button powerButton;
        private Button lockButton;
        private Button muteButton;
        private Button OSDButton;
        private NumericUpDown volumeUpDown;
        private ComboBox inputCombo;
        private ComboBox OPSCombo;
        private ComboBox ratioCombo;
        private Label statusLabel;
        private Label receiveLabel;
        private Label sentLabel;
        private Label msgLabel;
        private AbstractTV tv;
        private bool connected = false;

        public MainForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            tv = new LGTV(powerButton, lockButton, volumeUpDown, muteButton, OSDButton, inputCombo, ratioCombo, OPSCombo);
            string[] models = tv.GetTVModels();
            tvmodelsTextBox.Text = string.Join("\r\n", models);
            DisableTVControls();

        }

        private void InitializeComponent()
        {
            Text = "TV Simulator";
            Size = new Size(600, 780);
            StartPosition = FormStartPosition.CenterScreen;

            // Menu bar
            var menuStrip = new MenuStrip();
            var helpMenu = new ToolStripMenuItem("Help");
            var aboutMenuItem = new ToolStripMenuItem("About");
            aboutMenuItem.Click += AboutMenuItem_Click;
            helpMenu.DropDownItems.Add(aboutMenuItem);
            menuStrip.Items.Add(helpMenu);

            // Container that holds all existing panels below the menu bar,
            // so panel coordinates stay relative and don't overlap the menu.
            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            // Serial Settings Panel
            var serialPanel = new GroupBox
            {
                Text = "Serial Settings",
                Location = new Point(10, 10),
                Size = new Size(560, 120)
            };

            portCombo = new ComboBox
            {
                Location = new Point(110, 25),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            portCombo.Items.AddRange(SerialPort.GetPortNames());

            baudCombo = new ComboBox
            {
                Location = new Point(320, 25),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            baudCombo.Items.AddRange(new[] { "9600", "19200", "38400", "57600", "115200" });
            baudCombo.SelectedItem = "9600";

            dataBitsCombo = new ComboBox
            {
                Location = new Point(110, 55),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            dataBitsCombo.Items.AddRange(new[] { "7", "8" });
            dataBitsCombo.SelectedItem = "8";

            stopBitsCombo = new ComboBox
            {
                Location = new Point(320, 55),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            stopBitsCombo.Items.AddRange(new[] { "1", "2" });
            stopBitsCombo.SelectedItem = "1";

            parityCombo = new ComboBox
            {
                Location = new Point(110, 85),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            parityCombo.Items.AddRange(new[] { "None", "Even", "Odd" });
            parityCombo.SelectedItem = "None";

            connectButton = new Button
            {
                Text = "Connect",
                Location = new Point(450, 85),
                Size = new Size(80, 25)
            };
            connectButton.Click += ConnectButton_Click;

            serialPanel.Controls.AddRange(new Control[]
            {
                new Label { Text = "COM Port:", Location = new Point(10, 28) },
                portCombo,
                new Label { Text = "Baud Rate:", Location = new Point(220, 28) },
                baudCombo,
                new Label { Text = "Data Bits:", Location = new Point(10, 58) },
                dataBitsCombo,
                new Label { Text = "Stop Bits:", Location = new Point(220, 58) },
                stopBitsCombo,
                new Label { Text = "Parity:", Location = new Point(10, 88) },
                parityCombo,
                connectButton
            });

            // TV Settings Panel
            var tvPanel = new GroupBox
            {
                Text = "TV Settings",
                Location = new Point(10, 140),
                Size = new Size(560, 110)
            };

            tvIdField = new NumericUpDown
            {
                Location = new Point(110, 25),
                Size = new Size(50, 25),
                Minimum = 0,
                Maximum = 100,
                Value = 1
            };
            tvIdField.ValueChanged += TvIdField_ValueChanged;

            tvMfrCombo = new ComboBox
            {
                Location = new Point(280, 25),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            tvMfrCombo.Items.Add("LG");
            tvMfrCombo.Items.Add("LG2");
            tvMfrCombo.Items.Add("Sealoc");
            tvMfrCombo.Items.Add("Zenith");
            tvMfrCombo.SelectedItem = "LG";
            tvMfrCombo.SelectedIndexChanged += ModelCombo_SelectedIndexChanged;

            tvmodelsTextBox = new TextBox
            {
                Text = "",
                Location = new Point(415, 45),
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Multiline = true,
                Size = new Size(110, 50)
            };

            tvPanel.Controls.AddRange(new Control[]
            {
                new Label { Text = "TV ID:", Location = new Point(10, 28) },
                tvIdField,
                new Label { Text = "TV MFR:", Location = new Point(180, 28) },
                tvMfrCombo,
                new Label { Text = "TV Models:", Location = new Point(415, 20) },
                tvmodelsTextBox
            });

            // TV Controls Panel
            var controlPanel = new GroupBox
            {
                Text = "TV Controls",
                Location = new Point(10, 270),
                Size = new Size(560, 250)
            };

            powerButton = new Button
            {
                Text = "Power OFF",
                Location = new Point(60, 30),
                Size = new Size(70, 60),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            powerButton.Click += PowerButton_Click;
            controlPanel.Controls.Add(powerButton);

            muteButton = new Button
            {
                Text = "Mute OFF",
                Location = new Point(160, 30),
                Size = new Size(70, 60),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            muteButton.Click += MuteButton_Click;
            controlPanel.Controls.Add(muteButton);

            OSDButton = new Button
            {
                Text = "OSD OFF",
                Location = new Point(260, 30),
                Size = new Size(70, 60),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            OSDButton.Click += OSDButton_Click;
            controlPanel.Controls.Add(OSDButton);

            lockButton = new Button
            {
                Text = "Lock OFF",
                Location = new Point(360, 30),
                Size = new Size(70, 60),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            lockButton.Click += LockButton_Click;
            controlPanel.Controls.Add(lockButton);

            volumeUpDown = new NumericUpDown
            {
                Location = new Point(80, 120),
                Size = new Size(60, 25),
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };
            volumeUpDown.ValueChanged += VolumeUpDown_ValueChanged;
            controlPanel.Controls.AddRange(new Label { Text = "Volume:", Location = new Point(20, 120), Size = new Size(50, 20) }, volumeUpDown);

            inputCombo = new ComboBox
            {
                Location = new Point(80, 160),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            inputCombo.SelectedIndexChanged += InputCombo_SelectedIndexChanged;
            controlPanel.Controls.AddRange(new Control[] { new Label { Text = "Input:", Location = new Point(20, 163), Size = new Size(35, 20) }, inputCombo });

            OPSCombo = new ComboBox
            {
                Location = new Point(330, 160),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            OPSCombo.SelectedIndexChanged += OPSCombo_SelectedIndexChanged;
            controlPanel.Controls.AddRange(new Control[] { new Label { Text = "OPS:", Location = new Point(295, 163), Size = new Size(30, 20) }, OPSCombo });


            ratioCombo = new ComboBox
            {
                Location = new Point(80, 200),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            ratioCombo.SelectedIndexChanged += RatioCombo_SelectedIndexChanged;
            controlPanel.Controls.AddRange(new Control[] { new Label { Text = "Ratio:", Location = new Point(20, 203), Size = new Size(40, 20) }, ratioCombo }); 

            DisableTVControls();

            var messagingPanel = new GroupBox
            {
                Text = "Messaging",
                Location = new Point(10, 540),
                Size = new Size(560, 150)
            };

            // Status Panel
            statusLabel = new Label
            {
                Text = "Status: Disconnected",
                Location = new Point(10, 30),
                Size = new Size(300, 25)
            };

            receiveLabel = new Label
            {
                Text = "Received: ",
                Location = new Point(10, 55),
                Size = new Size(300, 25)
            };

            sentLabel = new Label
            {
                Text = "Sent: ",
                Location = new Point(10, 80),
                Size = new Size(300, 25)
            };

            msgLabel = new Label
            {
                Text = "ErrorLog: ",
                Location = new Point(10, 105),
                Size = new Size(300, 25),
                ForeColor = Color.Red,
                Visible = false
            };

            messagingPanel.Controls.AddRange(new Control[] { statusLabel, receiveLabel, sentLabel, msgLabel });

            contentPanel.Controls.AddRange(new Control[] { serialPanel, tvPanel, controlPanel, messagingPanel });

            // Add content first, then dock the menu on top. MainMenuStrip wires
            // up the menu so docked children lay out below it.
            Controls.Add(contentPanel);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string version =
                assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                ?? assembly.GetName().Version?.ToString()
                ?? "Unknown";

            // Strip any build metadata suffix (e.g. "1.0.0+abc123") for display.
            int plusIndex = version.IndexOf('+');
            if (plusIndex >= 0)
            {
                version = version.Substring(0, plusIndex);
            }

            MessageBox.Show(
                $"TV Simulator\r\nVersion {version}",
                "About TV Simulator",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void DisableTVControls()
        {
            lockButton.Enabled = false;
            lockButton.Text = "Lock OFF";
            lockButton.BackColor = Color.Red;
            
            powerButton.Enabled = false;
            powerButton.Text = "Power OFF";
            powerButton.BackColor = Color.Red;
            volumeUpDown.Enabled = false;

            muteButton.Enabled = false;
            muteButton.Text = "Mute OFF";
            muteButton.BackColor = Color.Red;

            OSDButton.Enabled = false;
            OSDButton.Text = "OSD OFF";
            OSDButton.BackColor = Color.Red;
                    
            inputCombo.Enabled = false;
            ratioCombo.Enabled = false;
            OPSCombo.Enabled = false;
        }

        private void EnableTVControls()
        {
            UpdatePowerButton();
            UpdateTvId();
            UpdateInputOptions();
            UpdateLockButton();
            UpdateMuteButton();
            UpdateOSDButton();
            UpdateRatioOptions();
            UpdateVolume();
            UpdateOPSOptions();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                tvMfrCombo.Enabled = false;
                tvIdField.Enabled = false;
                EnableTVControls();
                ConnectSerial();
                msgLabel.Text = "";
                receiveLabel.Text = "";
                sentLabel.Text = "";
            }
            else
            {
                tvMfrCombo.Enabled = true;
                tvIdField.Enabled = true;
                DisableTVControls();
                DisconnectSerial();
            }
        }

        private void PowerButton_Click(object sender, EventArgs e)
        {
            string currentPower = tv.GetPower();
            tv.SetPower(currentPower == "01" ? "00" : "01");
        }

        private void UpdatePowerButton()
        {
            try 
            {
                bool power = tv.GetPower() == "01";
                if (power)
                {
                    powerButton.Text = "Power ON";
                    powerButton.BackColor = Color.Green;
                }
                else
                {
                    powerButton.Text = "Power OFF";
                    powerButton.BackColor = Color.Red;
                }
                powerButton.Enabled = true;
            } catch (Exception ex)
            {
                if(ex is NotImplementedException)
                {
                    powerButton.BackColor = Color.Gray;
                }
                Console.WriteLine(ex.Message);
            }
        }

        private void MuteButton_Click(object sender, EventArgs e)
        {
            string currentMute = tv.GetMute();
            tv.SetMute(currentMute == "01" ? "00" : "01");
            UpdateMuteButton();

        }

        private void UpdateMuteButton()
        {
            try
            {
                string muteValue = tv.GetMute();
                bool mute = muteValue == "00";

                if (mute)
                {
                    muteButton.Text = "Mute ON";
                    muteButton.BackColor = Color.Green;
                }
                else
                {
                    muteButton.Text = "Mute OFF";
                    muteButton.BackColor = Color.Red;
                }
                muteButton.Enabled = true;
            } catch (Exception ex)
            {
                if(ex is NotImplementedException)
                {
                    muteButton.BackColor = Color.Gray;
                    muteButton.Enabled = false;
                }
                Console.WriteLine(ex.Message);
            }
        }

        private void OSDButton_Click(object sender, EventArgs e)
        {
            string currentOSD = tv.GetOSD();
            tv.SetOSD(currentOSD == "01" ? "00" : "01");
        }

        private void UpdateOSDButton()
        {
            try 
            {
                bool OSD = tv.GetOSD() == "01";

                if (OSD)
                {
                    OSDButton.Text = "OSD ON";
                    OSDButton.BackColor = Color.Green;
                }
                else
                {
                    OSDButton.Text = "OSD OFF";
                    OSDButton.BackColor = Color.Red;
                }
                OSDButton.Enabled = true;
            } catch (Exception ex)
            {
                if(ex is NotImplementedException)
                {
                    OSDButton.Enabled = false;
                    OSDButton.BackColor = Color.Gray; 
                }
                Console.WriteLine(ex.Message);
            }
        }

        private void LockButton_Click(object sender, EventArgs e)
        {
            string currentLock = tv.GetLock();
            tv.SetLock(currentLock == "01" ? "00" : "01");
        }

        private void UpdateLockButton()
        {
            try
            {
                bool lockTV = tv.GetLock() == "01";
                if (lockTV)
                {
                    lockButton.Text = "Lock ON";
                    lockButton.BackColor = Color.Green;
                }
                else
                {
                    lockButton.Text = "Lock OFF";
                    lockButton.BackColor = Color.Red;
                }
                lockButton.Enabled = true;
            } catch (Exception ex)
            {
                if(ex is NotImplementedException)
                {
                    lockButton.BackColor = Color.Gray;
                    lockButton.Enabled = false;
                }
                Console.WriteLine(ex.Message);
            }
        }

        private void VolumeUpDown_ValueChanged(object sender, EventArgs e)
        {
            tv.SetVolume((int)volumeUpDown.Value);
        }

        private void UpdateVolume()
        {
            volumeUpDown.Value = tv.GetVolume();
            volumeUpDown.Enabled = true;
        }
        
        private void InputCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tv.SetInputName(inputCombo.SelectedItem.ToString());
        }

        private void UpdateInputOptions()
        {
            try
            {
                inputCombo.Items.Clear();
                inputCombo.Items.AddRange(tv.GetAvailableInputs());
                inputCombo.SelectedItem = tv.GetInput();
                if (tv.GetAvailableInputs().Length < 2)
                {
                    inputCombo.Enabled = false;
                }
                else
                {
                    inputCombo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if(ex is NotImplementedException)
                {
                    inputCombo.Enabled = false;
                }
                Console.WriteLine(ex.Message);
            }
        }
        
        private void OPSCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tv.SetOPSName(OPSCombo.SelectedItem.ToString());
        }

        private void UpdateOPSOptions()
        {
            try
            {
                OPSCombo.Items.Clear();
                OPSCombo.Items.AddRange(tv.GetAvailableOPS());
                OPSCombo.SelectedItem = tv.GetOPSName(tv.GetOPS());
                if (tv.GetAvailableOPS().Length < 2)
                {
                    OPSCombo.Enabled = false;
                }
                else
                {
                    OPSCombo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if(ex is NotImplementedException)
                {
                    OPSCombo.Enabled = false;
                }
                Console.WriteLine(ex.Message);
            }
        }
        
        private void ModelCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tvMfrCombo.SelectedItem.ToString() == "LG")
            {
                tv = new LGTV(powerButton, lockButton, volumeUpDown, muteButton, OSDButton, inputCombo, ratioCombo, OPSCombo);
                string[] models = tv.GetTVModels();
                tvmodelsTextBox.Text = string.Join("\r\n", models);
            }
            else if (tvMfrCombo.SelectedItem.ToString() == "LG2")
            {
                tv = new LGTV2(powerButton, lockButton, volumeUpDown, muteButton, OSDButton, inputCombo, ratioCombo, OPSCombo);
                string[] models = tv.GetTVModels();
                tvmodelsTextBox.Text = string.Join("\r\n", models);
            }else if (tvMfrCombo.SelectedItem.ToString() == "Sealoc")
            {
                tv = new SealocTV(powerButton, lockButton, volumeUpDown, muteButton, OSDButton, inputCombo, ratioCombo, OPSCombo);
                string[] models = tv.GetTVModels();
                tvmodelsTextBox.Text = string.Join("\r\n", models);
            }
            else if (tvMfrCombo.SelectedItem.ToString() == "Zenith")
            {
                tv = new Zenith(powerButton, lockButton, volumeUpDown, muteButton, OSDButton, inputCombo, ratioCombo, OPSCombo);
                string[] models = tv.GetTVModels();
                tvmodelsTextBox.Text = string.Join("\r\n", models);
            }
        }


        
        private void RatioCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tv.SetAspectRatioName(ratioCombo.SelectedItem.ToString());
        }   
        private void UpdateRatioOptions()
        {
            ratioCombo.Items.Clear();
            try
            {
                ratioCombo.Items.AddRange(tv.GetAvailableAspectRatio());
                ratioCombo.SelectedItem = tv.GetAspectRatio();
                ratioCombo.Enabled = true;
            } catch (Exception ex)
            {
                if(ex is NotImplementedException)
                {
                    ratioCombo.Enabled = false;
                }
                Console.WriteLine(ex.Message);
            }
        }   

        private void TvIdField_ValueChanged(object sender, EventArgs e)
        {
            UpdateTvId();
        }
        
        private void UpdateTvId()
        {
            tv?.SetTVId((int)tvIdField.Value);
        }

        private void ConnectSerial()
        {
            try
            {
                serialPort = new SerialPort
                {
                    PortName = portCombo.SelectedItem?.ToString(),
                    BaudRate = int.Parse(baudCombo.SelectedItem.ToString()),
                    DataBits = int.Parse(dataBitsCombo.SelectedItem.ToString()),
                    StopBits = stopBitsCombo.SelectedItem.ToString() == "1" ? StopBits.One : StopBits.Two,
                    Parity = GetParityValue()
                };

                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();

                connected = true;
                connectButton.Text = "Disconnect";
                statusLabel.Text = $"Status: Connected to {portCombo.SelectedItem}";
                SetSerialControlsEnabled(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisconnectSerial()
        {
            connected = false;
            serialPort?.Close();
            serialPort?.Dispose();
            serialPort = null;

            connectButton.Text = "Connect";
            statusLabel.Text = "Status: Disconnected";
            SetSerialControlsEnabled(true);
        }

        private void SetSerialControlsEnabled(bool enabled)
        {
            portCombo.Enabled = enabled;
            baudCombo.Enabled = enabled;
            dataBitsCombo.Enabled = enabled;
            stopBitsCombo.Enabled = enabled;
            parityCombo.Enabled = enabled;
        }

        private Parity GetParityValue()
        {
            return parityCombo.SelectedItem.ToString() switch
            {
                "Even" => Parity.Even,
                "Odd" => Parity.Odd,
                _ => Parity.None
            };
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(10); // Small delay to ensure complete message
                byte[] buffer = new byte[serialPort.BytesToRead];
                serialPort.Read(buffer, 0, buffer.Length);
                
                if (receiveLabel.InvokeRequired)
                {
                    receiveLabel.Invoke(new System.Windows.Forms.MethodInvoker(() => ProcessMessage(buffer)));
                }
                else
                {
                    ProcessMessage(buffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serial error: {ex.Message}");
            }
        }

        private void ProcessMessage(byte[] data)
        {
            try
            {
                string dataAsString = BitConverter.ToString(data); //Encoding.ASCII.GetString(data);
                Console.WriteLine($"Incoming msg: {dataAsString}");
                receiveLabel.Text = $"Received: {dataAsString}";

                (byte[] response, string message) = tv.processIncoming(data);
                string errorString = "error";

                if (response != null && response.Length > 0)
                {
                    if (message != null)
                    {
                        if (message.Contains(errorString, StringComparison.OrdinalIgnoreCase))
                        {
                            this.msgLabel.ForeColor = Color.Red;
                            this.msgLabel.Text = $"{message}";
                            this.msgLabel.Visible = true;
                        } else
                        {
                            this.msgLabel.ForeColor = Color.Black;
                            this.msgLabel.Text = $"{message}";
                            this.msgLabel.Visible = true;
                        }
                    }
                    else
                    {
                        this.msgLabel.ForeColor = Color.Red;
                        this.msgLabel.Text = "ErrorLog: ";
                        this.msgLabel.Visible = false;

                    }
                    string responseAsString = BitConverter.ToString(response); //Encoding.ASCII.GetString(response);
                    Console.WriteLine($"Outgoing msg: {responseAsString}");
                    sentLabel.Text = $"Sent: {responseAsString}";
                    serialPort.Write(response, 0, response.Length);
                }
                else
                {
                    Console.WriteLine("No response");
                    if (message != null)
                    {
                        if (message.Contains(errorString, StringComparison.OrdinalIgnoreCase))
                        {
                            this.msgLabel.ForeColor = Color.Red;
                            this.msgLabel.Text = $"{message}";
                            this.msgLabel.Visible = true;
                        }
                    }
                    else
                    {
                        this.msgLabel.Text = "ErrorLog: msg is null";
                        this.msgLabel.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message processing error: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DisconnectSerial();
            base.OnFormClosing(e);
        }
    }
}