using Assets.Code.Manager;
using Assets.Code.Scripts.SceneControllers;
using UnityEngine;

//TODO auch hier muss man nochmal genau gucken, ob man den Singleton braucht

namespace Assets.Code.Scripts.FeatureScripts {
    public class PlayerScript : MonoBehaviour {
        public static PlayerScript instance;

        private bool controlsBlocked;

        private int DebugLogVar;

        [HideInInspector] public float position_X;
        [HideInInspector] public float position_Z;

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
        }

        public PlayerScript GetInstance() {
            return instance;
        }

        public void switchControlBlock() {
            controlsBlocked = !controlsBlocked;
            //Debug Ausgabe
            Debug.Log(controlsBlocked ? "Controls are now blocked" : "Controls are now enabled");
        }

        public void SetStartPosition() {
            position_X = Master.Instance().MyLevel.BoardBuilder_TMP.StartPosition.x;
            position_Z = Master.Instance().MyLevel.BoardBuilder_TMP.StartPosition.z;

            rb.MovePosition(new Vector3(position_X, 1.0f, position_Z));
        }

        private void OnCollisionStay(Collision other) {
            DebugLogVar++;
            if (!controlsBlocked)
                if (other.gameObject.CompareTag("Chest") && Input.GetKeyDown(KeyCode.E)) {
                    //öffnen der Truhe,Fragen laden
                    Debug.Log("Truhe öffnen (" + DebugLogVar + ")");
                }
                else if (other.gameObject.CompareTag("PinkPortal") && Input.GetKeyDown(KeyCode.E))
                        // && Portalstein vorhanden)
                {
                    //TODO Master.Instance().MyGameState. PINK PORTAL 
                    //Debug.Log("PinkPortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.PinkPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
                else if (other.gameObject.CompareTag("GreenPortal") && Input.GetKeyDown(KeyCode.E))
                        // && Portalstein vorhanden)
                {
                    //TODO Master.Instance().MyGameState. GREEN PORTAL 
                    //Debug.Log("GreenPortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.GreenPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
                else if (other.gameObject.CompareTag("BluePortal") && Input.GetKeyDown(KeyCode.E))
                        // && Portalstein vorhanden)
                {
                    //TODO Master.Instance().MyGameState. BLUE PORTAL 
                    //Debug.Log("BluePortalSkript.Activated = " + GameStateHolder.Instance().GameStateObject.LevelState.GreenPortalStone.Used.ToString() + " (" + DebugLogVar +")");
                }
                else if (other.gameObject.CompareTag("Finish") && Input.GetKeyDown(KeyCode.E)) { //Steht vor Endtür
                    other.gameObject.GetComponent<EndDoorScript>().OpenDoor();
                    Debug.Log("Türe wird durch den Spieler geöffnet");
                }
        }

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