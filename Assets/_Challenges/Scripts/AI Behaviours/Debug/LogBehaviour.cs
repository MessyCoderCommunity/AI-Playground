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
        [Tooltip("The message to display. This can include variables on the chalkboard using `{VARIABLE_NAME}`.")]
        public string message = "{agent} says 'Hi'";

        public override void Tick(Chalkboard chalkboard)
        {

            GameObject agent = chalkboard.GetUnity<GameObject>("agent".GetHashCode());
            message = message.Replace("{agent}", agent.ToString());
            Debug.Log(message);
        }
    }
}
