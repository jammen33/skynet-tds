using System;

namespace SkynetTDS.Launchers
{
    /// <summary>
    /// Holds information about the position of the missile launcher.
    /// </summary>
    public class PositionEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">Position of the launcher.</param>
        /// <param name="y">Position of the launcher.</param>
        public PositionEventArgs(int x, int y)
        {
            X = x;
            Y = y; 
        }

        #region Properties
        /// <summary>
        /// Gets the X-position of the launcher.
        /// </summary>
        public int X
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the Y-position of the launcher.
        /// </summary>
        public int Y
        {
            get;
            private set;
        }
        #endregion
    }
}
