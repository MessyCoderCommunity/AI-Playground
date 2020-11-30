using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    public class AiBehaviour : ScriptableObject
    {
        private GameObject agent;

        /// <summary>
        /// Get the Type of the agent this refers to. For base `AiBehaviours` this
        /// is always `GameObject` but implementations of this class can be generic,
        /// e.g. see `GenericAiBehaviour` or specifically tied to a specific agent
        /// type, such as `NavMeshAgent` or `Transform`.
        /// </summary>
        public virtual Type AgentType {
            get { return typeof(GameObject); }
        }

        /// <summary>
        /// Initialize is called once per behavour. It is used to configure the
        /// behaviour and is a place where you can cache values in the chalkboard
        /// etc.
        /// </summary>
        /// <param name="agent">The agent this behaviour belongs to.</param>
        /// <param name="chalkboard">The chalkboard for sharing variables between behaviours</param>
        public virtual void Initialize(GameObject agent, IChalkboard chalkboard)
        {
            this.agent = agent;
        }

        /// <summary>
        /// Tick is where the behaviour does it's work. In the curren implementation of
        /// the development framework this is called on a timed basis that is controlled
        /// by the `AiBehaviourRunner`. However, most Ai Frameworks assume the equivalent to this
        /// method is called every frame. We should consider 
        /// REFACTOR: move tick timing from `AiBehaviorRunner` to `AiBehaviour`
        /// See issue #3
        /// </summary>
        /// <param name="chalkboard"></param>
        public virtual void Tick(IChalkboard chalkboard) { }

        /// <summary>
        /// Called in editor whenever the agent executing this behaviour is selected.
        /// Use this to draw Gizmos to help in debugging the behaviour.
        /// </summary>
        /// <param name="agent">The agent this behaviour belongs to</param>
        public virtual void OnDrawGizmosSelected(UnityEngine.Object agent) { }
    }
}
