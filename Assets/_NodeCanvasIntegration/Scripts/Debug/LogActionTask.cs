#if NODE_CANVAS_PRESENT
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace MessyCoderCommunity.AI.NodeCanvasIntegration
{

	[Category("Messy AI/Debug")]
	[Description("Log an output to the console,")]
	public class LogActionTask : AbstractAction<Transform> {

        protected override string OnInit()
        {
            string result = base.OnInit();
            if (!string.IsNullOrEmpty(result)) {
                return result;
            }
 
            aiBehaviour.Initialize(agent.gameObject, chalkboard);

            // TODO: Catch errors in the behaviour initalization
            return null;
        }
    }
}
#endif