using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    /// <summary>
    /// An Interactable is an object that an character may take an interest
    /// in.
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        [SerializeField, Tooltip("The position that a character should stand when interacting with this object in local space.")]
        Vector3 interactionPosition = Vector3.zero;
        /// <summary>
        /// Get an available position from which a character can interact with this item.
        /// </summary>
        /// <returns>A Vector3 position in world space</returns>
        public Vector3 GetInteractionPosition() {
            return transform.position + interactionPosition;
        }
    }
}
