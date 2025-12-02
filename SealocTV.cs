using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TVSimulator
{

    public class SealocTV : AbstractTV
    {
        private Button powerButton;
        private Button lockButton;
        private Button muteButton;
        private Button OSDButton; //OnScreenDisplay
        private NumericUpDown volumeUpDown;
        private ComboBox inputCombo;
        private ComboBox ratioCombo;
        private ComboBox OPSCombo;
        private string input = "HDMI1";
        private new bool mute = true;
        

        private byte[] errorAckMsg = {
                0xEF,
                0x59,
                0x13,
                0x01,
                0x00,
            };

        private byte[] OKAckMsg = {
                0xEF,
                0x59,
                0x12,
                0x01,
                0x00,
            };

        private byte[] doneAckMsg = {
                0xEF,
                0x59,
                0x10,
                0x01,
                0x00,
            };

        private byte[] busyAckMsg = {
                0xEF,
                0x59,
                0x11,
                0x01,
                0x00,
            };


        private readonly new string[] TVModels = { "SLX2055-XD" };

        public SealocTV(Button powerButton, Button lockButton, NumericUpDown volumeUpDown, Button muteButton,
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
                case "01":
                    mute = true;
                    UpdateMuteButton();
                    return true;
                case "00":
                    mute = false;
                    UpdateMuteButton();
                    return true;
                default:
                    return false;
            }
            
            
        }
        public override string GetMute()
        {
            return mute ? "01" : "00"; 
        }

        public override Boolean SetLock(string value)
        {
            throw new NotImplementedException();
        }

        public override string GetOSD()
        {
            throw new NotImplementedException();
        }

        public override Boolean SetOSD(string value)
        {
            throw new NotImplementedException();
        }


        public override string GetLock()
        {
            throw new NotImplementedException();
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

        public override Boolean SetInput(string value)
        {
            switch (value)
            {
                case "103": // 0x67
                    input = "HDMI1";
                    UpdateInput();
                    return true;
                case "218": // 0xDA
                    input = "HDMI2";
                    UpdateInput();
                    return true;
                case "219": //0xDB
                    input = "HDMI3";
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
                    input = value;
                    break;
                default:  //this should never happen
                    break;
            }
        }

        //not used
        public override string GetInputName(string value)
        {
            switch (value)
            {
                case "g":  //0x67
                    return "HDMI1";
                case "Ú": //0xDA
                    return "HDMI2";
                case "Û": //0xDB
                    return "HDMI3";
                default:
                    return "Unknown";
            }
        }

        public override string GetInput()
        {
            return input;
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

        public override string[] GetAvailableInputs()
        {
            return new[] { "HDMI1","HDMI2","HDMI3"};
        }

        public override Boolean SetOPS(string value)
        {
            throw new NotImplementedException();
        }

        //this method should only be called by front end when user changed OPS combo box
        public override void SetOPSName(string value)
        {
            throw new NotImplementedException();
        }

        public override string GetOPSName(string value)
        {
            throw new NotImplementedException();
        }

        public override string GetOPS()
        {
            throw new NotImplementedException();
        }

        public override string[] GetAvailableOPS()
        {
            throw new NotImplementedException();
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
                muteButton.Text = "Mute ON";
                muteButton.BackColor = Color.Green;
            }
            else
            {
                muteButton.Text = "Mute OFF";
                muteButton.BackColor = Color.Red;
            }
        }

        private void UpdateOSDButton()
        {
            throw new NotImplementedException();
        }

        private void UpdateLockButton()
        {
            throw new NotImplementedException();
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

        private void UpdateOPSCombo()
        {
            throw new NotImplementedException();
        }

        //this method should only be called by front end when user changes aspect ratio combo box
        public override void SetAspectRatioName(string value)
        {
            throw new NotImplementedException();
        }

        public override string GetAspectRatio()
        {
            throw new NotImplementedException();
        }

        public override bool SetAspectRatio(string value)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAvailableAspectRatio()
        {
            throw new NotImplementedException();
        }

       
        private void UpdateAspectRatio()
        {
            throw new NotImplementedException();
        }

        public override (byte[], string) processIncoming(byte[] request)
        {

            Console.WriteLine("Processing incoming SealocTV message. Length:" + request.Length + " data bytes:" + BitConverter.ToString(request));
            if (request.Length == 4 && request[0] == 0x69)
            {

                try
                {
                    //string dataAsString = Encoding.ASCII.GetString(data);

                    byte data1 = request[1];


                    //byte[] setByte = { request[2]};
                    //string setBytesStr = BitConverter.ToChar(data, 2).ToString();
                    
                    int msgSetId = request[2];
Console.WriteLine("msgSetId:" + msgSetId);

                    if (msgSetId == this.tvId || msgSetId == 0)
                    {
                        Console.WriteLine("nv matching setId");
                        if (data1 == 0xD0 || data1 == 0xD1) 
                        {
                            return PowerRequest(request);
                        }
                        else if (data1 == 0xC2 || data1 == 0xC3) 
                        {
                            return VolumeRequest(request);
                        }
                        else if (data1 == 0xC0 || data1 == 0xC1) 
                        {
                            return MuteRequest(request);
                        }
                        else if (data1 == 0x67 || data1 == 0xDA || data1 == 0xDB) 
                        {
                            return InputRequest(request);
                        }
                        else
                        {
                            // unknown command, return error
                            //Console.WriteLine("unknown command");
                            return (errorAckMsg,"Error:Unknown command");

                        }

                    }
                    else
                    {
                        //Console.WriteLine("mismatching setId");
                        // wrong setId, return error
                        return (errorAckMsg,"Error:Wrong setId");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Message processing error: {ex.Message}");
                }
            }
            
            Console.WriteLine("Unexpected Message Length");
            return (errorAckMsg, "Error:Unexpected Message Length");
        }


        private (byte[], string) PowerRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };
            
            Console.WriteLine("power message");
            //string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
            

            if(data[1] == 0xD0) //power off
            {
                Console.WriteLine("power off request");

                if (SetPower("00"))
                {
                    responseList.AddRange(OKAckMsg);
                }
                
            } else if(data[1] == 0xD1) //power on
            {
                if (SetPower("01"))
                {
                    responseList.AddRange(OKAckMsg);
                }
                
            } else
            {
                return (errorAckMsg,"Error:Unsupported power request");
            }

            return (responseList.ToArray(), "Power Request");
        }

        

        private (byte[], string) MuteRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            byte command2 = data[1];

            if (command2 == 0xC0)  //mute off
            {
                Console.WriteLine("mute off request");
                if(SetMute("00"))
                {
                    responseList.AddRange(OKAckMsg);
                } else {
                    return (errorAckMsg,"Error:Could not set mute off");
                }
            } else if (command2 == 0xC1) //mute on
            {
                Console.WriteLine("mute set on request");
                if (SetMute("01"))
                {

                    responseList.AddRange(OKAckMsg);
                }
                else
                {
                    // wrong data, return error
                    return (errorAckMsg,"Error:Could not set mute on");
                }
            } else {
                return (errorAckMsg,"Error:Unsupported mute request");
            }

            return (responseList.ToArray(), "Mute Request");
        } //mute request

        private (byte[], string) VolumeRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            //byte command1 = data[0];
            byte command2 = data[1];

            //Console.WriteLine("volume message");
            
            if (data[1] == 0xC2)  //volume+
            {
                Console.WriteLine("volume+ ");

                if(SetVolume(GetVolume() + 1))
                {
                    responseList.AddRange(OKAckMsg);
                }else
                {
                    return (errorAckMsg,"Error:Volume out of range");
                }

            } else if (data[1] == 0xC3) //volume-
            {
                Console.WriteLine("volume-");

                if (SetVolume(GetVolume() - 1))
                {
                    responseList.AddRange(OKAckMsg);
                }
                else
                {

                    return (errorAckMsg,"Error:Volume out of range");
                }
            } else
            {
                    // wrong data, return error
                    return (errorAckMsg, "Error:Unsupported volume request");
            }
            
            return (responseList.ToArray(), "Volume Request");
        } //Volume

        

        private (byte[], string) InputRequest(byte[] data)
        {

            List<byte> responseList = new List<byte> { };

            //byte command1 = data[0];
            byte command2 = data[1];

            // query Input request
            if (command2 == 0x67 || command2 == 0xDA || command2 == 0xDB)  
            {
                Console.WriteLine("input request");
                byte[] inputBytes = {0x30, command2 }; //ASCII '0' + input code);
                string inputStatus = command2.ToString(); // System.Text.Encoding.ASCII.GetString(inputBytes);
                Console.WriteLine("input status to set:" + inputStatus);
                if(SetInput(inputStatus))
                {
                    responseList.AddRange(OKAckMsg);
                } else
                {
                    return (errorAckMsg,"Error:Could not set input");
                }
                

            }
            else 
            {
                return (errorAckMsg,"Error:Unsupported input request");
            }
            return (responseList.ToArray(), "Input Request");
        } //input request

        
    }
}