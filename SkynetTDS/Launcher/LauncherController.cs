using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildDefender;

namespace MissileControl
{
    public class LauncherController
    {
        int trackx;
        int tracky;
        MissileLauncher launcher;

        public LauncherController()
        {
            //Integers to translate the relative coordinate movements to absolute coordinates
            trackx = 0;
            tracky = 0;

            //Initialize launcher and reset the position to the base coordinates
            launcher = new MissileLauncher();
            launcher.command_reset();
        }

        public void set_launcher(int x, int y, bool fire)
        {
            x = x - trackx;
            y = y - tracky;
            move_launcher(x, y, fire);
        }

        public void move_launcher(int x, int y, bool fire)
        {
            //Update the x and y absolute coordinates
            trackx += x;
            tracky += y;

            //Move launcher by x and y
            if (x <= 0)
                launcher.command_Left(Math.Abs(x));
            else if (x > 0)
                launcher.command_Right(Math.Abs(x));
            if (y <= 0)
                launcher.command_Down(Math.Abs(y));
            else if (y > 0)
                launcher.command_Up(Math.Abs(y));

            //Fire the missile if "fire" is true
            if (fire)
                launcher.command_Fire();
        }

        public void soft_reset_position()
        {
            move_launcher(-trackx, -tracky, false);
        }
        public void hard_reset_position()
        {
            launcher.command_reset();
        }
    }
}
