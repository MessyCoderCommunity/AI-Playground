using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    public class AiBehaviour : ScriptableObject
    {
        public virtual Type AgentType {
            get { return typeof(GameObject); }
        }

        public virtual void Initialize(GameObject agent, Chalkboard chalkboard) { }

        public virtual void Tick(Chalkboard chalkboard) { }

        public virtual void OnDrawGizmosSelected(UnityEngine.Object agent) { }
    }
}
