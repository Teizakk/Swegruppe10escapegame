using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SceneManagerScripts
{
    public class NewModuleHelperScript : MonoBehaviour
    {
        private ModuleController ModuleControllerLeftover;
        public InputField ModuleNameField;

        public Button SubmitButton;

        public string _textToSubmit { get; set; }

        // Use this for initialization
        private void Start()
        {
            ModuleControllerLeftover = FindObjectOfType<ModuleController>();
            if (ModuleControllerLeftover == null) Debug.LogError("ModuleControllerLeftover nicht gefunden");
        }

        public void SubmitNewModule()
        {
            if (string.IsNullOrEmpty(_textToSubmit)) Debug.LogError("Modulname darf nicht leer sein.");
            ModuleControllerLeftover.SaveNewModule(_textToSubmit);
            ModuleNameField.text = "~Modul gespeichert~";
        }

        public void LeaveNewModuleWindow()
        {
            ModuleControllerLeftover.KillAModuleController();
            Debug.Log("Mitgeschleppte ModulControllerInstanz gekillt! (gewollt)");
        }
    }
}