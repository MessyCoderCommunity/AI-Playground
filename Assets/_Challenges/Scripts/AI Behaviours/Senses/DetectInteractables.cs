using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace MessyCoderCommunity.AI.Senses
{
    /// <summary>
    /// Detect all intereactables within a sense radius of the agent.
    /// </summary>
    [CreateAssetMenu(fileName ="Detect Interactables Behaviour", menuName = "Messy AI/Senses/Detect Interactable")]
    public class DetectInteractables : GenericAiBehaviour<Transform>
    {
        [SerializeField, Tooltip("The radius within which to attempt to detect interactables.")]
        float radius = 15;
        [SerializeField, Tooltip("The layers to detect interactables on.")]
        LayerMask layerMask = default;
        [SerializeField, Tooltip("The behaviour to execute if no interactable is within range.")]
        AiBehaviour noInteractablesBehaviour = null;
        [SerializeField, Tooltip("The behaviour to execute when an interactable is within range.")]
        AiBehaviour interactablesBehaviour = null;

        [Header("Ouputs")]
        [SerializeField, Tooltip("The name of the blackboard variable in which to store the list of interactables detected.")]
        string interactablesListName = "interactables";
        private int interactablesListHash;

        public override void Initialize(GameObject agent, Chalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);
            
            interactablesListHash = interactablesListName.GetHashCode();

            chalkboard.Add("NavMeshAgent", agent.GetComponent<NavMeshAgent>());
            noInteractablesBehaviour.Initialize(agent.gameObject, chalkboard);

            interactablesBehaviour.Initialize(agent.gameObject, chalkboard);
        }
        public override void Tick(Chalkboard chalkboard)
        {
            if (!IsTimeToUpdate) return;

            base.Tick(chalkboard);
            Transform t = chalkboard.GetUnity<Transform>("agent".GetHashCode());

            List<Interactable> detectedInteractables = new List<Interactable>();
            Collider[] colliders = Physics.OverlapSphere(t.position, radius, layerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != t.gameObject)
                {
                   Interactable interactable = colliders[i].GetComponentInParent<Interactable>();
                    if (interactable)
                    {
                        detectedInteractables.Add(interactable);
                    }
                }
            }
            chalkboard.Add(interactablesListName, detectedInteractables);

            if (detectedInteractables.Count == 0)
            {
                if (noInteractablesBehaviour)
                {
                    noInteractablesBehaviour.Tick(chalkboard);
                }
            }
            else
            {
                Debug.Log(t.name + " detected " + chalkboard.GetSystem<List<Interactable>>(interactablesListHash).Count + " Colliders");
            }
        }
    }
}
