using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewModuleDialogController : MonoBehaviour {
        
        //TODO reparieren

        private ModuleManager ModuleControllerLeftover;
        public InputField ModuleNameField;

        public Button SubmitButton;

        public string _textToSubmit { get; set; }

        //TODO dieser Fix hängt mit dem vom NewGameDialogController zusammen
        // | | | | | | | |
        // v v v v v v v v 
        // Use this for initialization
        //private void Start() {
        //    ModuleControllerLeftover = FindObjectOfType<ModuleManager>();
        //    if (ModuleControllerLeftover == null) Debug.LogError("ModuleControllerLeftover nicht gefunden");
        //}

        //public void SubmitNewModule() {
        //    if (string.IsNullOrEmpty(_textToSubmit)) Debug.LogError("Modulname darf nicht leer sein.");
        //    ModuleControllerLeftover.SaveNewModule(_textToSubmit);
        //    ModuleNameField.text = "~Modul gespeichert~";
        //}

        
        //public void LeaveNewModuleWindow() {
        //    ModuleControllerLeftover.KillAModuleController();
        //    Debug.Log("Mitgeschleppte ModulControllerInstanz gekillt! (gewollt)");
        //}
    }
}