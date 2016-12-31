using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class BringToFrontScript : MonoBehaviour {
        //Setzt das GameObject auf dem es ausgeführt wird als letztes Geschwisterkind = oben als letztes auf den anderen
        private void OnEnable() {
            transform.SetAsLastSibling();
        }
    }
}