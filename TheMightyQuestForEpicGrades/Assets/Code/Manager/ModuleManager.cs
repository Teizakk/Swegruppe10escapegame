using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Code.Manager {
    public class ModuleManager : MonoBehaviour {
        //Sollte man von außen den Dateinamen ändern wollen
        public readonly string _Dateiname = "Module.txt";

        private readonly List<string> module = new List<string>();

        public void LoadFromFile() {
            module.Clear();
            try {
                if (_Dateiname != null)
                    using (var streamReader = new StreamReader(_Dateiname)) {
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
                if (File.Exists(_Dateiname)) {
                    using (var myStreamWriter = new StreamWriter(_Dateiname, true)) { //true = append
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