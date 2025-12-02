using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// This is just a sample TV with minimal functionality
namespace TVSimulator
{
    public class Zenith : AbstractTV
    {
        private Button powerButton;
        private Button lockButton;
        private Button muteButton;
        private Button OSDButton;
        private NumericUpDown volumeUpDown;
        private ComboBox inputCombo;
        private ComboBox ratioCombo;
        private ComboBox OPSCombo;
        private string input = "Coax";
        //private string aspectRatio = "00";

        private readonly new string[] TVModels = { "Z50UHD" };

        public Zenith(Button powerButton, Button lockButton, NumericUpDown volumeUpDown, Button muteButton,
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
            throw new NotImplementedException();
        }

        public override string GetMute()
        {
            throw new NotImplementedException();
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
            if(value >= 0 && value <= 100)
            {
                volume = value;
                return true;
            }
            return false;
        }

        public override int GetVolume()
        {
            return volume;
        }

        public override bool SetInput(string value)
        {
            return false;
        }

        public override void SetInputName(string value)
        {
            //do nothing
        }

        public override string GetInputName(string value)
        {
            return "Coax";
        }

        public override string GetInput()
        {
            return input;
        }

        public override string[] GetAvailableInputs()
        {
            return new[] { "Coax" };
        }

        public override string[] GetTVModels()
        {
            return TVModels;
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

        public override void SetAspectRatioName(string value)
        {
            throw new NotImplementedException();
        }

        public override (byte[], string) processIncoming(byte[] data)
        {
            // Basic implementation - returns empty response
            return (new byte[0], "Error: Zenith protocol not implemented");
        }

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
            throw new NotImplementedException();
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