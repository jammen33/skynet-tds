using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildDefender;

namespace SkynetTDS.Launchers
{
    public class LauncherController : ILauncher
    {
        #region Attributes
        int trackx;
        int tracky;
        int maxNumberOfMissiles;
        MissileLauncher launcher;
        #endregion

        public LauncherController()
        {
            //Integers to translate the relative coordinate movements to absolute coordinates
            trackx = 0;
            tracky = 0;
            Name = "Gargamel";

            //set number of missiles
            maxNumberOfMissiles = 4;

            //Initialize launcher and reset the position to the base coordinates
            launcher = new MissileLauncher();
        }

        ~LauncherController()
        {
            launcher.command_switchLED(false);
        }

        #region Functions
        //If trackx or tracky have gone out of bounds, set them to bounds
        private void track_bind_launcher()
        {
            if (trackx > 2750)
            {
                trackx = 2750;
                onLimitReached(new EventArgs());
            }
            else if (trackx < -2750)
            {
                trackx = -2750;
                onLimitReached(new EventArgs());
            }
            if (tracky > 500)
            {
                tracky = 500;
                onLimitReached(new EventArgs());
            }
            if (tracky < -250)
                tracky = -250;
        }

        #region Launcher Commands
        //Fires the missile launcher
        public void Fire(int number_of_missiles)
        {
            for (int i = 0; i < number_of_missiles; i++)
            {
                launcher.command_Fire();
                onMissileFired(new EventArgs());
            }
        }

        //Move launcher relative to a fixed ZERO, in this case the launcher's Reset() position
        public void MoveAbsolute(int x, int y, int speed)
        {
            x = x - trackx;
            y = y - tracky;
            MoveRelative(x, y, 0);
        }

        //Move launcher relative to current position
        public void MoveRelative(int x, int y, int speed)
        {
            //Update the x and y absolute coordinates
            trackx += x;
            tracky += y;
            track_bind_launcher();

            //Move launcher by x and y
            if (x <= 0)
                launcher.command_Left(Math.Abs(x));
            else if (x > 0)
                launcher.command_Right(Math.Abs(x));
            if (y <= 0)
                launcher.command_Down(Math.Abs(y));
            else if (y > 0)
                launcher.command_Up(Math.Abs(y));

            onPositionChanged(new PositionEventArgs(trackx, tracky));
        }

        //Move the launcher back to center based on the trackx and tracky values
        public void soft_reset_position()
        {
            MoveRelative(-trackx, -tracky, 0);
        }

        //Use the DreamCheeky launcher defined reset
        public void Calibrate()
        {
            launcher.command_reset();
        }
        #endregion
        #endregion

        #region Events
        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler LimitReached;
        public event EventHandler MissileFired;
        private void onPositionChanged(PositionEventArgs e)
        {
            EventHandler<PositionEventArgs> positionChanged = PositionChanged;
            if (positionChanged != null)
            {
                positionChanged(this, e);
            }
        }
        private void onLimitReached(EventArgs e)
        {
            EventHandler limitReached = LimitReached;
            if (limitReached != null)
            {
                limitReached(this, e);
            }
        }
        private void onMissileFired(EventArgs e)
        {
            EventHandler missileFired = MissileFired;
            if (missileFired != null)
            {
                missileFired(this, e);
            }
        }
        #endregion

        #region IAttributes
        public string Name
        {
            get;
            set;

        }



        public int Theta
        {
            get
            {
                return trackx;
            }
            set
            {
                trackx = Theta;
            }
        }
        public int Phi
        {
            get
            {
                return tracky;
            }
            set
            {
                tracky = Phi;
            }
        }
        #endregion
    }
}
