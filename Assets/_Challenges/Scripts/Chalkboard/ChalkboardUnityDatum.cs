using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MessyCoderCommunity.AI
{
    [SerializeField]
    public class ChalkboardUnityDatum
    {
        public int hash;
        public string name;
        public UnityEngine.Object value;

        public ChalkboardUnityDatum(string name, UnityEngine.Object value)
        {
            this.hash = name.GetHashCode();
            this.name = name;
            this.value = value;
        }
    }
}
