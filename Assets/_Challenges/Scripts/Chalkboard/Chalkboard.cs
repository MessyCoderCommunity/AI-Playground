using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MessyCoderCommunity.AI
{
    /// <summary>
    /// A naive Chalkboard implementation for use in the test environment.
    /// This is not a robust or performant implementation do not use in production.
    /// </summary>
    public class Chalkboard : MonoBehaviour, IChalkboard
    {
        // REFACTOR: Now that we are storing in a Dictionary and we cannot use hash keys is there any value in using the Datum structs?
        [SerializeField]
        Dictionary<string, ChalkboardUnityDatum> unityEntries = new Dictionary<string, ChalkboardUnityDatum>();
        [SerializeField]
        Dictionary<string, ChalkboardSystemDatum> systemEntries = new Dictionary<string, ChalkboardSystemDatum>();
        [SerializeField]
        Dictionary<string, ChalkboardVector3Datum> vector3Entries = new Dictionary<string, ChalkboardVector3Datum>();
        
        /// <summary>
        /// Get a value from the chalkboard that is deriivable from System.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve</typeparam>
        /// <param name="name">The name of the variable</param>
        /// <returns>The value of the variable</returns>
        public T GetSystem<T>(string name)
        {
            ChalkboardSystemDatum datum;
            if (systemEntries.TryGetValue(name, out datum))
            {
                return (T)(object)datum.value;
            } else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Get a value from the chalkboard that is deriivable from UnityEngine.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve (must be derived from UnityEngine.Object)</typeparam>
        /// <param name="name">The name of the variable</param>
        /// <returns>The value of the variable</returns>
        public T GetUnity<T>(string name) where T : UnityEngine.Object
        {
            ChalkboardUnityDatum datum;
            if (unityEntries.TryGetValue(name, out datum))
            {
                return (T)(object)datum.value;
            }
            else
            {
                return default(T);
            }
        }

        public void AddOrUpdate(string name, UnityEngine.Object value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (unityEntries.ContainsKey(name))
                {
                    unityEntries.Remove(name);
                }
                unityEntries.Add(name, new ChalkboardUnityDatum(name, value));
            }
        }

        public void AddOrUpdate(string name, System.Object value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (systemEntries.ContainsKey(name))
                {
                    systemEntries.Remove(name);
                }
                systemEntries.Add(name, new ChalkboardSystemDatum(name, value));
            }
        }
    }
}
