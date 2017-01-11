using System.Collections.Generic;
using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewModuleDialogController : MonoBehaviour {

        public InputField ModuleNameField;

        public Button SubmitButton;

        public string TextToSubmit { get; set; }
        private List<string> knownModules; 

        public void SubmitNewModule() {
            if (string.IsNullOrEmpty(TextToSubmit)) {
                Debug.LogError("Modulname darf nicht leer sein.");
                ModuleNameField.placeholder.GetComponent<Text>().text = "! Feld darf nicht leer sein !";
                ModuleNameField.placeholder.GetComponent<Text>().color = new Color(1,0.25f,0); //Orange
                ModuleNameField.text = "";
                return;
            }
            //Modul gibt es bereits
            if (knownModules.Contains(TextToSubmit)) {
                ModuleNameField.placeholder.GetComponent<Text>().text = "! Modul existiert bereits !"; //Das verhindert sogar das überschreiben der .dat Datei - HAHA!
                ModuleNameField.placeholder.GetComponent<Text>().color = new Color(1, 0.25f, 0); //Orange
                ModuleNameField.text = "";
                return;
            }

            if (Master.Instance().MyModule.SaveToFile(TextToSubmit)) {
                ModuleNameField.placeholder.GetComponent<Text>().text = "~Modul gespeichert~";
                ModuleNameField.placeholder.GetComponent<Text>().color = new Color(0, 0.5f, 0);
                ModuleNameField.text = "";
                TextToSubmit = "";
                //knownModules aktualisieren
                knownModules = Master.Instance().MyModule.GetModulesAsList();
            }
            else {
                throw new UnityException("Erstellen der neuen Modul-Datei fehlgeschlagen!");
            }
        }

        #region DEVSTUFF

        [Header("DEV STUFF")]
        public InputField FileName;
        public InputField ModuleName;
        public Slider Difficulty;
        public InputField ChapterName;

        public void InsertCSV() {
            Master.Instance().MyModule.ReadQuestionsFromCSV(
                fileName: FileName.text,
                difficulty: (Difficulties)(int)Difficulty.value,
                modul: ModuleName.text,
                chapter: ChapterName.text
                );
        }
        #endregion

        public void Awake() {
            Master.Instance().MyModule.LoadFromFile();
            knownModules = Master.Instance().MyModule.GetModulesAsList();
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