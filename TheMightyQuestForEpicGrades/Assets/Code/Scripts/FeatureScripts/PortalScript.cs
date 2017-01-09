using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class PortalScript : MonoBehaviour {
        public int Index { get; set; }

        public void Activate() { //aka start glowing
            GetComponentInChildren<Light>().enabled = true;
        }

        private void Awake() {
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}
