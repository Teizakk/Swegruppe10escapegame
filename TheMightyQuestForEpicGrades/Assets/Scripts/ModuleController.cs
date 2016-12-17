using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModuleController : MonoBehaviour {

    public Dropdown difficultyDropdown;
    public Dropdown moduleDropdown;

    public string Dateiname;

    private ModuleLoader myModuleLoader = null;

    void Start() {
        myModuleLoader = new ModuleLoader(Dateiname);

        string[] modules = myModuleLoader.getModules();

        foreach (string str in modules)
        {
            moduleDropdown.options.Add(new Dropdown.OptionData() { text = str });
        }

        // Damit die neu eingefügten Optionen angezeigt werden können
        moduleDropdown.value = 1;
        moduleDropdown.value = 0;
    }

    public void SetGameOptionsInGameState() {
        GameStateHolder.Instance().GameStateObject.GameOptions.Difficulty = difficultyDropdown.value + 1; //+ 1 weil Easy = 1, Medium = 2, ... usw
        GameStateHolder.Instance().GameStateObject.GameOptions.Modul = moduleDropdown.options[moduleDropdown.value].text; //Hier könnte man auch mit Value implementieren, allerdings würde dann ein ändern der Modulliste die Savegames schrotten.
    }

    public void SaveNewModule(string newModuleName) {
        if (myModuleLoader.SaveNewModule(Dateiname, newModuleName)) {
            Debug.Log("Modul: " + newModuleName + " gespeichert!");
        }
        else {
            Debug.LogError("Modul: " + newModuleName + " konnte nicht gespeichert werden, Fehler beim Schreiben in die Datei: " + Dateiname);
        }
    }

    public void KeepAModuleControllerAlive() {
        var controllerObj = FindObjectOfType<ModuleController>();
        DontDestroyOnLoad(controllerObj);
    }

    public void KillAModuleController() {
        var controllerObj = FindObjectOfType<ModuleController>();
        Destroy(controllerObj.gameObject);
    }
}
