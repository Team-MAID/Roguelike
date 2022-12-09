using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtensionMethods
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this ICollection<T> collection)
        {
            return collection.ElementAt(Random.Range(0, collection.Count));
        }
    }
}