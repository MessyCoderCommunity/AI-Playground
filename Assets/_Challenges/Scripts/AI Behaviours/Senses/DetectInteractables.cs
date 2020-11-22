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
        private string chosenInteractablePositionName = "";

        private Transform transform = null;

        public override void Initialize(GameObject agent, IChalkboard chalkboard)
        {
            base.Initialize(agent, chalkboard);

            noInteractablesBehaviour.Initialize(agent, chalkboard);
            interactablesBehaviour.Initialize(agent, chalkboard);

            transform = agent.transform;
        }
        public override void Tick(IChalkboard chalkboard)
        {
            base.Tick(chalkboard);

            List<Interactable> detectedInteractables = new List<Interactable>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != transform.gameObject)
                {
                   Interactable interactable = colliders[i].GetComponentInParent<Interactable>();
                    if (interactable)
                    {
                        detectedInteractables.Add(interactable);
                    }
                }
            }
            chalkboard.AddOrUpdate(interactablesListName, detectedInteractables);

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
                chalkboard.AddOrUpdate(chosenInteractablePositionName, pos);

                if (interactablesBehaviour)
                {
                    interactablesBehaviour.Tick(chalkboard);
                }
                Debug.Log(transform.name + " detected " + chalkboard.GetSystem<List<Interactable>>(interactablesListName).Count + " Colliders");
            }
        }
    }
}
