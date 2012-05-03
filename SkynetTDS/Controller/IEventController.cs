using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkynetTDS.Controller
{
    public interface IEventController
    {
        /// <summary>
        /// starts the event loop
        /// </summary>
        void startEvent();

        /// <summary>
        /// Ask the event to stop
        /// </summary>
        void stopEvent();

        /// <summary>
        /// Terminates the event immediately
        /// </summary>
        void emergencyStop();

        /// <summary>
        /// Calibrates the missile launcher
        /// </summary>
        void calibrateLauncher();

        /// <summary>
        /// Fires all remaining Missiles and stops the event.
        /// </summary>
        void timesAlmostUp();

        /// <summary>
        /// the number of missiles
        /// </summary>
        int numberOfMissiles
        {
            get;
        }

        /// <summary>
        /// Fired when a missile is fired
        /// </summary>
        event EventHandler MissileFired;

        /// <summary>
        /// Fired when targers have been discovered
        /// </summary>
        event EventHandler<FoundTatgetEventArgs> FoundTargets;

        /// <summary>
        /// fired when out of missiles
        /// </summary>
        event EventHandler OutOfMissiles;

        /// <summary>
        /// Fired when the event terminates its self
        /// </summary>
        event EventHandler EventTerminated;
    }
}
