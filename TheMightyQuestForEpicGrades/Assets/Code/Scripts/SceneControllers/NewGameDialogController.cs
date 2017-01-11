using Assets.Code.GLOBALS;
using Assets.Code.Manager;
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
            moduleDropdown.AddOptions(Master.Instance().MyModule.GetModulesWithEnoughQuestionsAsList());

            // Damit die neu eingefügten Optionen angezeigt werden können
            moduleDropdown.value = 1;
            moduleDropdown.value = 0;

            //Variablen setzen für neuen Spielanfang
            PlayerScript._loadingASavedGame = false;
            InBetweenLevelsDialogController._loadingASaveGame = false;
            InBetweenLevelsDialogController._firstTimeUseOfScript = true;
        }

        public void SetGameOptionsInGameState() {
            Debug.Log("SetUpNewGame wird aufgerufen mit:\n" + 
                "PlayerName = " + playerName.text + 
                "\tModuleName = " + moduleDropdown.options[moduleDropdown.value].text + 
                "\tDifficulty = " + (Difficulties)difficultyDropdown.value + 1);
            Master.Instance().MyGameState.SetupNewGame(
               playerName: playerName.text,
               moduleName: moduleDropdown.options[moduleDropdown.value].text,
               difficulty: (Difficulties)difficultyDropdown.value + 1
               );
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