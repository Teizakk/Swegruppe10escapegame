using Assets.Models;
using UnityEngine;

namespace Assets.Scripts.FeatureScripts {
    public class GameStateHolderScript : MonoBehaviour {
        //Singleton instance
        private static GameStateHolderScript gameStateHolderObject;

        //Darf an keiner anderen Stelle implementiert sein
        public GameState GameStateObject;

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
        }

        //modifiziertes Singleton-Pattern (um von außen leicht an das GameState Objekt zu kommen)
        public static GameStateHolderScript Instance() {
            if (!gameStateHolderObject) {
                gameStateHolderObject = FindObjectOfType(typeof(GameStateHolderScript)) as GameStateHolderScript;
                if (!gameStateHolderObject)
                    Debug.LogError(
                        "Es muss ein aktives GameStateHolder Skript auf einem GameObject in der Szene existieren");
            }
            return gameStateHolderObject;
        }
    }
}