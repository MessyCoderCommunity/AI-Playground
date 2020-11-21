using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    /// <summary>
    /// The Log Behaviour simply logs a message to the console every tick.
    /// </summary>
    [CreateAssetMenu(fileName = "Log Behaviour", menuName = "Messy AI/Debug/Log")]
    public class LogBehaviour : GenericAiBehaviour<GameObject>
    {
        [Tooltip("The message to display.")]
        public string message;

        public override void Tick(Chalkboard chalkboard)
        {
            GameObject agent = chalkboard.GetUnity<GameObject>("agent".GetHashCode());
            Debug.Log(agent.name + " says " + message);
        }
    }
}
