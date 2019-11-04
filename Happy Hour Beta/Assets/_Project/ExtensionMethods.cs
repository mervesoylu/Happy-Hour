using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var randomIndex = Random.Range(i, count);
                var tmp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = tmp;
            }
        }
    } 
}
