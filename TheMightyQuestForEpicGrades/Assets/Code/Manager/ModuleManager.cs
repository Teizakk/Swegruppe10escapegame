using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Assets.Code.GLOBALS;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Manager {
    public class ModuleManager : MonoBehaviour {
        //Sollte man von außen den Dateinamen ändern wollen
        public readonly string _Dateiname = "Module.txt";

        //Zwischenspeicher für die Auswahl des Moduls, dem eine Frage hinzugefügt werden soll.
        private string _moduleToEdit;
        public string ModuleToEdit {
            get {
                if (SceneManager.GetActiveScene().name == "NewQuestion")
                {
                    return _moduleToEdit;
                }
                throw new UnityException("ModuleToEdit sollte außerhalb und NewQuestion Szenen nicht aufgerufen werden!");
            }
            set {
                if (SceneManager.GetActiveScene().name == "NewContent")
                {
                    _moduleToEdit = value;
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

        public List<string> GetModulesWithEnoughQuestionsAsList() {
            var tmpModuleList = new List<string>(module);
            var allModuls = Persist.GetModuleFiles();
            for (var index = 0; index < allModuls.Count; index++) {
                var modul = allModuls[index];
                var loadedModuleFile = Persist.Load<ModuleQuestions>("Modules\\" + modul);
                if (!loadedModuleFile.HasEnoughQuestions()) {
                    tmpModuleList.RemoveAt(index);
                }
                else {
                    tmpModuleList[index] += " (" + loadedModuleFile.GetCombinedNumberOfQuestions() + " Fragen)";
                }
            }
            return tmpModuleList;
        }

        public List<string> GetModulesWithEnoughQuestionWarningAsList() {
            var tmpModuleList = new List<string>(module);
            var allModuls = Persist.GetModuleFiles();
            for (var index = 0; index < allModuls.Count; index++) {
                var modul = allModuls[index];
                var loadedModuleFile = Persist.Load<ModuleQuestions>("Modules\\" + modul);
                if (!loadedModuleFile.HasEnoughQuestions()) {
                    tmpModuleList[index] += " (" + loadedModuleFile.GetCombinedNumberOfQuestions() + "/90 Fragen)";
                }
            }
            return tmpModuleList;
        }

        public List<string> GetModulesAsList() {
            return new List<string>(module);
        }

        public bool SaveToFile(string newModuleName) {
            try {
                if (File.Exists(_Dateiname)) {
                    using (var myStreamWriter = new StreamWriter(_Dateiname, true)) { //true = append
                        myStreamWriter.WriteLine(newModuleName);
                    }
                    //Refresh nach update
                    LoadFromFile();
                    Debug.Log("Modul erstellt und in Module.txt eingetragen");
                    return Master.Instance().MyQuestion.CreateNewModuleFile(newModuleName);
                }
                Debug.LogError("Module Savefile existiert nicht!");
                return false;
            }
            catch (Exception e) {
                Debug.LogError(e);
                return false;
            }
        }
        
        //ONLY DEV FUNCTION
        public void ReadQuestionsFromCSV(string fileName, string modul, Difficulties difficulty, string chapter) { //diese werte müssen "händisch" übergeben werden in diesem Fall
            var data = CSVReader.Read(fileName);
            modul = "DNIS"; //weil es im Moment nur das gibt 
            for (var index = 0; index < data.Count; index++) {
                var t = data[index]; //wählt Zeile aus
                var q = new Question {
                    Modul = modul,
                    Answers = new List<Question.Answer>(),
                    Chapter = chapter,
                    Difficulty = difficulty,
                    Hints =
                            new List<string>
                            {
                                t["Hinweis1"].ToString(),
                                t["Hinweis2"].ToString(),
                                t["Hinweis3"].ToString()
                            },
                    QuestionDuration = new TimeSpan(0),
                    ImagePath = t["FragenBild"].ToString(),
                    Used = false,
                    QuestionText = t["Frage"].ToString()
                };

                q.Answers.Capacity = 3; //Oder muss das mit .Add gemacht werden?

                q.Answers[0].AnswerText = null;
                q.Answers[1].AnswerText = null;
                q.Answers[2].AnswerText = null;

                //Antworten Mixer
                var valueIsSet = new bool[3];
                var randomizer = new System.Random();
                var i = 1;
                do {
                    var rdmInt = randomizer.Next(0, 2);
                    if (valueIsSet[rdmInt]) continue;
                    q.Answers[rdmInt].AnswerText = t["Antwort" + i].ToString();
                    q.Answers[rdmInt].ImagePath = t["Antwort" + i + "Bild"].ToString();
                    if (i == 3) {
                        q.CorrectAnswer = rdmInt;
                    }
                    valueIsSet[rdmInt] = true;
                    i++;
                } while (valueIsSet.Contains(false));

                Master.Instance().MyModule.AddQuestionToModule(q);
            }
        }

        public void AddQuestionToModule(Question q) {
            var moduleFile = Persist.Load<ModuleQuestions>(q.Modul);
            if (moduleFile == null) {
                throw new FileNotFoundException("Modulfile konnte nicht geladen/geöffnet werden!");
            }

            //Das Speichern sollte ab hier funktionieren - es müssen allerdings noch ein paar Werte angepasst werden:
            //Bilder hereinladen und Pfade ändern
            if (!string.IsNullOrEmpty(q.ImagePath)) { //Wenn es eine Frage-Bild gibt
                q.ImagePath = Persist.CopyPictureToResourcesFolder(q.ImagePath); //schreibt direkt den aktuellen Pfad herein
            }

            foreach (var a in q.Answers) {
                var answerImagePath = a.ImagePath;
                if (!string.IsNullOrEmpty(answerImagePath)) { //Wenn es ein Antwort-Bild gibt
                    a.ImagePath = Persist.CopyPictureToResourcesFolder(answerImagePath); //siehe oben
                }
            }

            switch (q.Difficulty) {
                case Difficulties.Easy:
                    moduleFile.QuestionsEasy.Add(q);
                    break;
                case Difficulties.Medium:
                    moduleFile.QuestionsMedium.Add(q);
                    break;
                case Difficulties.Hard:
                    moduleFile.QuestionsHard.Add(q);
                    break;
                default:
                    throw new UnityException("Ungültiger Schwierigkeitsgrad mitgegeben");
            }
            moduleFile.LastUpdated = DateTime.Now.ToLocalTime();
            //alle Infos hinzugefügt, jetzt wieder abspeichern
            Persist.Save(moduleFile, "Modules\\" + q.Modul);
            Debug.Log("Datei: " + q.Modul + ".dat erfolgreich aktualisiert!");
        }

        private void Start() {
            //Initiales Laden der Module
            LoadFromFile();
        }
    }
}