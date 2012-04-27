using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Controller
{
    class EventControllerCreator : IEventControllerCreator
    {

        IEventController IEventControllerCreator.createEventController(int type)
        {
            switch (type)
            {
                case 0:
                    return new FoeEventController();
                case 1:
                    return new FriendFoeController();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
