using System;
using System.Collections.Generic;
using System.IO;
using Assets.Code.Scripts.UtilityScripts;
using Assets.Code.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Manager {
    public class ModuleManager : MonoBehaviour {
        //Sollte man von außen den Dateinamen ändern wollen
        public readonly string _Dateiname = "Module.txt";

        //Zwischenspeicher für die Auswahl des Moduls, dem eine Frage hinzugefügt werden soll.
        private string moduleToEdit;
        public string ModuleToEdit {
            get {
                if (SceneManager.GetActiveScene().name == "NewQuestion") {
                    return moduleToEdit;
                }
                throw new UnityException("ModuleToEdit sollte außerhalb und NewQuestion Szenen nicht aufgerufen werden!");
            }
            set {
                if (SceneManager.GetActiveScene().name == "NewContent") {
                    moduleToEdit = value;
                    return;
                }
                throw new UnityException(
                    "ModuleToEdit sollte außerhalb von NewContent und NewModule Szenen nicht aufgerufen werden!");
            }
        }

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

        public List<string> GetModulesAsList() {
            return new List<string>(module);
        }

		// listet alle Module im Verzeichnis auf
        // TODO : wäre das nicht geeignet für Module bzw. Fragen?
		//public string[] getModules() {
  //          return Directory.GetFiles(Persist.ExecuteablePath + "\\Module").Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();
  //      }

        public bool SaveToFile(string newModuleName) {
            try {
                if (File.Exists(_Dateiname)) {
                    using (var myStreamWriter = new StreamWriter(_Dateiname, true)) { //true = append
                        myStreamWriter.WriteLine(newModuleName);
                    }
                    //Refresh nach update
                    LoadFromFile();
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

        // lädt Fragen aus dem modul in "Module" Verzeichnis
        // TODO : wäre das nicht geieignet für Module bzw. Fragen?
    //    public static List<Question> LoadQuestionsFromModul(string modul)
    //    {
    //        try {
				//Debug.Log(Persist.ExecuteablePath + "/Module/" + modul + ".txt");
				//string json = File.ReadAllText(Persist.ExecuteablePath + "/Module/" + modul + ".txt");
    //            return JsonConvert.DeserializeObject<List<Question>>(json);
    //        } catch (FileNotFoundException e) {
    //            return new List<Question>();
    //        }
    //    }

        private void Start() {
            //Initiales Laden der Module
            LoadFromFile();
        }
    }
}