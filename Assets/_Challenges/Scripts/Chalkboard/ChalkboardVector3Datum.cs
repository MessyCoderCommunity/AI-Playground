using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MessyCoderCommunity.AI
{
    [SerializeField]
    public class ChalkboardVector3Datum
    {
        public int hash;
        public string name;
        public Vector3 value;

        public ChalkboardVector3Datum(string name, Vector3 value)
        {
            this.hash = name.GetHashCode();
            this.name = name;
            this.value = value;
        }
    }
}
