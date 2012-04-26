using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Vision
{
    interface IImageProcessorCreator
    {
        public IImageProcessor createProcessor(string type);
    }
}