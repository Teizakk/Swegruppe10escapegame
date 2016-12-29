using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Code.Manager {
    public class ModuleManager : MonoBehaviour {
        //Sollte man von außen den Dateinamen ändern wollen
        public static string dateiname = "Module.txt";

        private readonly List<string> module = new List<string>();

        public void LoadFromFile() {
            module.Clear();
            try {
                if (dateiname != null)
                    using (var streamReader = new StreamReader(dateiname)) {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                            module.Add(line);
                    }
                else Debug.LogError("Dateiname muss gesetzt werden!"); //Überbleibsel aber hey, was solls man kann nie auf zu viele Fehler checken... :D
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
        }

        public string[] GetModulesAsArray() {
            return module.ToArray();
        }

        public bool SaveToFile(string newModuleName) {
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