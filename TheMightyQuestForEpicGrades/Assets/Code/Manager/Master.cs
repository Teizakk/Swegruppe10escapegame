using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Manager {
    public class Master : MonoBehaviour {

        private static Master _masterInstance;

        #region Zur Verfügung gestellte Manager
        // == ShortCuts für z.B. Master.Instance().GameStateManager.Save(); um das Spiel zu speichern und nicht über getComponent<GameStateManager>(); gehen zu müssen
        public GameStateManager MyGameState;
        public LevelManager MyLevel;
        public ModuleManager MyModule;
        public QuestionManager MyQuestion;
        public AudioManager MyAudio;
        public GameObject CurrentDialogController;
        #endregion

        //modifiziertes Singleton-Pattern (um von außen immer leicht an das Master Objekt zu kommen)
        public static Master Instance() {
            if (!_masterInstance) {
                _masterInstance = FindObjectOfType(typeof(Master)) as Master;
                if (!_masterInstance) {
                    Debug.LogError(
                        "Es muss ein aktives Master Skript auf einem GameObject in der Szene existieren");
                }
            }
            return _masterInstance;
        }

        // Use this for initialization
        void Start () {
            if (_masterInstance == null)
            {
                _masterInstance = this;
            }
            else if (_masterInstance != this)
            {
                Destroy(gameObject);
                return; //um sicher zu gehen, dass nicht weiter ausgeführt wird...
            }

            DontDestroyOnLoad(_masterInstance);

            //Debug-Ausgaben
            if (_masterInstance != null) Debug.Log("Master Objekt existiert");

            //Manager direkt zu Beginn initialisieren
            MyGameState = gameObject.AddComponent<GameStateManager>();
            MyLevel = gameObject.AddComponent<LevelManager>();
            MyModule = gameObject.AddComponent<ModuleManager>();
            MyQuestion = gameObject.AddComponent<QuestionManager>();
            MyAudio = gameObject.AddComponent<AudioManager>();
            CurrentDialogController = null;
        }

        public static void KILLME() {
            if (SceneManager.GetActiveScene().name == "MainGame" ||
                SceneManager.GetActiveScene().name == "EndOfGame" ||
                SceneManager.GetActiveScene().name == "InsertHighscoreEndOfGame") {

                if (PlayerScript.GetInstance() != null) DestroyImmediate(PlayerScript.GetInstance().gameObject);
                DestroyImmediate(Instance().gameObject);

                _masterInstance = null;
                Debug.Log("MASTER GEWOLLT GEKILLT - NEUER SPIELDURCHLAUF BEGINNT!");
                return;
            }
            Debug.Log("Aktuelle Szene: " + SceneManager.GetActiveScene().name);
            throw new UnityException("Master darf und sollte zu diesem Zeitpunkt nicht gelöscht werden!");
        }

        private void OnDestroy() {
            if (SceneManager.GetActiveScene().name == "MainGame" ||
                SceneManager.GetActiveScene().name == "EndOfGame" ||
                SceneManager.GetActiveScene().name == "InsertHighscoreEndOfGame" ||
                SceneManager.GetActiveScene().name == "MainMenu") return;
            Debug.Log("Aktuelle Szene: " + SceneManager.GetActiveScene().name);
            throw new UnityException("Master wurde auf irgendeine verwerfliche Art zum falschen Zeitpunkt gelöscht!");
        }
    }
}
