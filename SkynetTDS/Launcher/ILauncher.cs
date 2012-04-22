using System;

namespace SkynetTDS.Launchers
{
    /// <summary>
    /// Interface for a missile launcher.
    /// </summary>
    public interface ILauncher
    {        
        /// <summary>
        /// Fires a number of missiles.
        /// </summary>
        /// <param name="numberOfMissiles">Number of missiles to fire.</param>
        void Fire(int numberOfMissiles);
        /// <summary>
        /// Moves the position of the rocket launcher to a position.
        /// </summary>
        /// <param name="x">Lateral position</param>
        /// <param name="y">Longitudinal position</param>
        /// <param name="speed">Speed to move the launcher</param>
        void MoveRelative(int theta, int phi, int speed);
        /// <summary>
        /// Position of the launcher to move to.
        /// </summary>
        /// <param name="x">Position to move to.</param>
        /// <param name="y">Position to move to.</param>
        /// <param name="speed">Speed to move launcher while moving to location</param>
        void MoveAbsolute(int theta, int phi, int speed);
        /// <summary>
        /// Calibrates the missile launcher.
        /// </summary>
        void Calibrate();
        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        #region Events
        /// <summary>
        /// Fired when the position changes.
        /// </summary>
        event EventHandler<PositionEventArgs> PositionChanged;
        /// <summary>
        /// Fired when a limit is reached.
        /// </summary>
        event EventHandler LimitReached;
        /// <summary>
        /// Fired when a missile is fired.
        /// </summary>
        event EventHandler MissileFired;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        int Theta
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        int Phi
        {
            get;
            set;
        }
        #endregion
    }
}
