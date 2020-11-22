#if NODE_CANVAS_PRESENT
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace MessyCoderCommunity.AI.NodeCanvasIntegration
{
    [Category("Messy AI/Movement/Wander with Intent")]
    [Description("Wander within a defined area varying from the existing path by a manageable amount each step to give the" +
        " impression that the agent is going somewhere rather than just wanding back and forth.")]
    public class WanderWithintentAction : AbstractAction<Transform>
    {
        protected override string OnInit()
        {
            string result = base.OnInit();
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            aiBehaviour.Initialize(agent.gameObject, chalkboard);

            // TODO: Catch errors in the behaviour initalization
            return null;
        }
    }
}
#endif