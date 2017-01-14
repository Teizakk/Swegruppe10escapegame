using Assets.Code.GLOBALS;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class PortalScript : MonoBehaviour {
        public PortalColor Color { get; set; }

        public Mesh MeshToReset;
        private bool _arise;
        private int _cntOfSteps = 0;

        public void Activate() { //aka start glowing
            //GetComponentInChildren<Light>().enabled = true;
            _arise = true;
            GetComponent<SphereCollider>().enabled = false; //Damit keine Tooltips mehr kommen und man es nicht mehr öffnen kann
        }

        private void Update() {
            if (!_arise) return;
            //fährt hoch
            gameObject.transform.localScale += new Vector3(0.0f, 0.02f, 0.0f);
            _cntOfSteps++;
            if (_cntOfSteps >= 100) {
                //Fertig
                _arise = false;
            }
        }

        private void Awake() {
            GetComponent<MeshFilter>().mesh = MeshToReset;
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}
