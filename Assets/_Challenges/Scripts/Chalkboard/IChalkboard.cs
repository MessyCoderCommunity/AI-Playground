using System;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    public interface IChalkboard
    {
        /// <summary>
        /// Get a value from the chalkboard that is deriivable from System.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve</typeparam>
        /// <param name="name">The name of the variable</param>
        /// <returns></returns>
        T GetSystem<T>(string name);

        /// <summary>
        /// Get a value from the chalkboard that is deriivable from UnityEngine.Object 
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve (must be derived from UnityEngine.Object)</typeparam>
        /// <param name="name">The name of the variable</param>
        /// <returns></returns>
        T GetUnity<T>(string name) where T : UnityEngine.Object;

        void AddOrUpdate(string name, UnityEngine.Object value);
        void AddOrUpdate(string name, object value);
    }
}