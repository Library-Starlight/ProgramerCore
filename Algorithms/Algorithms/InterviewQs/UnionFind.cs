using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.InterviewQs
{
    public class UnionFind
    {
        /// <summary>
        /// Given a social network containing nn members and a log file containing mm timestamps at which times pairs of members formed friendships, 
        /// design an algorithm to determine the earliest time at which all members are connected (i.e., every member is a friend of a friend of a 
        /// friend ... of a friend). Assume that the log file is sorted by timestamp and that friendship is an equivalence relation. The running time 
        /// of your algorithm should be m \log nmlogn or better and use extra space proportional to nn.
        /// </summary>
        /// <remarks>
        /// these interview questions are ungraded and purely for your own enrichment. To get a hint, submit a solution.
        /// </remarks>
        public void SocialNetworkConnectivity()
        {
            var logs = new SocialNetworkLogProvider().GetLogs();
        }

    }

    public class SocialNetworkLogProvider
    {
        public string[] GetLogs()
        {
            return default;
        }
    }
}
