using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using Assets.Code.Scripts.SceneControllers;
using UnityEngine;

//Singlton nicht zwingend erforderlich aber Umstellung ist atm zu zeitaufwendig

namespace Assets.Code.Scripts.FeatureScripts {
    public class PlayerScript : MonoBehaviour {
        private static PlayerScript instance;

        public static bool _loadingASavedGame = false;
        
        private bool controlsBlocked;

        private int DebugLogVar;
        private int DebugLogVar2;

        private Vector3 position;

        private Rigidbody rb;
        
        public float speed;

        private void Start() {
            if (instance == null) {
                Debug.Log("Instanz wurde verknuepft");
                instance = this;
                Debug.Log("PlayerInstanceID = " + GetInstanceID());
            }
            else if (instance != null) {
                DestroyImmediate(this.gameObject);
            }
                
            rb = GetComponent<Rigidbody>();

            DontDestroyOnLoad(instance);
            
            Debug.Log("Muss der Spieler bewegt werden? " + _loadingASavedGame);
            
            SetStartPosition(_loadingASavedGame);
            _loadingASavedGame = false;
           
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
            //Debug.Log(controlsBlocked ? "Controls are now blocked" : "Controls are now enabled");
        }

        public void SetStartPosition(bool getPosFromGameState = false) {
            if (getPosFromGameState) {
                position.x = Master.Instance().MyGameState.PlayerPosCurrent.x;
                position.z = Master.Instance().MyGameState.PlayerPosCurrent.z;
            }
            else {
                position.x = Master.Instance().MyLevel.BoardBuilder_TMP.StartPosition.x;
                position.z = Master.Instance().MyLevel.BoardBuilder_TMP.StartPosition.z;
            }
            //y bereits in Start() gesetzt
            //BoardBuilder wieder killen, hat genau jetzt seinen Dienst getan
            if (Master.Instance().MyLevel.BoardBuilder_TMP != null) {
                Destroy(Master.Instance().MyLevel.BoardBuilder_TMP.gameObject);
                Master.Instance().MyLevel.BoardBuilder_TMP = null;
            }

            rb.MovePosition(new Vector3(position.x, 1.0f, position.z));
        }

        private void OnTriggerStay(Collider other) {
            DebugLogVar++;
            if (controlsBlocked) return; //Dann sowieso nix tun 
            //SOLLTE IMMER IN DER REIHENFOLGE PORTALSTEIN->ENDDOOR->FRAGE(Truhe) erfolgen
            if (other.gameObject.CompareTag("PinkPortal")) { // && Portalstein vorhanden)
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portalstein einsetzen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    Master.Instance().MyGameState.InsertPortalStone(other.gameObject, PortalColor.Pink);
                    //TODO Master.Instance().MyGameState. PINK PORTAL 
                    //Debug.Log("PinkPortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.PinkPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
            }
            else if (other.gameObject.CompareTag("GreenPortal")) { // && Portalstein vorhanden
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portalstein einsetzen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    Master.Instance().MyGameState.InsertPortalStone(other.gameObject, PortalColor.Green);
                    //TODO Master.Instance().MyGameState. GREEN PORTAL 
                    //Debug.Log("GreenPortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.GreenPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
            }
            else if (other.gameObject.CompareTag("BluePortal")) { // && Portalstein vorhanden)
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portalstein einsetzen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    Master.Instance().MyGameState.InsertPortalStone(other.gameObject, PortalColor.Blue);
                    //TODO Master.Instance().MyGameState. BLUE PORTAL 
                    //Debug.Log("BluePortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.GreenPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
            }
            else if (other.gameObject.CompareTag("Finish")) { // = steht vor der Endtüre
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Portal oeffnen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    if (Master.Instance().MyGameState.HasUsedAllPortalStones()) {
                        other.gameObject.GetComponent<EndDoorScript>().OpenDoor(); //TODO über GameStateManager gehen?
                        Debug.Log("Türe wird durch den Spieler geöffnet");
                    }
                }
            }
            else if (other.gameObject.CompareTag("Chest")) {
                Master.Instance().CurrentDialogController.SendMessage("ActivateTooltip", "Truhe oeffnen");
                if (Input.GetKeyDown(KeyCode.E)) {
                    //öffnen der Truhe,Fragen laden
                    Master.Instance().MyGameState.OpenChest(other.gameObject);
                    DebugLogVar2++;
                    Debug.Log("Truhe öffnen (" + DebugLogVar2 + ")");
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            Master.Instance().CurrentDialogController.SendMessage("DeactivateTooltip");
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