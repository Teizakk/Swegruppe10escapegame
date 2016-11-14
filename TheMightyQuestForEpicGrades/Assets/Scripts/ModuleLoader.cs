using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class ModuleLoader {
    private List<string> module = new List<string>();

    public ModuleLoader(string dateiname)
    {
        module.Clear();

        try
        {
            if (dateiname != null)
            {
                using (StreamReader streamReader = new StreamReader(dateiname))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        module.Add(line);
                    }
                }
            }
            else Debug.LogError("Dateiname muss gesetzt werden!");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public string[] getModules()
    {
        return module.ToArray();
    }
}
