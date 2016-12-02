using UnityEngine;
using System.Collections;

public class BringToFront : MonoBehaviour {
    //Setzt das GameObject auf dem es ausgeführt wird als letztes Geschwisterkind = oben als letztes auf den anderen
    void OnEnable() {
        transform.SetAsLastSibling();
    }
}
