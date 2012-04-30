using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Controller
{
    class FoeEventController : IEventController
    {
        public FoeEventController()
        {
            System.Windows.Forms.MessageBox.Show("Hello, World! This is FoeEventController!");
        }
        public void startEvent()
        {
            throw new NotImplementedException();
        }


        public void stopEvent()
        {
            throw new NotImplementedException();
        }

        public void emergencyStop()
        {
            throw new NotImplementedException();
        }


        public event EventHandler MissileFired;


        public event EventHandler<FoundTatgetEventArgs> FoundTargets;


        public void calibrateLauncher()
        {
            throw new NotImplementedException();
        }
    }
}
