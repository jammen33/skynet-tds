﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Collections.ObjectModel;

using SkynetTDS.Targets;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.Util;

namespace SkynetTDS.Vision
{

    class FriendFoeImageProcessor : IImageProcessor
    {
        Collection<Target> targets;
        Image<Bgr, Byte> img;
        float resolution = 5;
        int minDistance = 70;
        int minRadius = 25;
        int maxRadius = 40;
        int centerThreshold = 50;

        public Collection<Target> DetectTargets(Image image)
        {
            lock (image)
            {
                img = new Image<Bgr, byte>(new Bitmap((Image)image.Clone()));
            }
            targets = new Collection<Target>();
            //img = new Image<Bgr, byte>("NewCircleTest.png").Resize(610, 360, INTER.CV_INTER_LINEAR);
            findCircleTargets();
            findSquareTargets();
            img.Dispose();

            return targets;
        }

        private void findCircleTargets()
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
                    else if (color.Green > color.Blue && color.Red < color.Green && color.Green > 150)
                    {
                        tmpTarget = new Target();
                        tmpTarget.Color = Color.Green;
                        tmpTarget.IsFriend = true;
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

        private void findSquareTargets()
        {
            Target tmpTarget;
            bool ok = true;
            //Convert the image to grayscale and filter out the noise
            Image<Gray, Byte> gray = img.Copy().Convert<Gray, Byte>().PyrDown().PyrUp();

            Gray cannyThreshold = new Gray(180);
            Gray cannyThresholdLinking = new Gray(120);
            Gray circleAccumulatorThreshold = new Gray(120);

            Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold, cannyThresholdLinking);
            LineSegment2D[] lines = cannyEdges.HoughLinesBinary(
                1, //Distance resolution in pixel-related units
                Math.PI / 45.0, //Angle resolution measured in radians.
                20, //threshold
                30, //min Line width
                10 //gap between lines
                )[0]; //Get the lines from the first channel

            #region Find triangles and rectangles
            List<MCvBox2D> boxList = new List<MCvBox2D>();

            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                for (Contour<Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    if (contours.Area > 250) //only consider contours with area greater than 250
                    {
                        if (currentContour.Total == 4) //The contour has 4 vertices.
                        {
                            #region determine if all the angles in the contour are within the range of [80, 100] degree
                            bool isRectangle = true;
                            Point[] pts = currentContour.ToArray();
                            LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                            for (int i = 0; i < edges.Length; i++)
                            {
                                double angle = Math.Abs(
                                   edges[(i + 1) % edges.Length].GetExteriorAngleDegree(edges[i]));
                                if (angle < 80 || angle > 100)
                                {
                                    isRectangle = false;
                                    break;
                                }
                            }
                            #endregion

                            if (isRectangle) boxList.Add(currentContour.GetMinAreaRect());
                        }
                    }

            #endregion



                    #region draw triangles and rectangles

                    foreach (MCvBox2D box in boxList)
                    {
                        bool match = false;
                        foreach (Target t in targets)
                        {
                            double cx = box.center.X - t.Point.X;
                            double cy = box.center.Y - t.Point.Y;

                            if (Math.Abs(cx) < centerThreshold && Math.Abs(cy) < centerThreshold)
                            {
                                match = true;
                            }
                        }
                        if (!match)
                        {
                            Bgr color = img[(int)box.center.Y, (int)box.center.X];

                            if (color.Green > 150 || color.Red > 150)
                            {
                                tmpTarget = new Target();

                                if (color.Green > color.Red)
                                {
                                    tmpTarget.Color = Color.Green;
                                }
                                else
                                {
                                    tmpTarget.Color = Color.Red;
                                }
                                tmpTarget.IsFriend = true;
                                tmpTarget.Point = box.center;
                                tmpTarget.Distance = 0;
                                tmpTarget.IsMoving = false;
                                targets.Add(tmpTarget);
                            }
                            else
                            {
                                match = false;
                            }
                        }
                    #endregion




                    }
                }
        }
    }
}
