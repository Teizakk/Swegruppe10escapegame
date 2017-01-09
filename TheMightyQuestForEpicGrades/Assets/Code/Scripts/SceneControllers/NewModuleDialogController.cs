using System.Collections.Generic;
using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewModuleDialogController : MonoBehaviour {

        private ModuleManager ModuleControllerLeftover;
        public InputField ModuleNameField;

        public Button SubmitButton;

        private string _textToSubmit { get; set; }
        private List<string> knownModules; 

        public void SubmitNewModule() {
            if (string.IsNullOrEmpty(_textToSubmit)) {
                Debug.LogError("Modulname darf nicht leer sein.");
                ModuleNameField.placeholder.GetComponent<Text>().text = "! Feld darf nicht leer sein !";
                ModuleNameField.placeholder.GetComponent<Text>().color = new Color(1,0.25f,0); //Orange
                ModuleNameField.text = "";
                return;
            }
            //Modul gibt es bereits
            if (knownModules.Contains(_textToSubmit)) {
                ModuleNameField.placeholder.GetComponent<Text>().text = "! Modul existiert bereits !"; //Das verhindert sogar das überschreiben der .dat Datei - HAHA!
                ModuleNameField.placeholder.GetComponent<Text>().color = new Color(1, 0.25f, 0); //Orange
                ModuleNameField.text = "";
                return;
            }
            if (Master.Instance().MyModule.SaveToFile(_textToSubmit)) {
                ModuleNameField.placeholder.GetComponent<Text>().text = "~Modul gespeichert~";
                ModuleNameField.placeholder.GetComponent<Text>().color = new Color(0, 0.5f, 0);
                ModuleNameField.text = "";
                _textToSubmit = "";
                //knownModules aktualisieren
                knownModules = Master.Instance().MyModule.GetModulesAsList();
            }
            else {
                throw new UnityException("Erstellen der neuen Modul-Datei fehlgeschlagen!");
            }
        }

        public void Awake() {
            Master.Instance().MyModule.GetModulesAsList();
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