using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Controller
{
    class EventControllerCreator : IEventControllerCreator
    {

        IEventController IEventControllerCreator.createEventController(string type)
        {
            switch (type)
            {
                case "FoeEventController":
                    return new FoeEventController();
                case "FriendFoeEventController":
                    return new FriendFoeController();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
