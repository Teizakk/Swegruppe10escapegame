using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewGameDialogController : MonoBehaviour {

        public Dropdown difficultyDropdown;
        public Dropdown moduleDropdown;
        public InputField playerName;

        private void Start() {
            var modules = Master.Instance().MyModule.GetModulesAsArray();

            foreach (var str in modules)
                moduleDropdown.options.Add(new Dropdown.OptionData {text = str});

            // Damit die neu eingefügten Optionen angezeigt werden können
            moduleDropdown.value = 1;
            moduleDropdown.value = 0;
        }

        public void SetGameOptionsInGameState() {
            //TODO Instance nur einmal aufrufen und zwischenspeichern wäre glaube ich einfacher
            int difficulty = difficultyDropdown.value + 1; //+ 1 weil Easy = 1, Medium = 2, ... usw
            Master.Instance().MyGameState.DifficultyChosen = (Difficulties)difficulty;
            Master.Instance().MyGameState.ModuleUsed =
                    moduleDropdown.options[moduleDropdown.value].text;
            //Hier könnte man auch mit Value implementieren, allerdings würde dann ein ändern der Modulliste die Savegames schrotten.
            var setPlayerName = playerName.text;
            //wenn nicht gesetzt = Anonymus
            Master.Instance().MyGameState.PlayerName = setPlayerName;
        }

        public void SaveNewModule(string newModuleName) {
            Master.Instance().MyModule.SaveToFile(newModuleName);
            if (Master.Instance().MyModule.SaveToFile(newModuleName))
                Debug.Log("Modul: " + newModuleName + " gespeichert!");
            else
                Debug.LogError(
                    "Modul: " + newModuleName + " konnte nicht gespeichert werden, Fehler beim Schreiben in die Datei: " +
                    Master.Instance().MyModule._Dateiname);
        }

        //TODO geht jetzt so auch nicht mehr ist aber auch besser so xD

//        public void KeepAModuleControllerAlive() {
//            var controllerObj = FindObjectOfType<ModuleManager>();
//            DontDestroyOnLoad(controllerObj);
//        }

//        public void KillAModuleController() {
//            var controllerObj = FindObjectOfType<ModuleManager>();
//            Destroy(controllerObj.gameObject);
//        }
    }
}