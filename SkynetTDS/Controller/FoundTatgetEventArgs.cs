using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SkynetTDS.Targets;


namespace SkynetTDS.Controller
{
    public class FoundTatgetEventArgs : EventArgs
   {
        /// <summary>
        /// Constructor.
        /// </summary>
       /// <param name="numberOfFoes">Number of foe targets found.</param>
       ///  /// <param name="numberOfFriends">Number of friend targets found.</param>
        public FoundTatgetEventArgs(int numberOfFoes, int numberOfFriends, Collection<Target> t)
        {
            FoeCount = numberOfFoes;
            FriendCount = numberOfFriends;
            targets = t;
        }

        #region Properties
        /// <summary>
        /// Gets the number of foes
        /// </summary>
        public int FoeCount
        {
            get;
            private set;
        }
        /// Gets the number of friends
        /// </summary>
        public int FriendCount
        {
            get;
            private set;
        }
        public Collection<Target> targets
        {
            get;
            private set;
        }
        #endregion

    }
}
