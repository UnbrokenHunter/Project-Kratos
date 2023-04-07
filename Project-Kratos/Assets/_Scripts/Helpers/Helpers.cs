using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public static class Helpers 
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        /// <summary>
        /// Non Allocating WaitForSeconds
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

    }
}
