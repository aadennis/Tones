using System.Collections.Generic;
using System;

namespace Model {

    public class NumberUtilities {

        /// <summary>
        ///  Return a pair of random integers, where the first is always lower than
        ///  the second, and there is not more than maxDistance between the returned integers. 
        ///  Keeping this interval close is because the main usecase is a musical interval test
        ///  where a large distance would not help with aural training.
        ///  It helps if the Random passed in has been used in previous calls, to help entropy.
        /// </summary>
        public List<int> GetRandomInterval(int lowerLimit, int upperLimit, int maxDistance, Random rand) {
            var lowerAndUpperLimit = new List<int>{rand.Next(lowerLimit,upperLimit)};
            var nextNote = rand.Next(lowerLimit, upperLimit);
            while (lowerAndUpperLimit[0] == nextNote || System.Math.Abs(lowerAndUpperLimit[0] - nextNote) > maxDistance) {
                nextNote = rand.Next(lowerLimit, upperLimit);
            }
            lowerAndUpperLimit.Add(nextNote);
            lowerAndUpperLimit.Sort();
            return lowerAndUpperLimit;
        }
    }
}