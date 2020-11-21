using UnityEngine;

namespace MessyCoderCommunity.AI
{
    /// <summary>
    /// This is a very basic AI behaviour runner. Attach an AI Behaviour to this MonoBehaviour 
    /// and it will run it without the need for any third party AI Frameworks.
    /// </summary>
    [RequireComponent(typeof(Chalkboard))]
    public class AiBehaviourRunner : MonoBehaviour
    {
        [SerializeField, Tooltip("The AI behaviour to run.")]
        AiBehaviour aiBehaviour = null;

        [SerializeField, Tooltip("The tick frequency in seconds.")]
        float tickFrequency = 0.25f;

        private float nextTickTime = 0;
        private UnityEngine.Object m_Agent;
        private Chalkboard chalkboard;

        protected virtual UnityEngine.Object Agent
        {
            get
            {
                return m_Agent;
            }
        }

        private void Awake()
        {
            chalkboard = GetComponent<Chalkboard>();
            Debug.Assert(chalkboard != null, "Cannot find a chalkboard fpr " + gameObject.name);

            Debug.Assert(aiBehaviour != null, "You must provide a behaviour for the AiBehaviourRunner to run in " + gameObject.name);
            if (aiBehaviour.AgentType == typeof(GameObject))
            {
                m_Agent = gameObject;
            }
            else
            {
                m_Agent = GetComponent(aiBehaviour.AgentType);
                Debug.Assert(m_Agent != null, "Cannot find " + aiBehaviour.AgentType + " in " + gameObject.name + " or its children");
            }
            aiBehaviour.Initialize(gameObject, chalkboard);

            chalkboard.Add("agent", m_Agent);
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > nextTickTime)
            {
                aiBehaviour.Tick(chalkboard);
                nextTickTime = Time.time + tickFrequency;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (aiBehaviour != null && Agent != null)
            {
                aiBehaviour.OnDrawGizmosSelected(Agent);
            }
        }
    }
}
