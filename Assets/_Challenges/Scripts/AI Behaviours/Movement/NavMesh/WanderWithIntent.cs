using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MessyCoderCommunity.AI.NavMeshMovement
{
    /// <summary>
    /// Wander semi-randomly such that the character looks like they have intent.
    /// </summary>
    [CreateAssetMenu(fileName = "Wander with Intent Behaviour", menuName = "Messy AI/Movement/Nav Mesh/Wander with Intent")]
    public class WanderWithIntent : GenericAiBehaviour<NavMeshAgent>
    {
        [SerializeField, Tooltip("How close does the agent need to be to a target to be deemed to have reached that target.")]
        public float minimumReachDistance = 0.25f;
        [SerializeField, Tooltip("What is the minimum distance the agent should be prepared to wander, time allowing? " +
            "When a new targt is selected a target at a distance between this and the max distance is selected. ")]
        public float minDistanceOfRandomPathChange = 10f;
        [SerializeField, Tooltip("What is the maximum distance the agent should be prepared to wander, time allowing? " +
            "When a new targt is selected a target at a distance between this and the min distance is selected. ")]
        public float maxDistanceOfRandomPathChange = 20f;
        [SerializeField, Tooltip("What is the minimum angle from the current path the agent should be prepared to wander? " +
            "When a new targt is selected a target at an angle between this and the max angle is selected. ")]
        public float minAngleOfRandomPathChange = -30;
        [SerializeField, Tooltip("What is the minimum angle from the current path the agent should be prepared to wander? " +
            "When a new targt is selected a target at an angle between this and the max angle is selected. ")]
        public float maxAngleOfRandomPathChange = 30;
        [SerializeField, Tooltip("How far from the Home position (the starting position of this agent) will the agent be " +
            "allowed to wander? If a destination cannot be found within this range the AI will turn around " +
            "and head back towards home.")]
        public float maxRange = 100;

        private Vector3 homePosition;
        private float sqrMagnitudeRange;


        public override void Initialize(GameObject agent, IChalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);

            homePosition = agent.transform.position;
            sqrMagnitudeRange = maxRange * maxRange;
        }

        public override void Tick(IChalkboard chalkboard)
        {
            NavMeshAgent agent = chalkboard.GetUnity<NavMeshAgent>("agent");
            if (agent == null)
            {
                agent = chalkboard.GetUnity<NavMeshAgent>("NavMeshAgent");
            }
            
            agent.SetDestination(GetValidWanderPosition(agent.transform, 0));
            agent.isStopped = false;
        }

        private Vector3 GetValidWanderPosition(Transform transform, int attemptCount)
        {
            int maxAttempts = 6;
            bool turnAround = false;

            attemptCount++;
            if (attemptCount > maxAttempts)
            {
                return homePosition;
            }
            else if (attemptCount > maxAttempts / 2)
            {
                turnAround = true;
            }

            Vector3 position;
            float minDistance = minDistanceOfRandomPathChange;
            float maxDistance = maxDistanceOfRandomPathChange;

            Quaternion randAng;
            if (!turnAround)
            {
                randAng = Quaternion.Euler(0, UnityEngine.Random.Range(minAngleOfRandomPathChange, maxAngleOfRandomPathChange), 0);
            }
            else
            {
                randAng = Quaternion.Euler(0, UnityEngine.Random.Range(180 - minAngleOfRandomPathChange, 180 + maxAngleOfRandomPathChange), 0);
                minDistance = maxDistance;
            }
            position = transform.position + randAng * transform.forward * UnityEngine.Random.Range(minDistance, maxDistance);

            // TODO: should handle height on multiple terrains
            // TODO: should handle height on meshes too
            if (UnityEngine.Terrain.activeTerrain != null)
            {
                position.y = UnityEngine.Terrain.activeTerrain.SampleHeight(position);
            }

            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
            {
                position = hit.position;
            }
            else
            {
                GetValidWanderPosition(transform, attemptCount);
            }

            if (Vector3.SqrMagnitude(homePosition - position) > sqrMagnitudeRange)
            {
                return homePosition;
            }
            return position;
        }

        public override void OnDrawGizmosSelected(UnityEngine.Object agent)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(((NavMeshAgent)agent).destination, 0.25f);

            Gizmos.color = Color.gray;
            Gizmos.DrawCube(homePosition, new Vector3(0.25f, 0.25f, 0.25f));

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(homePosition, maxRange);
        }
    }
}
