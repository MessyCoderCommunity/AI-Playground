using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MessyCoderCommunity.AI
{
    [SerializeField]
    public class ChalkboardSystemDatum
    {
        public int hash;
        public string name;
        public System.Object value;

        public ChalkboardSystemDatum(string name, System.Object value)
        {
            this.hash = name.GetHashCode();
            this.name = name;
            this.value = value;
        }
    }
}
