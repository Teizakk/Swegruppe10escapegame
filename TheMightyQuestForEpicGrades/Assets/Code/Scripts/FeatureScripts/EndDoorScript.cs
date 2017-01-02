using Assets.Code.Manager;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class EndDoorScript : MonoBehaviour {

        private bool _canBeOpened;
        private bool _isOpening;
        private bool _isOpen;
        private int _cntOfDescends;

        private const float _DESCENDING_RATE = 0.2f;

        // Use this for initialization
        void Start () {
            _canBeOpened = false;
        }

        public void OnTriggerEnter(Collider other) {
            Debug.Log("OnTriggerEnterFunktion");
            if (GetComponent<SphereCollider>().enabled) { //Noch nicht offen - check ob geöffnet werden kann
                Debug.Log("SphereCollider hat getriggert");
                if (_canBeOpened) return;
                if (!other.gameObject.CompareTag("Player")) return;
                if (Master.Instance().MyGameState.HasUsedAllPortalStones()) {
                    _canBeOpened = true;
                    Debug.Log("OnTriggerEnterFunktion -> Türe kann geöffnet werden (Voraussetzungen erfüllt)");
                }
            }
            else if (GetComponent<CapsuleCollider>().enabled) { //offen CapsuleCollider bewirkt laden des nächsten Levels
                Master.Instance().MyGameState.FinishLevel();
            }
            else {
                Debug.LogError("Kein Collider mehr aktiv?!");
            }
        }

        public void OpenDoor() {
            if (!_canBeOpened) {
                throw new UnityException("Türe darf noch nicht geöffnet werden - Aufruf an falscher Stelle?");
            }
            _isOpening = true;
            GetComponent<SphereCollider>().enabled = false;
            Debug.Log("EndDoor: Türe wird geöffnet");
        }

        // Update is called once per frame
        void Update () {
            if (!_isOpening) return;
            transform.localPosition = transform.localPosition + (Vector3.down * _DESCENDING_RATE);
            _cntOfDescends++;
            if (_cntOfDescends < 3*(1/_DESCENDING_RATE)) return;
            _isOpening = false;
            _isOpen = true;
            //Kollision mit Box abschalten
            GetComponent<BoxCollider>().enabled = false;
            //Trigger Collider für LevelEnde anschalten
            GetComponent<CapsuleCollider>().enabled = true;
            Debug.Log("Collider geswitched");
        }
    }
}
