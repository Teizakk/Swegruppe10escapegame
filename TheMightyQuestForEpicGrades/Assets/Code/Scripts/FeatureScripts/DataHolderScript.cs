using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class DataHolderScript : MonoBehaviour {
       public Dictionary<string, object> LinkedObjects = new Dictionary<string, object>();
        public Dictionary<string, ValueType> StoredValues = new Dictionary<string, ValueType>();
    }
}
