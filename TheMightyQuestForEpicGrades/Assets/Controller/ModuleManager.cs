using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Controller {
    public class ModuleManager {
        private readonly List<string> module = new List<string>();

        public ModuleManager(string dateiname) {
            module.Clear();
            try {
                if (dateiname != null)
                    using (var streamReader = new StreamReader(dateiname)) {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                            module.Add(line);
                    }
                else Debug.LogError("Dateiname muss gesetzt werden!");
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
        }

        public string[] getModules() {
            return module.ToArray();
        }

        public bool SaveNewModule(string dateiname, string newModuleName) {
            try {
                if (File.Exists(dateiname)) {
                    using (var myStreamWriter = new StreamWriter(dateiname, true)) { //true = append
                        myStreamWriter.WriteLine(newModuleName);
                    }
                    return true;
                }
                Debug.LogError("Module Savefile existiert nicht!");
                return false;
            }
            catch (Exception e) {
                Debug.LogError(e);
                return false;
            }
        }
    }
}