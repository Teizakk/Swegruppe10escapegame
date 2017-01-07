using Assets.Code.Manager;
using UnityEngine;

//Singlton nicht zwingend erforderlich aber Umstellung ist atm zu zeitaufwendig

namespace Assets.Code.Scripts.FeatureScripts {
    public class PlayerScript : MonoBehaviour {
        private static PlayerScript instance;

        private bool controlsBlocked;

        private int DebugLogVar;

        private Vector3 position;

        private Rigidbody rb;
        
        public float speed;

        private void Start() {
            if (instance == null) {
                Debug.Log("Instanz wurde verknuepft");
                instance = this;
            }
            else if (instance != null)
                Destroy(gameObject);

            rb = GetComponent<Rigidbody>();

            DontDestroyOnLoad(instance);
            SetStartPosition();

            //Setzen der Start-Position
            //rb.MovePosition(new Vector3(position_X,0,position_Z));

            //Standardmäßig sind die Kontrollen natürlich an...
            controlsBlocked = false;

            //Setzen des Speeds
            speed = GLOBALS.CONSTANTS.PLAYER_SPEED;

            //Position allokieren und Höhe setzen
            position = new Vector3( -1.0f, 1.0f, -1.0f);
        }

        public static PlayerScript GetInstance() {
            return instance;
        }

        public Vector3 GetPosition() {
            //Aktualisiert und returned position
            position = gameObject.transform.position;
            return position;
        }

        public void SwitchControlBlock() {
            controlsBlocked = !controlsBlocked;
            //Debug Ausgabe
            Debug.Log(controlsBlocked ? "Controls are now blocked" : "Controls are now enabled");
        }

        private void SetStartPosition() {
            position.x = Master.Instance().MyLevel.BoardBuilder_TMP.StartPosition.x;
            position.z = Master.Instance().MyLevel.BoardBuilder_TMP.StartPosition.z;
            //y bereits in Start() gesetzt
            //BoardBuilder wieder killen, hat genau jetzt seinen Dienst getan
            Destroy(Master.Instance().MyLevel.BoardBuilder_TMP.gameObject);
            Master.Instance().MyLevel.BoardBuilder_TMP = null;

            rb.MovePosition(new Vector3(position.x, 1.0f, position.z));
        }

        private void OnCollisionStay(Collision col) {
            DebugLogVar++;
            if (controlsBlocked) return; //Dann sowieso nix tun
            //SOLLTE IMMER IN DER REIHENFOLGE PORTALSTEIN->ENDDOOR->FRAGE(Truhe) erfolgen (EndDoor ist allerdings trigger.. daher andere Funktion siehe unten)
            if (col.gameObject.CompareTag("PinkPortal")) { // && Portalstein vorhanden)
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portalstein einsetzen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    //TODO Master.Instance().MyGameState. PINK PORTAL 
                    //Debug.Log("PinkPortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.PinkPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
            }
            else if (col.gameObject.CompareTag("GreenPortal")) { // && Portalstein vorhanden
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portalstein einsetzen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    //TODO Master.Instance().MyGameState. GREEN PORTAL 
                    //Debug.Log("GreenPortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.GreenPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
            }
            else if (col.gameObject.CompareTag("BluePortal")) { // && Portalstein vorhanden)
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portalstein einsetzen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    //TODO Master.Instance().MyGameState. BLUE PORTAL 
                    //Debug.Log("BluePortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.GreenPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
            }
            else if (col.gameObject.CompareTag("Chest")) {
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Truhe oeffnen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    //öffnen der Truhe,Fragen laden
                    Debug.Log("Truhe öffnen (" + DebugLogVar + ")");
                }
            }
        }

        private void OnCollisionExit(Collision other) {
            Master.Instance().CurrentDialogController.SendMessage("DeactivateTooltip");
        }

        private void OnTriggerStay(Collider other) {
            if (!other.gameObject.CompareTag("Finish")) return; //Steht nicht vor Endtür
            Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portal oeffnen");
            if (!Input.GetKeyDown(KeyCode.E)) return;
            other.gameObject.GetComponent<EndDoorScript>().OpenDoor();
            Debug.Log("Türe wird durch den Spieler geöffnet");
        }

        //Debug-Funktion
        //private void OnTriggerEnter(Collider col) {
        //    Debug.Log(gameObject.name + " has triggered " + other.gameObject.name);
        //}

        private void FixedUpdate() {
            if (!controlsBlocked) {
                //Movement
                var moveHorizontal = Input.GetAxis("Vertical")*-1;
                var moveVertical = Input.GetAxis("Horizontal");

                //Gucken ob die kombinierte Bewegung von X und Z Achse über dem gesetzten Maximum von 1 liegt
                var combindedSpeed = Mathf.Sqrt(moveHorizontal*moveHorizontal + moveVertical*moveVertical);
                if (combindedSpeed > 1.0f) {
                    //Limitiert die Quadrate der Bewegungen so, dass maximal 1 als kombinierte Bewegung resultiert
                    moveHorizontal /= combindedSpeed;
                    moveVertical /= combindedSpeed;
                }
                var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.velocity = movement*speed;
                //Debug Ausgabe zeigt Spielergeschwindigkeit
                //Debug.Log(Mathf.Sqrt((moveHorizontal * moveHorizontal) + (moveVertical * moveVertical)));   
            }
        }
    }
}