using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class IListExtensions
    {
        public static void Shuffle<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = ts.Count - 1;

            for (var i = 0; i < last; i++)
            {
                var random = new Random().Next(i, count);
                var tmp = ts[i];
                ts[i] = ts[random];
                ts[random] = tmp;
            }
        }
    }
}
