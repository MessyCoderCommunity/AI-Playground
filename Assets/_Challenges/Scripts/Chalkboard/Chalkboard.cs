using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MessyCoderCommunity.AI
{
    public class Chalkboard : MonoBehaviour
    {
        [SerializeField]
        List<ChalkboardUnityDatum> unityEntries = new List<ChalkboardUnityDatum>();
        [SerializeField]
        List<ChalkboardSystemDatum> systemEntries = new List<ChalkboardSystemDatum>();

        /// <summary>
        /// Get a value from the chalkboard that is deriivable from UnityEngine.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve (must be derived from UnityEngine.Object)</typeparam>
        /// <param name="hash">The hash of the name of the variable</param>
        /// <returns></returns>
        public T GetUnity<T>(int hash) where T : UnityEngine.Object
        {
            for (int i = 0; i < unityEntries.Count; i++)
            {
                if (unityEntries[i].hash == hash)
                {
                    if (typeof(UnityEngine.Component).IsAssignableFrom(typeof(T)))
                    {
                        return ((UnityEngine.Component)unityEntries[i].value).GetComponent<T>();
                    }

                    return (T)unityEntries[i].value;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Get a value from the chalkboard that is deriivable from System.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve</typeparam>
        /// <param name="hash">The hash of the name of the variable</param>
        /// <returns></returns>
        public T GetSystem<T>(int hash)
        {
            for (int i = 0; i < systemEntries.Count; i++)
            {
                if (systemEntries[i].hash == hash)
                {
                    return (T)(object)systemEntries[i].value;
                }
            }

            return default(T);
        }

        internal void Add(string name, UnityEngine.Object value)
        {
            unityEntries.Add(new ChalkboardUnityDatum(name, value));
        }

        internal void Add(string name, System.Object value)
        {
            systemEntries.Add(new ChalkboardSystemDatum(name, value));
        }
    }
}
