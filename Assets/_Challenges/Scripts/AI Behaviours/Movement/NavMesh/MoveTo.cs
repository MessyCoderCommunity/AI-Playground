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
        [SerializeField, Tooltip("The name of a Vector3 variable that holds the position to be moved to.")]
        string targetPositionVariable = "targetPosition";

        int targetPositionVariableHash;
        Vector3 currentTargetPosition = Vector3.zero;

        public override void Initialize(GameObject agent, IChalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);
        }

        public override void Tick(IChalkboard chalkboard)
        {
            base.Tick(chalkboard);

            Vector3 position = chalkboard.GetVector3(targetPositionVariableHash);

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

            NavMeshAgent agent = chalkboard.GetUnity<NavMeshAgent>("agent");

            agent.SetDestination(position);
            agent.isStopped = false;
        }
    }
}
