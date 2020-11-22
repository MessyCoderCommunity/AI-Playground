#if NODE_CANVAS_PRESENT
using UnityEngine;
using NodeCanvas;
using NodeCanvas.Framework;

namespace MessyCoderCommunity.AI.NodeCanvasIntegration
{
    /// <summary>
    /// The blackboard bridge wraps a Node Canvas blackboard and presents it as
    /// a Messy AI chalkboard
    /// </summary>
    public class NCBlackboardBridge : IChalkboard
    {
        IBlackboard board;

        public NCBlackboardBridge(IBlackboard board)
        {
            this.board = board;
        }
        
        public void Add(string name, Object value)
        {
            board.AddVariable(name, value);
        }

        public void Add(string name, object value)
        {
            board.AddVariable(name, value);
        }

        public T GetUnity<T>(string name) where T : Object
        {
            return board.GetVariableValue<T>(name);
        }

        public T GetSystem<T>(string name)
        {
            return board.GetVariableValue<T>(name);
        }

        public T GetSystem<T>(int hash)
        {
            throw new System.NotImplementedException("Node Canvas does not support getting variables by hash");
        }

        public T GetUnity<T>(int hash) where T : Object
        {
            throw new System.NotImplementedException("Node Canvas does not support getting variables by hash");
        }
    }
}
#endif