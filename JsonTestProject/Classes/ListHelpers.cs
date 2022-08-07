using System;
using System.Collections.Generic;

namespace JsonTestProject.Classes
{
    public static class ListHelpers
    {
        private static readonly Random random = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            int indexer = list.Count;
            while (indexer > 1)
            {
                indexer--;
                int item = random.Next(indexer + 1);
                (list[item], list[indexer]) = (list[indexer], list[item]);
            }
        }
    }
}
