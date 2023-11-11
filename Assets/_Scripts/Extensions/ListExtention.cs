﻿using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using Random = System.Random;

namespace _Scripts.Extensions
{
    public static class ListExtention
    {
        private static Random rng = new Random();  

        public static IList<T> Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }

        public static T[] Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }

            return array;
        }


        public static void Move<T>(this IList<T> list, int oldIndex, int newIndex)
        {
            T item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
        }
        
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            int index = rng.Next(0, enumerable.Count());

            return enumerable.ElementAt(index);
        }


        //public static T MaxBy<T>(this IList<T> list, Func<>)
    }
}

/*
 * Шафлить можно проще, вот так - 
 * Random rnd = new Random();
 * list = list.OrderBy(x => rnd.Next()).ToArray(); 
 */