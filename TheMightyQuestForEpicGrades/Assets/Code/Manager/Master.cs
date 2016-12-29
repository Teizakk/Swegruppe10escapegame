using UnityEngine;

namespace Assets.Code.Manager {
    public class Master : MonoBehaviour {

        private static Master _masterInstance;

        #region Zur Verfügung gestellte Manager
        // == ShortCuts für z.B. Master.Instance().GameStateManager.Save(); um das Spiel zu speichern und nicht über getComponent<GameStateManager>(); gehen zu müssen
        public GameStateManager MyGameState;
        public LevelManager MyLevel;
        public ModuleManager MyModule;
        public QuestionManager MyQuestion;
        #endregion

        //modifiziertes Singleton-Pattern (um von außen immer leicht an das Master Objekt zu kommen)
        public static Master Instance() {
            if (!_masterInstance) {
                _masterInstance = FindObjectOfType(typeof(Master)) as Master;
                if (!_masterInstance) {
                    Debug.LogError(
                        "Es muss ein aktives GameStateHolder Skript auf einem GameObject in der Szene existieren");
                    Debug.Break();
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
        }
    }
}
