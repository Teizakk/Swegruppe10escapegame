using Assets.Controller;
using Assets.Models;
using Assets.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SceneControllers {
    public class NewGameViewScript : MonoBehaviour {
        private ModuleManager _myModuleController;

        public string Dateiname;

        public Dropdown difficultyDropdown;
        public Dropdown moduleDropdown;
        public InputField playerName;

        private void Start() {
            _myModuleController = new ModuleManager(Dateiname);

            var modules = _myModuleController.getModules();

            foreach (var str in modules)
                moduleDropdown.options.Add(new Dropdown.OptionData {text = str});

            // Damit die neu eingefügten Optionen angezeigt werden können
            moduleDropdown.value = 1;
            moduleDropdown.value = 0;
        }

        public void SetGameOptionsInGameState() {
            //TODO Instance nur einmal aufrufen und zwischenspeichern wäre glaube ich einfacher
            int difficulty = difficultyDropdown.value + 1; //+ 1 weil Easy = 1, Medium = 2, ... usw
            switch (difficulty) {
                case (int)Difficulties.Easy:
                    GameStateHolder.Instance().GameStateObject.GameOptions.Difficulty = Difficulties.Easy;
                    break;
                case (int)Difficulties.Medium:
                    GameStateHolder.Instance().GameStateObject.GameOptions.Difficulty = Difficulties.Medium;
                    break;
                case (int)Difficulties.Hard:
                    GameStateHolder.Instance().GameStateObject.GameOptions.Difficulty = Difficulties.Hard;
                    break;
            }
            GameStateHolder.Instance().GameStateObject.GameOptions.Modul =
                    moduleDropdown.options[moduleDropdown.value].text;
            //Hier könnte man auch mit Value implementieren, allerdings würde dann ein ändern der Modulliste die Savegames schrotten.
            var setPlayerName = playerName.text;
            //wenn nicht gesetzt = Anonymus
            if (string.IsNullOrEmpty(setPlayerName))
                GameStateHolder.Instance().GameStateObject.GameOptions.PlayerName = "Anonymous";
            else GameStateHolder.Instance().GameStateObject.GameOptions.PlayerName = playerName.text;
        }

        public void SaveNewModule(string newModuleName) {
            if (_myModuleController.SaveNewModule(Dateiname, newModuleName))
                Debug.Log("Modul: " + newModuleName + " gespeichert!");
            else
                Debug.LogError(
                    "Modul: " + newModuleName + " konnte nicht gespeichert werden, Fehler beim Schreiben in die Datei: " +
                    Dateiname);
        }

        //TODO geht jetzt so auch nicht mehr

        public void KeepAModuleControllerAlive() {
            var controllerObj = FindObjectOfType<ModuleManager>();
            DontDestroyOnLoad(controllerObj);
        }

        public void KillAModuleController() {
            var controllerObj = FindObjectOfType<ModuleManager>();
            Destroy(controllerObj.gameObject);
        }
    }
}