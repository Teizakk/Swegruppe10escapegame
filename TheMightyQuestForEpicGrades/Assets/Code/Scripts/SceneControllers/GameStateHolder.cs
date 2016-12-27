using Assets.Code.Manager;
using Assets.Code.Models;
using UnityEngine;

namespace Assets.Code.Scripts.SceneControllers {
    public class GameStateHolder : MonoBehaviour {
        //Singleton instance
        private static GameStateHolder gameStateHolderObject;


        #region "Speicherplatz"
        //Darf an keiner anderen Stelle implementiert sein
        public GameState GameStateObject;
        #endregion

        #region Zur Verfügung gestellte Manager
        //TODO kappa-kapseln und nur über getter erreichbar machen?
        public GameManager MyGameManager;
        public LevelManager MyLevelManager;
        public ModuleManager MyModuleManager;
        public QuestionManager MyQuestionManager;
        #endregion


        //Sollte im Hauptmenü initialisiert werden.
        public void Start() {
            if (gameStateHolderObject == null) {
                gameStateHolderObject = this;
            }
            else if (gameStateHolderObject != this) {
                Destroy(gameObject);
                return; //um sicher zu gehen, dass nicht weiter ausgeführt wird...
            }

            DontDestroyOnLoad(gameStateHolderObject);

            //Debug-Ausgaben
            if (gameStateHolderObject != null) Debug.Log("GameStateHolder Objekt existiert");

            GameStateObject = new GameState();

            if (GameStateObject != null) Debug.Log("GameState Objekt existiert");

            //TODO Manager direkt zu Beginn initialisieren?
            MyGameManager = gameObject.AddComponent<GameManager>();
            MyLevelManager = gameObject.AddComponent<LevelManager>();
            MyModuleManager = gameObject.AddComponent<ModuleManager>();
            MyQuestionManager = gameObject.AddComponent<QuestionManager>();
            //TODO sind die Referenzen jetzt wieder Kurzschlüsse auf das gleiche Objekt?
            //-> anscheinend nur auf die Componenten, weil man diese direkt ansprechen kann.
            
        }

        //modifiziertes Singleton-Pattern (um von außen leicht an das GameState Objekt zu kommen)
        public static GameStateHolder Instance() {
            if (!gameStateHolderObject) {
                gameStateHolderObject = FindObjectOfType(typeof(GameStateHolder)) as GameStateHolder;
                if (!gameStateHolderObject)
                    Debug.LogError(
                        "Es muss ein aktives GameStateHolder Skript auf einem GameObject in der Szene existieren");
            }
            return gameStateHolderObject;
        }
    }
}