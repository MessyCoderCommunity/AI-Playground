using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessyCoderCommunity.AI
{
    [Serializable]
    public class GenericAiBehaviour<T> : AiBehaviour where T : UnityEngine.Object
    {
        public override Type AgentType
        {
            get { return typeof(T); }
        }
    }
}