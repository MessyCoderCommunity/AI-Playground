using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    public class AiBehaviour : ScriptableObject
    {
        [SerializeField, Tooltip("The tick frequency in seconds. Set to zero for every frame.")]
        float tickFrequency = 0f;

        private float nextTickTime = 0;

        public virtual Type AgentType {
            get { return typeof(GameObject); }
        }

        public virtual void Initialize(GameObject agent, Chalkboard chalkboard) { }

        public virtual void Tick(Chalkboard chalkboard)
        {
        }

        /// <summary>
        /// Is it time for this behaviour to update.
        /// </summary>
        protected virtual bool IsTimeToUpdate
        {
            get
            {
                if (nextTickTime != 0 && Time.time < nextTickTime)
                {
                    return false;
                }
                nextTickTime = Time.time + tickFrequency;
                return true;
            }
        }

        public virtual void OnDrawGizmosSelected(UnityEngine.Object agent) { }
    }
}
