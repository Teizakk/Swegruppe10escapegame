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
        private List<string> knownModules = Master.Instance().MyModule.GetModulesAsList();

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
                ModuleNameField.placeholder.GetComponent<Text>().text = "! Modul existiert bereits !";
                ModuleNameField.placeholder.GetComponent<Text>().color = new Color(1, 0.25f, 0); //Orange
                ModuleNameField.text = "";
                return;
            }
            Master.Instance().MyModule.SaveToFile(_textToSubmit);
            ModuleNameField.placeholder.GetComponent<Text>().text = "~Modul gespeichert~";
            ModuleNameField.placeholder.GetComponent<Text>().color = new Color(0, 0.5f, 0);
            ModuleNameField.text = "";
            _textToSubmit = "";
            //knownModules aktualisieren
            knownModules = Master.Instance().MyModule.GetModulesAsList();
        }
    }
}