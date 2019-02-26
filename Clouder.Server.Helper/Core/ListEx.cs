using System;
using System.Linq;
using System.Collections.Generic;

namespace Clouder.Server.Helper.Core
{
    public static class ListEx
    {
        public static List<T> Randomize<T>(this List<T> list)
        {
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < list.Count / 2; i++)
            {
                var item = list[random.Next(list.Count)];
                list.Remove(item);
                list.Insert(random.Next(list.Count), item);
            }

            return list;
        }

        /// <summary>
        /// Use this for short list since it does a full copy
        /// </summary>
        public static List<T> RandomlyTakeFromShortList<T>(this IEnumerable<T> all, int maxCount)
        {
            var count = (int)Math.Min(all.Count(), maxCount);
            var allCopy = new List<T>(all);

            var taken = new List<T>();
            var random = new Random(DateTime.Now.Millisecond);
            while (taken.Count < count)
            {
                var item = allCopy[random.Next(allCopy.Count)];
                taken.Add(item);
                allCopy.Remove(item);
            }

            return taken;
        }

        /// <summary>
        /// Use this for longer list since it does a full copy. It might return less number
        /// of items than asked
        /// </summary>
        public static List<T> RandomlyTakeFromLongList<T>(this IEnumerable<T> all, int maxCount)
        {
            var count = (int)Math.Min(all.Count(), maxCount);

            var taken = new List<T>();
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < count; i++)
            {
                var item = all.ElementAt(random.Next(all.Count()));
                if (!taken.Contains(item))
                {
                    taken.Add(item);
                }
            }

            return taken;
        }
    }
}
