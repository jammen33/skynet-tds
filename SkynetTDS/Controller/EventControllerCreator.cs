using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Controller
{
    interface EventControllerCreator : IEventControllerCreator
    {
        IEventController createEventController(string type)
        {
            switch (type)
            {

            }

        }
    }
}
