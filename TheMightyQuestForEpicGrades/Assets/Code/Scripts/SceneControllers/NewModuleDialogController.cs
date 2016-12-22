using Assets.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SceneControllers {
    public class NewModuleHelperScript : MonoBehaviour {
        
        //TODO reparieren

        private ModuleManager ModuleControllerLeftover;
        public InputField ModuleNameField;

        public Button SubmitButton;

        public string _textToSubmit { get; set; }

        // Use this for initialization
        private void Start() {
            ModuleControllerLeftover = FindObjectOfType<ModuleManager>();
            if (ModuleControllerLeftover == null) Debug.LogError("ModuleControllerLeftover nicht gefunden");
        }

        public void SubmitNewModule() {
            if (string.IsNullOrEmpty(_textToSubmit)) Debug.LogError("Modulname darf nicht leer sein.");
            ModuleControllerLeftover.SaveNewModule(_textToSubmit);
            ModuleNameField.text = "~Modul gespeichert~";
        }

        public void LeaveNewModuleWindow() {
            ModuleControllerLeftover.KillAModuleController();
            Debug.Log("Mitgeschleppte ModulControllerInstanz gekillt! (gewollt)");
        }
    }
}