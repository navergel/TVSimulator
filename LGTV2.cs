using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TVSimulator
{

    public class LGTV2 : AbstractTV
    {
        private Button powerButton;
        private Button lockButton;
        private Button muteButton;
        private Button OSDButton; //OnScreenDisplay
        private NumericUpDown volumeUpDown;
        private ComboBox inputCombo;
        private ComboBox ratioCombo;
        private ComboBox OPSCombo;
        private string input = "AV"; //set default input to AV
        private string OPS = "00"; //disabled  OpenPluggableSpecification
        private new bool mute = true;
        private string AspectRatio = "Full Screen";

        private readonly new string[] TVModels = { "43SH7E","49SH7E","55SH7E","32SM5E","43SM5E","49SM5E","55SM5E",
                                                   "32SM5KE","43SM5KE","49SM5KE","55SM5KE" };

        public LGTV2(Button powerButton, Button lockButton, NumericUpDown volumeUpDown, Button muteButton,
                    Button OSDButton, ComboBox inputCombo, ComboBox ratioCombo, ComboBox OPSCombo)
        {
            this.powerButton = powerButton;
            this.lockButton = lockButton;
            //UpdateLockButton();
            this.volumeUpDown = volumeUpDown;
            this.muteButton = muteButton;
            //UpdateMuteButton();
            this.OSDButton = OSDButton;
            //UpdateOSDButton();
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

        public override Boolean SetInput(string value)
        {
            switch (value)
            {
                case "20": //AV
                    input = "AV";
                    UpdateInput();
                    return true;
                case "40": //Component
                    input = "Component";
                    UpdateInput();
                    return true;
                case "60": //RGB
                    input = "RGB";
                    UpdateInput();
                    return true;
                case "70": //DVI-D (PC)
                    input = "DVI-D (PC)";
                    UpdateInput();
                    return true;
                case "80": //DVI-D (DTV)
                    input = "DVI-D (DTV)";
                    UpdateInput();
                    return true;
                case "90": //HDMI1 (DTV)
                    input = "HDMI1 (DTV)";
                    UpdateInput();
                    return true;
                case "A0": //HDMI1 (PC)
                    input = "HDMI1 (PC)";
                    UpdateInput();
                    return true;
                case "91": //HDMI2 (DTV)
                    input = "HDMI2 (DTV)";
                    UpdateInput();
                    return true;
                case "A1": //HDMI2 (PC)
                    input = "HDMI2 (PC)";
                    UpdateInput();
                    return true;
                case "92": //OPS/HDMI3DVI-D (DTV)
                    input = "OPS/HDMI3DVI-D (DTV)";
                    UpdateInput();
                    return true;
                case "A2": //OPS/HDMI3DVI-D (PC)
                    input = "OPS/HDMI3DVI-D (PC)";
                    UpdateInput();
                    return true;
                case "95": //OPS/DVI-D (DTV)
                    input = "OPS/DVI-D (DTV)";
                    UpdateInput();
                    return true;
                case "A5": //OPS/DVI-D (PC)
                    input = "OPS/DVI-D (PC)";
                    UpdateInput();
                    return true;
                case "96": //HDMI3/DVI-D (DTV)
                    input = "HDMI3/DVI-D (DTV)";
                    UpdateInput();
                    return true;
                case "A6": //HDMI3/DVI-D (PC)
                    input = "HDMI3/DVI-D (PC)";
                    UpdateInput();
                    return true;
                case "97": //HDMI3/HDMI2/DVI-D (DTV)
                    input = "HDMI3/HDMI2/DVI-D (DTV)";
                    UpdateInput();
                    return true;
                case "A7": //HDMI3/HDMI2/DVI-D (PC)
                    input = "HDMI3/HDMI2/DVI-D (PC)";
                    UpdateInput();
                    return true;
                case "98": //OPS (DTV)
                    input = "OPS (DTV)";
                    UpdateInput();
                    return true;
                case "A8": //OPS (PC)
                    input = "OPS (PC)";
                    UpdateInput();
                    return true;
                case "99": //HDMI3/HDMI2 (DTV)
                    input = "HDMI3/HDMI2 (DTV)";
                    UpdateInput();
                    return true;
                case "A9": //HDMI3/HDMI2 (PC)
                    input = "HDMI3/HDMI2 (PC)";
                    UpdateInput();
                    return true;
                case "C0": //DisplayPort (DTV)
                    input = "DisplayPort (DTV)";
                    UpdateInput();
                    return true;
                case "D0": //DisplayPort (PC)
                    input = "DisplayPort (PC)";
                    UpdateInput();
                    return true;
                case "C1": //DisplayPort/USB-C (DTV)
                    input = "DisplayPort/USB-C (DTV)";
                    UpdateInput();
                    return true;
                case "D1": //DisplayPort/USB-C (PC)
                    input = "DisplayPort/USB-C (PC)";
                    UpdateInput();
                    return true;
                case "C2": //HDMI3 (DTV)
                    input = "HDMI3 (DTV)";
                    UpdateInput();
                    return true;
                case "D2": //HDMI3 (PC)
                    input = "HDMI3 (PC)";
                    UpdateInput();
                    return true;
                case "C3": //HDBaseT (DTV)
                    input = "HDBaseT (DTV)";
                    UpdateInput();
                    return true;
                case "D3": //HDBaseT (PC)
                    input = "HDBaseT (PC)";
                    UpdateInput();
                    return true;
                case "E0": //SuperSign webOS Player
                    input = "SuperSign webOS Player";
                    UpdateInput();
                    return true;
                case "E1": //Others
                    input = "Others";
                    UpdateInput();
                    return true;
                case "E2": //Multi Screen
                    input = "Multi Screen";
                    UpdateInput();
                    return true;
                case "E3": //Player via URL
                    input = "Player via URL";
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
                case "AV": 
                case "Component":
                case "RGB":
                case "DVI-D (PC)":
                case "DVI-D (DTV)":
                case "HDMI1 (DTV)":
                case "HDMI1 (PC)":
                case "HDMI2 (DTV)":
                case "HDMI2 (PC)":
                case "OPS/HDMI3DVI-D (DTV)":
                case "OPS/HDMI3DVI-D (PC)":
                case "OPS/DVI-D (DTV)":
                case "OPS/DVI-D (PC)":
                case "HDMI3/DVI-D (DTV)":
                case "HDMI3/DVI-D (PC)":
                case "HDMI3/HDMI2/DVI-D (DTV)":
                case "HDMI3/HDMI2/DVI-D (PC)":
                case "OPS (DTV)":
                case "OPS (PC)":
                case "HDMI3/HDMI2 (DTV)":
                case "HDMI3/HDMI2 (PC)":
                case "DisplayPort (DTV)":
                case "DisplayPort (PC)":
                case "DisplayPort/USB-C (DTV)":
                case "DisplayPort/USB-C (PC)":
                case "HDMI3 (DTV)":
                case "HDMI3 (PC)":
                case "HDBaseT (DTV)":
                case "HDBaseT (PC)":
                case "SuperSign webOS Player":
                case "Others":
                case "Multi Screen":
                case "Player via URL":
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
                case "20":
                    return "AV";
                case "40":
                    return "Component";
                case "60":
                    return "RGB";
                case "70":
                    return "DVI-D (PC)";
                case "80":
                    return "DVI-D (DTV)";
                case "90":
                    return "HDMI1 (DTV)";
                case "A0":
                    return "HDMI1 (PC)";
                case "91":
                    return "HDMI2 (DTV)";
                case "A1":
                    return "HDMI2 (PC)";
                case "92":
                    return "OPS/HDMI3/DVI-D (DTV)";
                case "A2":
                    return "OPS/HDMI3/DVI-D (PC)";
                case "95":
                    return "OPS/DVI-D (DTV)";
                case "A5":
                    return "OPS/DVI-D (PC)";
                case "96":
                    return "HDMI3/DVI-D (DTV)";
                case "A6":
                    return "HDMI3/DVI-D (PC)";
                case "97":
                    return "HDMI3/HDMI2/DVI-D (DTV)";
                case "A7":
                    return "HDMI3/HDMI2/DVI-D (PC)";
                case "98":
                    return "OPS (DTV)";
                case "A8":
                    return "OPS (PC)";
                case "99":
                    return "HDMI2/OPS (DTV)";
                case "A9":
                    return "HDMI2/OPS (PC)";
                case "C0":
                    return "DISPLAYPORT (DTV)";
                case "D0":
                    return "DISPLAYPORT (PC)";
                case "C1":
                    return "DISPLAYPORT/USB-C (DTV)";
                case "D1":
                    return "DISPLAYPORT/USB-C (PC)";
                case "C2":
                    return "HDMI3 (DTV)";
                case "D2":
                    return "HDMI3 (PC)";
                case "C3":
                    return "HDBaseT (DTV)";
                case "D3":
                    return "HDBaseT (PC)";
                case "E0":
                    return "SuperSign webOS Player";
                case "E1":
                    return "Others";
                case "E2":
                    return "Multi Screen";
                case "E3":
                    return "Play via URL";
                default:
                    return "Unknown";
            }
        }

        public override string GetInput()
        {
            return input;
        }

        private string GetInputNameCode()
        {
            switch (input)
            {
                case "AV":
                    return "20";
                case "Component":
                    return "40";
                case "RGB":
                    return "60";
                case "DVI-D (PC)":
                    return "70";
                case "DVI-D (DTV)":
                    return "80";
                case "HDMI1 (DTV)":
                    return "90";
                case "HDMI1 (PC)":
                    return "A0";
                case "HDMI2 (DTV)":
                    return "91";
                case "HDMI2 (PC)":
                    return "A1";
                case "OPS/HDMI3/DVI-D (DTV)":
                    return "92";
                case "OPS/HDMI3/DVI-D (PC)":
                    return "A2";
                case "OPS/DVI-D (DTV)":
                    return "95";
                case "OPS/DVI-D (PC)":
                    return "A5";
                case "HDMI3/DVI-D (DTV)":
                    return "96";
                case "HDMI3/DVI-D (PC)":
                    return "A6";
                case "HDMI3/HDMI2/DVI-D (DTV)":
                    return "97";
                case "HDMI3/HDMI2/DVI-D (PC)":
                    return "A7";
                case "OPS (DTV)":
                    return "98";
                case "OPS (PC)":
                    return "A8";
                case "HDMI2/OPS (DTV)":
                    return "99";
                case "HDMI2/OPS (PC)":
                    return "A9";
                case "DISPLAYPORT (DTV)":
                    return "C0";
                case "DISPLAYPORT (PC)":
                    return "D0";
                case "DISPLAYPORT/USB-C (DTV)":
                    return "C1";
                case "DISPLAYPORT/USB-C (PC)":
                    return "D1";
                case "HDMI3 (DTV)":
                    return "C2";
                case "HDMI3 (PC)":
                    return "D2";
                case "HDBaseT (DTV)":
                    return "C3";
                case "HDBaseT (PC)":
                    return "D3";
                case "SuperSign webOS Player":
                    return "E0";
                case "Others":
                    return "E1";
                case "Multi Screen":
                    return "E2";
                case "Play via URL":
                    return "E3";
                default:
                    return "Unknown";
            }
        }

        public override string[] GetAvailableInputs()
        {
            return new[] { "AV","COMPONENT","RGB","DVI-D (PC)","DVI-D (DTV)","HDMI1 (DTV)","HDMI1 (PC)","HDMI2 (DTV)","HDMI2 (PC)",
              "OPS/HDMI3/DVI-D (DTV)","OPS/HDMI3/DVI-D (PC)","OPS/DVI-D (DTV)","OPS/DVI-D (PC)","HDMI3/DVI-D (DTV)","HDMI3/DVI-D (PC)",
              "HDMI3/HDMI2/DVI-D (DTV)","HDMI3/HDMI2/DVI-D (PC)","OPS (DTV)","OPS (PC)","HDMI2/OPS (DTV)","HDMI2/OPS (PC)","DISPLAYPORT (DTV)",
              "DISPLAYPORT (PC)","DISPLAYPORT/USB-C (DTV)","DISPLAYPORT/USB-C (PC)","HDMI3 (DTV)","HDMI3 (PC)","HDBaseT (DTV)","HDBaseT (PC)",
              "SuperSign webOS Player","Others","Multi Screen","Play via URL" };
        }

        public override Boolean SetOPS(string value)
        {
            switch (value)
            {
                case "00": //Disable
                case "01": //Sync-On
                case "02": //Sync-On/Off
                    OPS = value;
                    return true;
                default:
                    return false;
            }
        }

        //this method should only be called by front end when user changed OPS combo box
        public override void SetOPSName(string value)
        {
            switch (value)
            {
                case "Disable":
                    OPS = "00";
                    break;
                case "Sync-On":
                    OPS = "01";
                    break;
                case "Sync-On/Off":
                    OPS = "02";
                    break;
                default:  //this should never happen
                    break;
            }
        }

        public override string GetOPSName(string value)
        {
            switch (value)
            {
                case "00":
                    return "Disable";
                case "01":
                    return "Sync-On";
                case "02":
                    return "Sync-On/Off";
                default:
                    return "Unknown";
            }
        }

        public override string GetOPS()
        {
            return OPS;
        }

        public override string[] GetAvailableOPS()
        {
            return new[] { "Disable", "Sync-On", "Sync-On/Off" };
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

        private void UpdateOPSCombo()
        {
            if (OPSCombo.InvokeRequired)
            {
                OPSCombo.Invoke(new Action(UpdateOPSCombo));
                return;
            }

            OPSCombo.SelectedItem = GetOPSName(OPS);
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
                case "Full Screen":
                case "Original":
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

        //this method is called when processing incoming message
        public override bool SetAspectRatio(string value)
        {
            switch (value)
            {
                case "02": //Full Screen
                    AspectRatio = "Full Screen";
                    UpdateAspectRatio();
                    return true;
                case "06": //Original
                    AspectRatio = "Original";
                    UpdateAspectRatio();
                    return true;
                default:
                    return false;
            }
        }

        //this method is called when processing query message
        private string GetAspectRatioCode()
        {
            switch (AspectRatio)
            {
                case "Full Screen":
                    return "02";
                case "Original":
                    return "06";
                default:
                    return "00"; //unknown
            }
        }

        public override string[] GetAvailableAspectRatio()
        {
            return new[] { "Full Screen", "Original"};
        }

        /*public override string GetAspectRatioName(string value)
        {
            switch (value)
            {
                case "02":
                    return "Full Screen";
                case "06":
                    return "Original";
                default:
                    return "Unkown";
            }
        }*/

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

            if (data.Length == 9) 
            {
                if( data[8] != 0x0D)
                {
                    return ErrorResponse(data, "Error:Invalid message termination");
                }

                try
                {
                    string dataAsString = Encoding.ASCII.GetString(data);

                    byte command1 = data[0];
                    byte command2 = data[1];



                    /*byte[] setBytes = { data[3], data[4] };
                    string setBytesStr = Encoding.ASCII.GetString(setBytes);
                    int msgSetId = int.Parse(setBytesStr);

                    if (msgSetId == this.tvId || msgSetId == 0)
                    {*/
                     //   Console.WriteLine("nv processIncoming()dataLength9: matching setId");
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
                        else if (command1 == 0x6B && command2 == 0x63) //kc
                        {
                            return AspectRatioRequest(data);
                        }
                        else if (command1 == 0x66 && command2 == 0x79) //fy
                        {
                            return GetSerialNumber(data);
                        }
                        else
                        {
                            // unknown command, return error
                            //Console.WriteLine("unknown command");
                            return ErrorResponse(data, "Error:Unknown command");

                        }

                    /*}
                    else
                    {
                        Console.WriteLine("processIncoming()dataLength9: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                        // wrong setId, return error
                        return ErrorResponse(data, "Error:Wrong setId");
                    }*/

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Message processing error: {ex.Message}");
                }
            }
            else if (data.Length == 12) 
            {
                if(data[11] != 0x0D)
                {
                    return ErrorResponse(data, "Error:Invalid message termination");
                }

                // extended command
                Console.WriteLine("extended command");
                try
                {
                    string dataAsString = Encoding.ASCII.GetString(data);
                    //(s)(n)( )(tvID[0])(tvID[1])( )(8)(b)( )(data[0])(data[1])(Cr)
                    byte command1 = data[0];
                    byte command2 = data[1];
                    byte command3 = data[6];
                    byte command4 = data[7];



                    byte[] setBytes = { data[3], data[4] };
                    string setBytesStr = Encoding.ASCII.GetString(setBytes);
                    int msgSetId = int.Parse(setBytesStr);

                    /*if (msgSetId == this.tvId || msgSetId == 0)
                    {
                        Console.WriteLine("nv processIncoming()dataLength12: matching setId"); */
                        if (command1 == 0x73 && command2 == 0x6E && command3 == 0x38 && command4 == 0x62) //s,n,8b
                        {
                            return OPSRequest(data);
                        }
                        else
                        {
                            // unknown command, return error
                            //Console.WriteLine("unknown command");
                            return ErrorResponse(data, "Error:Unknown extended command");

                        }

                    /*}
                    else
                    {
                        Console.WriteLine("processIncoming()dataLength12: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                        // wrong setId, return error
                        return ErrorResponse(data, "Error:Wrong setId");
                    } */

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Message processing error: {ex.Message}");
                }
                Console.WriteLine("Extended command failure");


            }
            Console.WriteLine("Unexpected Message Length");
            return ErrorResponse(data, "Error:Unexpected Message Length");
        }



        private static (byte[], string) ErrorResponse(byte[] data, string errorText)
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

            return (errorAckMsg, errorText);

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
            byte[] setBytes = { data[3], data[4] };
                    string setBytesStr = Encoding.ASCII.GetString(setBytes);
                    int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
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
            } else {
				Console.WriteLine("PowerRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "Power Request Error:Wrong setId");
			}

            return (responseList.ToArray(), "Power Request");
        }

        private (byte[], string) LockRequest(byte[] data)
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
            
            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
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
            } else
            {
                Console.WriteLine("LockRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "LockRequest-Error:Wrong setId");
            }

            return (responseList.ToArray(), "Lock Request");
        } //LockRequest

        private (byte[], string) MuteRequest(byte[] data)
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
            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
                string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                // query mute request
                //Console.WriteLine("dataStr: " + dataStr);
                if ((dataStr.Equals("FF")))
                {
                    Console.WriteLine("mute status query");
                    string muteStatus = this.GetMute();

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
                    Console.WriteLine("mute set request");
                    string muteData = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                    if (SetMute(muteData))
                    {

                        string MuteStatus = this.GetMute();
                        // [OK/NG][Data][x]
                        byte[] bytesToAdd = {
                            0x4F, // "O"
                            0x4B, // "K"
                            (byte)MuteStatus[0],
                            (byte)MuteStatus[1],
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
            } else
            {
                Console.WriteLine("MuteRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "MuteRequest-Error:Wrong setId"); 
            }

            return (responseList.ToArray(), "Mute Request");
        } //mute request

        private (byte[], string) VolumeRequest(byte[] data)
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
            
            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
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
            } else
            {
                Console.WriteLine("VolumeRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "VolumeRequest-Error:Wrong setId");
            }
            return (responseList.ToArray(), "Volume Request");
        } //Volume

        private (byte[], string) OSDRequest(byte[] data)
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

            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
                string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                // query OSD request

                if ((dataStr.Equals("FF")))
                {
                    Console.WriteLine("OSD status query");
                    string osdStatus = this.GetOSD();

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
                    Console.WriteLine("osd set request");
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
            } else
            {
                Console.WriteLine("OSDRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "OSDRequest-Error:Wrong setId");
            }

            return (responseList.ToArray(), "OSD Request");
        } //OSD (on screen display)

        private (byte[], string) InputRequest(byte[] data)
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

            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
                string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                // query Input request
                if ((dataStr.Equals("FF")))
                {
                    Console.WriteLine("input query");
                    string inputStatus = GetInputNameCode();

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
                    string inputData = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                    if (SetInput(inputData))
                    {
                        string inputStatus = GetInputNameCode();
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
                        return ErrorResponse(data, "Error:Unsupported input request");
                    }
                }
            } else
            {
                Console.WriteLine("InputRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "InputRequest-Error:Wrong setId");
            }
            return (responseList.ToArray(), "Input Request");
        } //input request

        private (byte[], string) ResetRequest(byte[] data)
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

            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
                string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                // query Reset request
                //Console.WriteLine("dataStr: " + dataStr);
                if (dataStr.Equals("00")) //picture reset
                {
                    //Console.WriteLine("nv picture reset request");
                    string inputStatus = "90"; //hdmi

                    if (SetInput(inputStatus))
                    {
                    
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
                    //Console.WriteLine("nv factory reset request");

                    SetPower("01"); //power on
                    SetInput("90");  //hdmi1
                    SetOSD("00");  //OSD off
                    SetVolume(0);
                    SetMute("01"); //mute off
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
            } else
            {
                Console.WriteLine("ResetRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "ResetRequest-Error:Wrong setId"); 
            }

            return (responseList.ToArray(), "Reset Request");
        } // Reset request

        private (byte[], string) AspectRatioRequest(byte[] data)
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
            
            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
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
                    string aspectData = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                    if (this.SetAspectRatio(aspectData))
                    {
                        UpdateAspectRatio(); //update front end
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

                    }
                    else
                    {
                        // wrong data, return error
                        return ErrorResponse(data, "Error:Unsupported aspect ratio request");
                    }
                }
            } else
            {
                Console.WriteLine("AspectRatioRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "AspectRatioRequest-Error:Wrong setId");
            }
            return (responseList.ToArray(), "Aspect Ratio Request");
        } //AspectRatio request



        private (byte[], string) GetSerialNumber(byte[] data)
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

            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
                string dataStr = System.Text.Encoding.ASCII.GetString(data, 6, 2);
                // query serial number request
                if (dataStr.Equals("FF"))
                {
                    Console.WriteLine("get serialnumber query");

                    //TODO: not sure exactly how many bytes or sample of a TV SerialNumber, just send nv for now
                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        0x6E, // "n" (Data)
                        0x76, // "v" (Data)
                        0x74, // "t" (Data)
                        0x76, // "v" (Data)
                        0x30, // "0" (Data)
                        0x30, // "0" (Data)
                        0x31, // "1" (Data)
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                } 
                else
                {
                    Console.WriteLine("invalid serialnumber request");

                    // wrong data, return error
                    return ErrorResponse(data, "Error:Unsupported SerialNumber request");

                }
            } else
            {
                Console.WriteLine("SerialNumberRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "SerialNumberRequest-Error:Wrong setId");
            }
            return (responseList.ToArray(), "SerialNumber Request");
        }
        
        private (byte[], string) OPSRequest(byte[] data)
        {
            //OPS is used by player to determine what code to set the input to. I've seen PC player disable OPS before setting input.
            List<byte> responseList = new List<byte> { };

            //(s)(n)( )(tvID[0])(tvID[1])( )(8)(b)( )(data[0])(data[1])(Cr)

            byte command1 = data[0];
            byte command2 = data[1];
            byte command3 = data[6];
            byte command4 = data[7];

            // Response format: [command2][ ][SetID][OK/NG][Data][x]
            responseList.Add(command2);
            responseList.Add(0x20); //space

            //add setId 
            responseList.Add(data[3]);
            responseList.Add(data[4]);
            responseList.Add(0x20); //space

            byte[] setBytes = { data[3], data[4] };
            string setBytesStr = Encoding.ASCII.GetString(setBytes);
            int msgSetId = int.Parse(setBytesStr);

            if (msgSetId == this.tvId || msgSetId == 0)
            {
                string dataStr = System.Text.Encoding.ASCII.GetString(data, 9, 2);
                //Console.WriteLine("OPS message data: " + dataStr );
                // query OPS request
                if ((dataStr.Equals("FF")))
                {
                    Console.WriteLine("get OPS query");
                    string OPSStatus = this.GetOPS();


                    // [OK/NG][Data][x]
                    byte[] bytesToAdd = {
                        0x4F, // "O"
                        0x4B, // "K"
                        command3,  //8
                        command4,  //b
                        (byte)OPSStatus[0],
                        (byte)OPSStatus[1], 
                        0x78 // 'x'
                    };
                    responseList.AddRange(bytesToAdd);
                } // set OPS request. Valid values: 00 - disabled, 01 - sync(On), 02 - sync(On/Off)
                else
                {
                    Console.WriteLine("set OPS request");
                    string OPSStatus = System.Text.Encoding.ASCII.GetString(data, 9, 2);
                    if (this.SetOPS(OPSStatus))
                    {
                        // [OK/NG][command3][command4][Data][x]
                        byte[] bytesToAdd = {
                            0x4F, // "O"
                            0x4B, // "K"
                            command3, // "8"
                            command4, // "b"
                            data[9],  //  (Data)
                            data[10], //  (Data)
                            0x78 // 'x'
                        };
                        responseList.AddRange(bytesToAdd);
                        UpdateOPSCombo();
                    }
                    else
                    {
                        // failed to set OPS, return error
                        // [OK/NG][command3][command4][Data][x]
                        byte[] bytesToAdd = {
                            0x4E, // "N"
                            0x47, // "G"
                            command3, // "8"
                            command4, // "b"
                            data[9],  //  (Data)
                            data[10], //  (Data)
                            0x78 // 'x'
                        };
                        responseList.AddRange(bytesToAdd);
                        return (responseList.ToArray(), "Error:OPS request");
                    }
                }

            } else
            {
                Console.WriteLine("OPSRequest: mismatching reqSetId:"+msgSetId+" setId:"+this.tvId);
                // wrong setId, return error
                return ErrorResponse(data, "OPSRequest-Error:Wrong setId");
            }
            
            return (responseList.ToArray(), "OPS Request");
        }
    }
}