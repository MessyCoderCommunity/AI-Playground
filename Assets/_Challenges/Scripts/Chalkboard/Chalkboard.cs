using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MessyCoderCommunity.AI
{
    public class Chalkboard : MonoBehaviour, IChalkboard
    {
        [SerializeField]
        List<ChalkboardUnityDatum> unityEntries = new List<ChalkboardUnityDatum>();
        [SerializeField]
        List<ChalkboardSystemDatum> systemEntries = new List<ChalkboardSystemDatum>();
        [SerializeField]
        List<ChalkboardVector3Datum> vector3Entries = new List<ChalkboardVector3Datum>();

        public Vector3 GetVector3(int hash) 
        {
            for (int i = 0; i < vector3Entries.Count; i++)
            {
                if (vector3Entries[i].hash == hash)
                {
                    return vector3Entries[i].value;
                }
            }

            return default(Vector3);
        }

        /// <summary>
        /// Get a value from the chalkboard that is deriivable from System.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve</typeparam>
        /// <param name="hash">The hash of the name of the variable</param>
        /// <returns>The value of the variable</returns>
        public T GetSystem<T>(string name)
        {
            for (int i = 0; i < systemEntries.Count; i++)
            {
                if (systemEntries[i].name == name)
                {
                    return (T)(object)systemEntries[i].value;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Get a value from the chalkboard that is deriivable from UnityEngine.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve (must be derived from UnityEngine.Object)</typeparam>
        /// <param name="hash">The hash of the name of the variable</param>
        /// <returns>The value of the variable</returns>
        public T GetUnity<T>(string name) where T : UnityEngine.Object
        {
            for (int i = 0; i < unityEntries.Count; i++)
            {
                if (unityEntries[i].name == name)
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

        public void Add(string name, UnityEngine.Object value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                unityEntries.Add(new ChalkboardUnityDatum(name, value));
            }
        }

        public void Add(string name, System.Object value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                systemEntries.Add(new ChalkboardSystemDatum(name, value));
            }
        }
    }
}
