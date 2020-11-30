using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace MessyCoderCommunity.AI.NavMeshMovement
{
    /// <summary>
    /// Move the agent to a specific location.
    /// </summary>
    [CreateAssetMenu(fileName = "Move To", menuName = "Messy AI/Movement/Nav Mesh/Move To")]
    public class MoveTo : GenericAiBehaviour<NavMeshAgent>
    {
        [Tooltip("The name of a Vector3 variable that holds the position to be moved to.")]
        public string targetPositionVariable = "targetPosition";

        Vector3 currentTargetPosition = Vector3.zero;
        NavMeshAgent navMeshAgent = null;

        public override void Initialize(GameObject agent, IChalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);

            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            Debug.Assert(navMeshAgent != null, "MoveTo behaviour requires a NavMeshAgent component on the agent.");
        }

        public override void Tick(IChalkboard chalkboard)
        {
            base.Tick(chalkboard);

            Vector3 position = chalkboard.GetSystem<Vector3>(targetPositionVariable);

            if (position == currentTargetPosition)
            {
                return;
            }

            currentTargetPosition = position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
            {
                position = hit.position;
            }

            navMeshAgent.SetDestination(position);
            navMeshAgent.isStopped = false;
        }
    }
}
