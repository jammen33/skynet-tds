﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

using SkynetTDS.Targets;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace SkynetTDS.Vision
{
    class FoeImageProcessor: IImageProcessor
    {
        Collection<Target> targets;
        Image<Bgr, Byte> img;
        float resolution = 5;
        int minDistance = 70;
        int minRadius = 30;
        int maxRadius = 40;
        int centerThreshold = 10;

        public FoeImageProcessor()
        {
            targets = new Collection<Target>();
        }

        public Collection<Target> DetectTargets(Image image) 
        {
            Bitmap bmpImage = new Bitmap(image);
            img = new Image<Bgr, byte>( bmpImage );
            findFoes();
            return targets;
        }

        private void findFoes()
        {
            bool ok = true;
            Target tmpTarget;
            //Convert the image to grayscale and filter out the noise
            Image<Gray, Byte> gray = img.Copy().Convert<Gray, Byte>().PyrDown().PyrUp();

            Gray cannyThreshold = new Gray(180);
            Gray cannyThresholdLinking = new Gray(120);
            Gray circleAccumulatorThreshold = new Gray(120);

            CircleF[] circles = gray.HoughCircles(
                cannyThreshold,
                circleAccumulatorThreshold,
                resolution, //Resolution of the accumulator used to detect centers of the circles
                minDistance, //min distance 
                minRadius, //min radius
                maxRadius //max radius
                )[0]; //Get the circles from the first channel

            foreach (CircleF circle in circles)
            {
                foreach (Target t in targets)
                {
                    if (Math.Abs(circle.Center.X - t.Point.X) < centerThreshold)
                    {
                        ok = false;
                    }
                }
                if (ok)
                {
                    Bgr color = img[(int)circle.Center.Y, (int)circle.Center.X];
                    if (color.Red > color.Blue && color.Red > color.Green && color.Red > 150)
                    {
                        tmpTarget = new Target();

                        tmpTarget.Color = Color.Red;
                        tmpTarget.IsFriend = false;
                        tmpTarget.Point = circle.Center;
                        tmpTarget.Distance = 0;
                        tmpTarget.IsMoving = false;

                        targets.Add(tmpTarget);
                    }
                }
                else
                {
                    ok = true;
                }
            }
        }
    }
}
