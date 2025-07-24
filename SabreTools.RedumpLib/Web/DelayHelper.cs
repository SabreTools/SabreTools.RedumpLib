using System;
using System.Threading;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Helper class for delaying
    /// </summary>
    internal static class DelayHelper
    {
        /// <summary>
        /// Delay a random amount of time up to 5 seconds
        /// </summary>
        public static void DelayRandom()
        {
            var r = new Random();
            int delay = r.Next(0, 50);
            Thread.Sleep(delay * 100);
        }
    }
}
