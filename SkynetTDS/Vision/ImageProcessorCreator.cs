using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Vision
{
    class ImageProcessorCreator : IImageProcessorCreator
    {
        public IImageProcessor createProcessor(string type)
        {
            switch (type)
            {
                case "foe":
                    return new FoeImageProcessor();
                case "friendFoe":
                    return new FriendFoeImageProcessor();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
