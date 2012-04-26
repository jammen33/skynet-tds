using System;

namespace SkynetTDS.Launchers
{
    /// <summary>
    /// Holds onto information about whether the limit was reached or not.
    /// </summary>
    public class LimitReachedEventArgs: EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="reached">Enumeration indicating if the limit was reached.</param>
        public LimitReachedEventArgs(LimitReached reached)
        {
            LimitReached = reached;
        }

        #region Properties
        /// <summary>
        /// Gets when the limit is reached.
        /// </summary>
        public LimitReached LimitReached
        {
            get;
            private set;
        }
        #endregion
    }
    
    /// <summary>
    /// Enumeration indicating what limit has been reached.
    /// </summary>
    public enum LimitReached
    {
        None,
        XLimitReached,
        YLimitReached,
        Both
    }
}
