#if NODE_CANVAS_PRESENT
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace MessyCoderCommunity.AI.NodeCanvasIntegration
{

	[Category("Messy AI/Debug")]
	[Description("Log an output to the console,")]
	public class LogActionTask : ActionTask<Transform> {
		[Tooltip("The AI behaviour to run.")]
		public AiBehaviour aiBehaviourTemplate = null;

		private NCBlackboardBridge chalkboard;
		private AiBehaviour aiBehaviour;

		protected override string OnInit(){
			chalkboard = new NCBlackboardBridge(blackboard);

			Debug.Assert(aiBehaviourTemplate != null, "You must provide a behaviour for the AiBehaviourRunner to run in " + agent.name);
			aiBehaviour = ScriptableObject.Instantiate<AiBehaviour>(aiBehaviourTemplate);
			aiBehaviour.Initialize(agent.gameObject, chalkboard);

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