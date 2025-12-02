using System;
using System.Windows.Forms;

namespace TVSimulator
{
    public abstract class AbstractTV
    {
        protected bool power = false;
        protected int tvId = 1;
        protected bool mute = true;
        protected bool OSDLock = false;
        protected bool lockTV = false;
        protected int volume = 0;
        protected string serial = "";

        protected string[] TVModels;

        public abstract string GetPower();
        public abstract Boolean SetPower(string value);
        public abstract void SetTVId(int value);
        public abstract int GetTVId();
        public abstract Boolean SetMute(string value);
        public abstract string GetMute();  
        public abstract Boolean SetOSD(string value);
        public abstract string GetOSD();
        public abstract Boolean SetLock(string value);
        public abstract string GetLock();
        public abstract Boolean SetVolume(int value);
        public abstract int GetVolume();
        public abstract Boolean SetInput(string value);
        public abstract void SetInputName(string value);
        public abstract string GetInput();
        public abstract string[] GetAvailableInputs();
        public abstract string[] GetTVModels();
        public abstract string GetInputName(string value);
        public abstract string GetAspectRatio();
        public abstract Boolean SetAspectRatio(string value);
        //public abstract string GetAspectRatioName(string value);
        public abstract string[] GetAvailableAspectRatio();
        public abstract void SetAspectRatioName(string value);
        public abstract Boolean SetOPS(string value);
        public abstract void SetOPSName(string value);
        public abstract string GetOPS();
        public abstract string[] GetAvailableOPS();
        public abstract string GetOPSName(string value);
        public abstract (byte[], string) processIncoming(byte[] data);
    }
}