using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class ListExtensions
    {
        public static IReadOnlyList<T> GetRandomItems<T>(this IReadOnlyList<T> source, int count)
        {
            var items = new List<T>();
       
            for (var i = 0; i < count; i++)
            {
                var randomIndex = Random.Range(0, source.Count-1);
           
                if (items.Contains(source[randomIndex]))
                {
                    i--;
                    continue;
                }
           
                items.Add(source[randomIndex]);
            }
       
            return items;
        }

        public static T GetRandomItem<T>(this IReadOnlyList<T> source, T obj) where T : IComparable<T>
        {
            var randomIndex = Random.Range(0, source.Count-1);
        
            if (obj == null)
            {
                return source[randomIndex];
            }
        
            var items = source.Where(item => item.CompareTo(obj) != 1).ToList();
            randomIndex = Random.Range(0, items.Count - 1);
        
            return items[randomIndex];
        }

        public static T GetRandomItem<T>(this IReadOnlyList<T> source)
        {
            var randomIndex = Random.Range(0, source.Count - 1);
            return source[randomIndex];
        }
    }
}