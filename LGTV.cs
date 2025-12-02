using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TVSimulator
{

    public class LGTV : AbstractTV
    {
        private Button powerButton;
        private Button lockButton;
        private Button muteButton;
        private Button OSDButton;
        private NumericUpDown volumeUpDown;
        private ComboBox inputCombo;
        private ComboBox ratioCombo;
        private ComboBox OPSCombo;
        private string input = "HDMI1";
        private new bool mute = true;
        private string AspectRatio = "FullWide";

        private readonly new string[] TVModels = { "43UD79" };

        public LGTV(Button powerButton, Button lockButton, NumericUpDown volumeUpDown, Button muteButton,
                    Button OSDButton, ComboBox inputCombo, ComboBox ratioCombo, ComboBox OPSCombo)
        {
            this.powerButton = powerButton;
            this.lockButton = lockButton;
            this.volumeUpDown = volumeUpDown;
            this.muteButton = muteButton;
            this.OSDButton = OSDButton;
            this.inputCombo = inputCombo;
            this.ratioCombo = ratioCombo;
            this.OPSCombo = OPSCombo;
        }

        public override string GetPower()
        {
            return power ? "01" : "00";
        }

        public override Boolean SetPower(string value)
        {
            switch (value)
            {
                case "00":
                    power = false;
                    UpdatePowerButton();
                    return true;
                case "01":
                    power = true;
                    UpdatePowerButton();
                    return true;
                default:
                    return false;
            }
        }

        public override void SetTVId(int value)
        {
            this.tvId = value;
        }

        public override int GetTVId()
        {
            return this.tvId;
        }

        public override Boolean SetMute(string value)
        {
            switch (value)
            {
                case "00":
                    mute = true;
                    UpdateMuteButton();
                    return true;
                case "01":
                    mute = false;
                    UpdateMuteButton();
                    return true;
                default:
                    return false;
            }  
            
        }
        public override string GetMute()
        {
            return mute ? "01" : "00"; //yes, this is reversed per manual
        }

        public override Boolean SetLock(string value)
        {
            switch (value)
            {
                case "00":
                    lockTV = false;
                    UpdateLockButton();
                    return true;
                case "01":
                    lockTV = true;
                    UpdateLockButton();
                    return true;
                default:
                    return false;
            }
        }

        public override string GetOSD()
        {
            return OSDLock ? "01" : "00";
        }

        public override Boolean SetOSD(string value)
        {
            switch (value)
            {
                case "00":
                    OSDLock = false;
                    UpdateOSDButton();
                    return true;
                case "01":
                    OSDLock = true;
                    UpdateOSDButton();
                    return true;
                default:
                    return false;
            }
        }


        public override string GetLock()
        {
            return lockTV ? "01" : "00";
        }

        public override Boolean SetVolume(int value)
        {
            if (value >= 0 && value <= 100)
            {
                volume = value;
                UpdateVolumeUpDown();
                return true;
            }
            return false;
        }

        public override int GetVolume()
        {
            return volume;
        }

        //this is called when processing incoming serial message
        public override Boolean SetInput(string value)
        {
            switch (value)
            {
                case "90": //HDMI1
                    input = "HDMI1";
                    UpdateInput();
                    return true;
                case "91": //HDMI2
                    input = "HDMI2";
                    UpdateInput();
                    return true;
                case "92": //HDMI3
                    input = "HDMI3";
                    UpdateInput();
                    return true;
                case "93": //HDMI4
                    input = "HDMI4";
                    UpdateInput();
                    return true;
                case "C0": //DP
                    input = "DP";
                    UpdateInput();
                    return true;
                case "E0": //USB-C
                    input = "USB-C";
                    UpdateInput();
                    return true;
                default:
                    return false;
            }
        }

        //this method should only be called by front end when user changed input combo box
        public override void SetInputName(string value)
        {
            switch (value)
            {
                case "HDMI1":
                case "HDMI2":
                case "HDMI3":
                case "HDMI4":
                case "DP":
                case "USB-C":
                    input = value;
                    break;
                default:  //this should never happen
                    break;
            }
        }

        public override string GetInputName(string value)
        {
            switch (value)
            {
                case "90":
                    return "HDMI1";
                case "91":
                    return "HDMI2";
                case "92":
                    return "HDMI3";
                case "93":
                    return "HDMI4";
                case "C0":
                    return "DP";
                case "E0":
                    return "USB-C";
                default:
                    return "Unknown";
            }
        }

        public override string GetInput()
        {
            return input;
        }

        public override string[] GetAvailableInputs()
        {
            return new[] { "HDMI1", "HDMI2", "HDMI3", "HDMI4", "DP", "USB-C" };
        }

        public override string[] GetTVModels()
        {
            return TVModels;
        }

        private void UpdatePowerButton()
        {
            if (powerButton == null) return;
            
            if (powerButton.InvokeRequired)
            {
                powerButton.BeginInvoke(new Action(UpdatePowerButton));
                return;
            }

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
        }

        private void UpdateMuteButton()
        {
            
            if (muteButton.InvokeRequired)
            {
                muteButton.Invoke(new Action(UpdateMuteButton));
                return;
            }
            if (mute)
            {
                muteButton.Text = "Mute OFF";
                muteButton.BackColor = Color.Red;
            }
            else
            {
                muteButton.Text = "Mute ON";
                muteButton.BackColor = Color.Green;
            }
        }

        private void UpdateOSDButton()
        {
            if (OSDButton == null) return;
            
            if (OSDButton.InvokeRequired)
            {
                OSDButton.Invoke(new Action(UpdateOSDButton));
                return;
            }

            if (OSDLock)
            {
                OSDButton.Text = "OSD ON";
                OSDButton.BackColor = Color.Green;
            }
            else
            {
                OSDButton.Text = "OSD OFF";
                OSDButton.BackColor = Color.Red;
            }
        }

        private void UpdateLockButton()
        {
            if (lockButton == null) return;
            
            if (lockButton.InvokeRequired)
            {
                lockButton.Invoke(new Action(UpdateLockButton));
                return;
            }

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
        }

        private void UpdateVolumeUpDown()
        {
            if (volumeUpDown == null) return;
            
            if (volumeUpDown.InvokeRequired)
            {
                volumeUpDown.Invoke(new Action(UpdateVolumeUpDown));
                return;
            }

            volumeUpDown.Value = volume;
        }

        private void UpdateInput()
        {
            if (inputCombo.InvokeRequired)
            {
                inputCombo.Invoke(new Action(UpdateInput));
                return;
            }

            inputCombo.SelectedItem = this.input;
        }

       //this method should only be called by front end when user changes aspect ratio combo box
        public override void SetAspectRatioName(string value)
        {
            switch (value)
            {
                case "FullWide":
                case "Original":
                case "1:1":
                case "Cinema1":
                case "Cinema2":
                    AspectRatio = value;
                    break;
                default:  //this should never happen
                    break;
            }
        }

        public override string GetAspectRatio()
        {
            return AspectRatio;
        }

        public override bool SetAspectRatio(string value)
        {
            switch (value)
            {
                case "00": //FullWide
                    AspectRatio = "FullWide";
                    UpdateAspectRatio();
                    return true;
                case "01": //Original
                    AspectRatio = "Original";
                    UpdateAspectRatio();
                    return true;
                case "02": //1:1
                    AspectRatio = "1:1";
                    UpdateAspectRatio();
                    return true;
                case "03": //Cinema1
                    AspectRatio = "Cinema1";
                    UpdateAspectRatio();
                    return true;
                case "04": //Cinema2
                    AspectRatio = "Cinema2";
                    UpdateAspectRatio();
                    return true;
                default:
                    return false;
            }
        }

        public override string[] GetAvailableAspectRatio()
        {
            return new[] { "FullWide", "Original", "1:1", "Cinema1", "Cinema2" };
        }

        private string GetAspectRatioCode()
        {
            switch (AspectRatio)
            {
                case "FullWide":
                    return "00";
                case "Original":
                    return "01";
                case "1:1":
                    return "02";
                case "Cinema1":
                    return "03";
                case "Cinema2":
                    return "04";
                default:
                    return "Unknown";
            }
        }

        private void UpdateAspectRatio()
        {
            if (ratioCombo.InvokeRequired)
            {
                ratioCombo.Invoke(new Action(UpdateAspectRatio));
                return;
            }

            ratioCombo.SelectedItem = this.AspectRatio;
        }

        public override (byte[], string) processIncoming(byte[] data)
        {
            
            if (data.Length == 9 && data[8] == 0x0D)
            {

                try
                {
                    string dataAsString = Encoding.ASCII.GetString(data);

                    byte command1 = data[0];
                    byte command2 = data[1];



                    byte[] setBytes = { data[3], data[4] };
                    string setBytesStr = Encoding.ASCII.GetString(setBytes);
                    int msgSetId = int.Parse(setBytesStr);

                    if (msgSetId == this.tvId || msgSetId == 0)
                    {
                        //Console.WriteLine("nv matching setId");
                        if (command1 == 0x6B && command2 == 0x61) //ka
                        {
                            return PowerRequest(data);
                        }
                        else if (command1 == 0x6B && command2 == 0x6D) //km
                        {
                            return LockRequest(data);
                        }
                        else if (command1 == 0x6B && command2 == 0x66) //kf
                        {
                            return VolumeRequest(data);
                        }
                        else if (command1 == 0x6B && command2 == 0x65) //ke
                        {
                            return MuteRequest(data);
                        }
                        else if (command1 == 0x6B && command2 == 0x6C) //kl
                        {
                            return OSDRequest(data);
                        }
                        else if (command1 == 0x78 && command2 == 0x62) //xb
                        {
                            return InputRequest(data);
                        }
                        else if (command1 == 0x66 && command2 == 0x6B) //fk
                        {
                            return ResetRequest(data);
                        }
                        else if (command1 == 0x78 && command2 == 0x66) //xf
                        {
                            return AspectRatioRequest(data);
                        }
                        else
                        {
                            // unknown command, return error
                            //Console.WriteLine("unknown command");
                            return ErrorResponse(data, "Error:Unknown command");

                        }

                    }
                    else
                    {
                        //Console.WriteLine("mismatching setId");
                        // wrong setId, return error
                        return ErrorResponse(data, "Wrong setId");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Message processing error: {ex.Message}");
                }
            }
            return ErrorResponse(data, "Error:Unexpected Message Length");
        }



        private static (byte[],string) ErrorResponse(byte[] data, string errorText)
        {
            //Console.WriteLine(errorText);
            // 
            byte[] errorAckMsg = {
                data[1],
                0x20, // space
                data[3], // SetID
                data[4], // SetID
                0x20, // space
                0x4E, // "N"
                0x47, // "G"
                data[6], // Data
                data[7], // Data
                0x78 // 'x'
            };

            return (errorAckMsg,errorText);

        }

        private (byte[], string) PowerRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            //Console.WriteLine("power message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query power request
            
            if ((dataStr.Equals("FF")))
            {
                Console.WriteLine("power status query");
                string powerStatus = this.GetPower();

                // [OK/NG][Data][x]
                byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)powerStatus[0],
                    (byte)powerStatus[1], // Data
                    0x78 // 'x'
                };
                responseList.AddRange(bytesToAdd);
            } // set power request
            else
            {
                Console.WriteLine("power set request");
                string powerData = System.Text.Encoding.ASCII.GetString(data, 6, 2);

                if (SetPower(powerData))
                {
                    string powerStatus = GetPower();
                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        (byte)powerStatus[0],
                        (byte)powerStatus[1], // Data
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                }
                else
                {
                    // wrong data, return error
                    return ErrorResponse(data, "Error:Unsupported power request");
                }
            }

            return (responseList.ToArray(),"Power Request");
        }

        private (byte[],string) LockRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            //Console.WriteLine("lock message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query lock request

            if ((dataStr.Equals("FF")))
            {
                Console.WriteLine("lock status query");
                string lockStatus = this.GetLock();

                // [OK/NG][Data][x]
                byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)lockStatus[0],
                    (byte)lockStatus[1], // Data
                    0x78 // 'x'
                };
                responseList.AddRange(bytesToAdd);
            } // set lock request
            else
            {
                Console.WriteLine("lock set request");
                string lockData = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                if (SetLock(lockData))
                {
                    string lockStatus = GetLock();
                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        (byte)lockStatus[0],
                        (byte)lockStatus[1], // Data
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                }
                else
                {
                    // wrong data, return error
                    return ErrorResponse(data, "Error:Unsupported lock request");
                }
            }

            return (responseList.ToArray(),"Lock Request");
        } //LockRequest

        private (byte[],string) MuteRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            //Console.WriteLine("mute message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query mute request
            //Console.WriteLine("dataStr: " + dataStr);
            if ((dataStr.Equals("FF")))
            {
                //Console.WriteLine("mute status query");
                string muteStatus = GetMute();

                // [OK/NG][Data][x]
                byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)muteStatus[0],
                    (byte)muteStatus[1], // Data
                    0x78 // 'x'
                };
                responseList.AddRange(bytesToAdd);
            } // set power request
            else
            {
                //Console.WriteLine("mute set request");
                string muteData = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                if (SetMute(muteData))
                {
                    string muteStatus = GetMute();
                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        (byte)muteStatus[0],
                        (byte)muteStatus[1],
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                }
                else
                {
                    // wrong data, return error
                    return ErrorResponse(data, "Error:Unsupported mute request");
                }
            }

            return (responseList.ToArray(),"Mute Request");
        } //mute request

        private (byte[],string) VolumeRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            //byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            //Console.WriteLine("volume message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query volume request

            if ((dataStr.Equals("FF")))
            {
                Console.WriteLine("volume status query");

                if (this.volume < 10)
                {
                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    0x30, // padding with 0
                    (byte)this.volume.ToString("X2")[1],
                    0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                }
                else
                {
                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)this.volume.ToString("X2")[0],
                    (byte)this.volume.ToString("X2")[1],
                    0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                }

            } // set volume request
            else
            {
                int volumeData = Convert.ToInt32(dataStr, 16);
                Console.WriteLine("volume set request: " + volumeData);
                if (SetVolume(volumeData))
                {
                    int volumeStatus = GetVolume();

                    if (volumeStatus < 10)
                    {
                        // [OK/NG][Data][x]
                        byte[] bytesToAdd = {
                            0x4F, // "O"
                            0x4B, // "K"
                            0x30, // padding with 0
                            (byte)volumeStatus.ToString("X2")[1],
                            0x78 // 'x'
                        };
                        responseList.AddRange(bytesToAdd);
                    }
                    else
                    {

                        // [OK/NG][Data][x]
                        byte[] bytesToAdd = {
                            0x4F, // "O"
                            0x4B, // "K"
                            (byte)volumeStatus.ToString("X2")[0],
                            (byte)volumeStatus.ToString("X2")[1], // Data
                            0x78 // 'x'
                        };
                        responseList.AddRange(bytesToAdd);
                    }
                }
                else
                {
                    // wrong data, return error
                    return ErrorResponse(data, "Error:Unsupported volume request");
                }
            }
            return (responseList.ToArray(),"Volume Request");
        } //Volume

        private (byte[],string) OSDRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            //Console.WriteLine("OSD message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query OSD request
            
            if ((dataStr.Equals("FF")))
            {
                Console.WriteLine("OSD status query");
                string osdStatus = GetOSD();

                // [OK/NG][Data][x]
                byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)osdStatus[0],
                    (byte)osdStatus[1], // Data
                    0x78 // 'x'
                };
                responseList.AddRange(bytesToAdd);
            } // set osd request
            else
            {
                Console.WriteLine("OSD set request");
                string osdData = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                if (SetOSD(osdData))
                {
                    string osdStatus = GetOSD();
                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        (byte)osdStatus[0],
                        (byte)osdStatus[1], // Data
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                }
                else
                {
                    // wrong data, return error
                    return ErrorResponse(data, "Error:Unsupported OSD request");
                }
            }

            return (responseList.ToArray(),"OSD Request");
        } //OSD (on screen display)
        
        private (byte[],string) InputRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            //Console.WriteLine("input message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query power request
            if ((dataStr.Equals("FF")))
            {
                Console.WriteLine("input query");
                string inputStatus = this.GetInput();

                // [OK/NG][Data][x]
                byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)inputStatus[0],
                    (byte)inputStatus[1], // Data
                    0x78 // 'x'
                };
                responseList.AddRange(bytesToAdd);
            } // set input request
            else
            {
                Console.WriteLine("input set request");
                string inputStatus = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                if (this.SetInput(inputStatus))
                {
                    UpdateInput();

                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        (byte)inputStatus[0],
                        (byte)inputStatus[1], // Data
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);

                }
                else
                {
                    // wrong data, return error
                    Console.WriteLine("Unsupported input set request");
                    return ErrorResponse(data, "Error:Unsupported input request");
                }
            }
            return (responseList.ToArray(),"Input Request");
        } //input request

        private (byte[],string) ResetRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            Console.WriteLine("Reset message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query Reset request
            //Console.WriteLine("dataStr: " + dataStr);
            if (dataStr.Equals("00")) //picture reset
            {
                //Console.WriteLine("nv picture reset request");
                string inputStatus = "90"; //hdmi

                if (this.SetInput(inputStatus))
                {
                    UpdateInput();

                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        (byte)dataStr[0],
                        (byte)dataStr[1], // Data
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                }
                else
                {
                    // wrong data, return error
                    return ErrorResponse(data, "Error:Failed to reset picture");
                }
                
            }
            else if (dataStr.Equals("01")) //factory reset
            {
                SetPower("01"); //power on
                SetInput("90");  //hdmi1
                UpdateInput();
                SetOSD("00");  //OSD off
                SetVolume(0);
                SetMute("01"); //mute off
                SetLock("00");  //unlocked
                // [OK/NG][Data][x]
                byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)dataStr[0],
                    (byte)dataStr[1], // Data
                    0x78 // 'x'
                };
                
                responseList.AddRange(bytesToAdd);
            }   
            else
            {
                // wrong data, return error
                return ErrorResponse(data, "Error:Unsupported reset request");
            }

            return (responseList.ToArray(),"Reset Request");
        } // Reset request

        private (byte[],string) AspectRatioRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command1 = data[0];
            byte command2 = data[1];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space
            Console.WriteLine("aspect ratio message");
            string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            // query aspect ratio request
            if ((dataStr.Equals("FF")))
            {
                Console.WriteLine("aspect ratio query");
                string aspectRatioStatus = GetAspectRatioCode();

                // [OK/NG][Data][x]
                byte[] bytesToAdd = {
                    0x4F, // "O"
                    0x4B, // "K"
                    (byte)aspectRatioStatus[0],
                    (byte)aspectRatioStatus[1], // Data
                    0x78 // 'x'
                };
                responseList.AddRange(bytesToAdd);
            } // set aspect ratio request
            else
            {
                Console.WriteLine("aspect ratio set request");
                string aspectRatioStatus = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                if (this.SetAspectRatio(aspectRatioStatus))
                {
                    UpdateAspectRatio();

                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        (byte)aspectRatioStatus[0],
                        (byte)aspectRatioStatus[1], // Data
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);

                }
                else
                {
                    // wrong data, return error
                    return ErrorResponse(data, "Error:Unsupported aspect ratio request");
                }
            }
            return (responseList.ToArray(),"Aspect Ratio Request");
        } //AspectRatio request

        public override bool SetOPS(string value)
        {
            return false;
        }

        public override void SetOPSName(string value)
        {
            throw new NotImplementedException();
        }

        public override string GetOPS()
        {
            return null;
        }

        public override string[] GetAvailableOPS()
        {
            throw new NotImplementedException();
        }

        public override string GetOPSName(string value)
        {
            return null;
        }

    }
}