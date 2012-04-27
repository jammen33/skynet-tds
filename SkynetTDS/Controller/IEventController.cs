using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Controller
{
    public interface IEventController
    {
        void startEvent();
        void stopEvent();
        void emergencyStop();
    }
}
