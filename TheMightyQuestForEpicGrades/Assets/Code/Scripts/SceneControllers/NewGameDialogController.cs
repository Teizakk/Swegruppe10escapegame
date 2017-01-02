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

        private void Awake() {
            Master.Instance().MyModule.LoadFromFile(); //Falls zwischenzeitlich aktualisiert
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

        #region Master-Link
        private void Start() {
            Master.Instance().CurrentDialogController = this.gameObject;
        }

        private void OnDestroy() {
            Master.Instance().CurrentDialogController = null;
        }
        #endregion
    }
}