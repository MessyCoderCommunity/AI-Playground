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
        [SerializeField, Tooltip("The name of the chalkboard variable in which to store the list of interactables detected.")]
        string interactablesListName = "interactables";
        [SerializeField, Tooltip("The name of the chalkboard variable in which to store the position of the chosen interactable. " +
            "If null the position will not be stored.")]
        private string chosenInteractablePositionName;


        public override void Initialize(GameObject agent, Chalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);
            
            chalkboard.Add("NavMeshAgent", agent.GetComponent<NavMeshAgent>());
            noInteractablesBehaviour.Initialize(agent.gameObject, chalkboard);

            interactablesBehaviour.Initialize(agent.gameObject, chalkboard);
        }
        public override void Tick(Chalkboard chalkboard)
        {
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
                Vector3 pos = detectedInteractables[0].GetInteractionPosition();
                chalkboard.Add(chosenInteractablePositionName, pos);

                if (noInteractablesBehaviour)
                {
                    interactablesBehaviour.Tick(chalkboard);
                }
            }
        }
    }
}
