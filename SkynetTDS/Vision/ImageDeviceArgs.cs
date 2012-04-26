using System;
using System.Drawing;

namespace SkynetTDS.Vision
{
    /// <summary>
    /// Image Device Arguments.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImageDeviceArgs: EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageDeviceArgs(Image image)
        {
            Frame       = image;
            Timestamp   = DateTime.Now;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageDeviceArgs(Image image, DateTime time)
        {
            Frame       = image;
            Timestamp   = time;
        }

        #region Properties
        /// <summary>
        /// Gets the image from the device.
        /// </summary>
        public Image Frame
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the time the image was taken.
        /// </summary>
        public DateTime Timestamp
        {
            get;
            private set;
        }
        #endregion
    }
}
