#if NODE_CANVAS_PRESENT
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;

namespace MessyCoderCommunity.AI.NodeCanvasIntegration
{
    public abstract class AbstractAction<T> : ActionTask<T> where T : UnityEngine.Object
    {
        [Tooltip("The AI behaviour to run.")]
        public AiBehaviour aiBehaviourTemplate = null;

        protected NCBlackboardBridge chalkboard;
        protected AiBehaviour aiBehaviour;

        protected override string OnInit()
        {
            chalkboard = new NCBlackboardBridge(blackboard);

            Debug.Assert(aiBehaviourTemplate != null, "You must provide a behaviour for the AiBehaviourRunner to run in " + agent.name);
            aiBehaviour = ScriptableObject.Instantiate<AiBehaviour>(aiBehaviourTemplate);

            // TODO: capture errors in the behaviour initalization and pass them on to NodeCanvas
            return null;
        }

        protected override void OnExecute()
        {
            // TODO: MessyAI needs an equivalent to the OnExecute of NodeCanvas actions
        }

        protected override void OnUpdate()
        {
            aiBehaviour.Tick(chalkboard);
        }

        protected override void OnStop()
        {
            // TODO: MessyAI needs an equivalent to the OnStop of NodeCanvas actions
        }

        protected override void OnPause()
        {
            // TODO: MessyAI needs an equivalent to the OnPause of NodeCanvas actions			
        }
    }
}
#endif
