using System.Drawing;

namespace SkynetTDS.Targets
{
    /// <summary>
    /// Contains information about a target.
    /// </summary>
    public class Target
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Target()
        {
            Color       = Color.Blue;
            IsFriend    = false;
            Point       = new System.Drawing.Point(0, 0);
            Distance    = 0;
            IsMoving    = false;
        }

        #region Properties
        /// <summary>
        /// Gets or sets whether the target is moving or not.
        /// </summary>
        public bool IsMoving
        {
            get;
            set;
        }        
        /// <summary>
        /// Gets or sets whether the target is friendly.
        /// </summary>
        public bool IsFriend
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the color of the target.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the outline of the target shape.
        /// </summary>
        public Shape Outline
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the position of the target.
        /// </summary>
        public PointF Point
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the depth (Z) of the target.
        /// </summary>
        public int Distance
        {
            get;
            set;
        }
        #endregion
    }
}
