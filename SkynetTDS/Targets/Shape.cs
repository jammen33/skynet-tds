using System.Collections.ObjectModel;
using System.Drawing;

namespace SkynetTDS.Targets
{
    /// <summary>
    /// Holds information about the shape.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Shape()
        {
            Vertices = new Collection<PointF>();
        }

        #region Properties
        /// <summary>
        /// Gets or sets the verticies.
        /// </summary>
        public Collection<PointF> Vertices
        {
            get;
            set;
        }
        #endregion
    }
}
