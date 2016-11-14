using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModuleController : MonoBehaviour {

    public Dropdown dropdown;
    public string Dateiname;

    void Start()
    {
        ModuleLoader moduleLoader = new ModuleLoader(Dateiname);
        string[] modules = moduleLoader.getModules();

        foreach (string str in modules)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = str });
        }

        // Damit die neu eingefügten Optionen angezeigt werden können
        dropdown.value = 1;
        dropdown.value = 0;
    }
}
