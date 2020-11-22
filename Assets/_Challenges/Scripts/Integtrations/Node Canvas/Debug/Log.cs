#if NODE_CANVAS_PRESENT
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace MessyCoderCommunity.AI.NodeCanvasIntegration
{

	[Category("Messy AI/Debug")]
	[Description("Log an output to the console,")]
	public class Log : ActionTask<GameObject> {
		[Tooltip("The AI behaviour to run.")]
		public AiBehaviour aiBehaviour = null;

		private NCBlackboardBridge chalkboard;

		protected override string OnInit(){
			chalkboard = new NCBlackboardBridge(blackboard); 
			aiBehaviour.Initialize(agent, chalkboard);

			// TODO: capture errors in the behaviour initalization and pass them on to NodeCanvas
			return null;
		}

		protected override void OnExecute(){
			// TODO: MessyAI needs an equivalent to the OnExecute of NodeCanvas actions
		}

		protected override void OnUpdate(){
			aiBehaviour.Tick(chalkboard);
		}

		protected override void OnStop(){
			// TODO: MessyAI needs an equivalent to the OnStop of NodeCanvas actions
		}

		protected override void OnPause(){
			// TODO: MessyAI needs an equivalent to the OnPause of NodeCanvas actions			
		}
	}
}
#endif