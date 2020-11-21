using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    /// <summary>
    /// The Log Behaviour simply logs a message to the console every tick.
    /// </summary>
    [CreateAssetMenu(fileName = "Log Behaviour", menuName = "Messy AI/Debug/Log")]
    public class LogBehaviour : GenericAiBehaviour<GameObject>
    {
        [SerializeField, Tooltip("The message to display. This can include variables on the chalkboard using `{VARIABLE_NAME}`.")]
        string message = "{agent} says 'Hi'";

        private Regex variableRegex;

        public override void Initialize(GameObject agent, Chalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);

            variableRegex = new Regex(@"\{([^}]*)\}", RegexOptions.Compiled);
        }

        public override void Tick(Chalkboard chalkboard)
        {
            string expandedMessage = message;
            MatchCollection matches = variableRegex.Matches(expandedMessage);
            for (int i = 0; i < matches.Count; i ++)
            {
                string token = matches[i].Groups[0].Value;
                string variableName = matches[i].Groups[1].Value;

                UnityEngine.Object unityValue = chalkboard.GetUnity<UnityEngine.Object>(variableName.GetHashCode());
                if (unityValue != null)
                {
                    expandedMessage = expandedMessage.Replace(token, unityValue.ToString());
                } else
                {
                    System.Object systemValue = chalkboard.GetSystem<System.Object>(variableName.GetHashCode());
                    if (systemValue != null)
                    {
                        expandedMessage = expandedMessage.Replace(token, systemValue.ToString());
                    } else
                    {
                        expandedMessage = expandedMessage.Replace(token, "[Missing variable " + token + "]");
                    }
                }
                
            }

            Debug.Log(expandedMessage);
        }
    }
}
