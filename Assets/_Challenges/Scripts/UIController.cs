using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MessyCoderCommunity
{
    public class UIController : MonoBehaviour
    {
        [Header("Help")]
        [SerializeField, Tooltip("The help text to show in the center of the screen. The user can hit 'h' to show/hide this textbox.")]
        TextMeshProUGUI helpText = default;
        [SerializeField, Tooltip("Display the help text on startup?")]
        bool displayHelpText = false;

        private void Awake()
        {
            helpText.enabled = displayHelpText;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                helpText.enabled = !helpText.enabled;
            }
        }
    }
}
