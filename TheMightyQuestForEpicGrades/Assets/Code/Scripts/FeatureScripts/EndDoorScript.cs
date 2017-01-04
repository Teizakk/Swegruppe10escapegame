using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Scripts.FeatureScripts {
    public class EndDoorScript : MonoBehaviour {

        private bool _canBeOpened;
        private bool _isOpening;
        private int _cntOfDescends;

        private const float _DESCENDING_RATE = 0.01f;
        private Vector3 _colStartPos;
        
        // Use this for initialization
        void Start () {
            _canBeOpened = false;
            _colStartPos = transform.localPosition;
        }

        private void OnTriggerEnter(Collider other) {
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
                Debug.Log("EndOfLevel getriggert");
                GoToNextScreenPromt();
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
            transform.localPosition += (Vector3.down * _DESCENDING_RATE);
            if (_cntOfDescends % 10 == 0) { //Alle 10 Schritte (weniger speicherintensiv) Position immer wieder auf default zurücksetzen
                GetComponent<CapsuleCollider>().center = (_colStartPos - transform.localPosition)/3.0f; //wieso der andere sich 3x so schnell bewegt? Keine Ahnung. Scale scheint es nicht zu sein!
            }
            _cntOfDescends++;
            if (_cntOfDescends < 2.3f*(1/_DESCENDING_RATE)) return;
            //Ab nun ist das Portal offen
            _isOpening = false;
            //Kollision mit Box abschalten
            GetComponent<BoxCollider>().enabled = false;
            //Trigger Collider für LevelEnde anschalten
            GetComponent<CapsuleCollider>().enabled = true;
            Debug.Log("Collider geswitched");
            //Debug.Log("Unterschied zur Startposition: " + (_colStartPos - transform.localPosition));
        }

        private void GoToNextScreenPromt() {
            if (Master.Instance().MyGameState.StageCurrent == 3) {
                SceneManager.LoadScene("EndOfGame");
                Master.Instance().MyGameState.SetGameWon();
                return;
            }
            SceneManager.LoadScene("InBetweenLevels");
        }
    }
}
