using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewModuleHelperScript : MonoBehaviour {

    private ModuleController ModuleControllerLeftover;

    public Button SubmitButton;
    public InputField ModuleNameField;

    public string _textToSubmit { get; set; }

    // Use this for initialization
    void Start() {
        ModuleControllerLeftover = FindObjectOfType<ModuleController>();
        if (ModuleControllerLeftover == null) {
            Debug.LogError("ModuleControllerLeftover nicht gefunden");
        }
    }

    public void SubmitNewModule() {
        if (String.IsNullOrEmpty(_textToSubmit)) {
            Debug.LogError("Modulname darf nicht leer sein.");
        }
        ModuleControllerLeftover.SaveNewModule(_textToSubmit);
        ModuleNameField.text = "~Modul gespeichert~";
    }

    public void LeaveNewModuleWindow() {
        ModuleControllerLeftover.KillAModuleController();
        Debug.Log("Mitgeschleppte ModulControllerInstanz gekillt! (gewollt)");
    }

}