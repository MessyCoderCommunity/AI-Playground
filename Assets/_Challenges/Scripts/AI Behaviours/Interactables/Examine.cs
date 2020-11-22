using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using MessyCoderCommunity.AI.NavMeshMovement;

namespace MessyCoderCommunity.AI.Interactables
{
    /// <summary>
    /// Move to an interaction position for an interactable and examin the item.
    /// </summary>
    [CreateAssetMenu(fileName = "Examine Interactable", menuName = "Messy AI/Interactable/Examine")]
    public class Examine : GenericAiBehaviour<GameObject>
    {
        [SerializeField, Tooltip("The name of the chalkboard variable in the interactable to be examined is stored. ")]
        private string interactable = "interactable";

        [SerializeField, Tooltip("The behaviour to execute in order to move to the chosen interaction point.")]
        // TODO: Generatlize this to allow different movement types
        MoveTo moveToBehaviour = null;

        public override void Initialize(GameObject agent, Chalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);

            Debug.Assert(moveToBehaviour != null, "The Examine behaviour requires that a movement " +
                "behaviour is provided otherwise the agent cannot move to the interaction point.");

            chalkboard.Add("NavMeshAgent", agent.GetComponent<NavMeshAgent>());
            moveToBehaviour.Initialize(agent.gameObject, chalkboard);
        }

        public override void Tick(Chalkboard chalkboard)
        {
            base.Tick(chalkboard);

            // FIXME: Need to provide a method that is called once on startup of a behaviour so we can cache values like these.
            Interactable item = chalkboard.GetUnity<Interactable>(interactable.GetHashCode());
            Vector3 pos = item.GetInteractionPosition();
            chalkboard.Add(moveToBehaviour.targetPositionVariable, pos);
            moveToBehaviour.Tick(chalkboard);
        }

    }
}
