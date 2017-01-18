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

        private List<string> module = new List<string>();


        public List<string> GetModulesWithEnoughQuestionsAsList() {
			var tmpList = new List<string> ();
            var allModuls = Persist.GetModuleFiles();
            foreach (var modul in allModuls) {
                var loadedModuleFile = Persist.Load<ModuleQuestions>("Modules\\" + modul);
                if (loadedModuleFile.HasEnoughQuestions()) {
                    tmpList.Add(modul);
                }
            }
            return tmpList;
        }

        public List<string> GetModulesWithEnoughQuestionWarningAsList() {
            var tmpModuleList = new List<string>(module);
            var allModuls = Persist.GetModuleFiles();
            for (var index = 0; index < allModuls.Count; index++) {
                var modul = allModuls[index];
                var loadedModuleFile = Persist.Load<ModuleQuestions>("Modules\\" + modul);
                if (!loadedModuleFile.HasEnoughQuestions()) {
					if (loadedModuleFile.GetCombinedNumberOfQuestions () >= 90)
						tmpModuleList [index] += " (Add Kapitel/Frage)";
					else
                    	tmpModuleList[index] += " (" + loadedModuleFile.GetCombinedNumberOfQuestions() + "/90 Fragen)";
                }
            }
            return tmpModuleList;
        }

        public List<string> GetModulesAsList()
        {
            // Refresh
            module = Persist.GetModuleFiles();
            return module;
        }

        public bool SaveToFile(string newModuleName) {
            try {
                var sucess = Master.Instance().MyQuestion.CreateNewModuleFile(newModuleName);
                if(sucess)
                    Debug.Log("Modul erstellt und in Module.txt eingetragen");
                return sucess;
            }
            catch (Exception e) {
                Debug.LogError(e);
                return false;
            }
        }

        #region DEV-ONLY
        //ONLY DEV FUNCTION
        public void DEVReadQuestionsFromCSV(string fileName, string modul, Difficulties difficulty, string chapter) {
            //diese werte müssen "händisch" übergeben werden in diesem Fall
            var data = CSVReader.Read(fileName);
            modul = "DNIS";
            //weil es im Moment nur das gibt 
            foreach (var zeile in data) {
                try
                {
                    var q = new Question();
                    q.Modul = modul;
                    q.Answers = new List<Question.Answer>();
                    q.Chapter = chapter;
                    q.Difficulty = difficulty;
                    q.Hints = new List<string>();
                    Debug.Log("Hinweise1: " + zeile["Hinweis1"].ToString());
                    q.Hints.Add(zeile["Hinweis1"].ToString());
                    Debug.Log("Hinweise2: " + zeile["Hinweis2"].ToString());
                    q.Hints.Add(zeile["Hinweis2"].ToString());
                    Debug.Log("Hinweise3: " + zeile["Hinweis3"].ToString());
                    q.Hints.Add(zeile["Hinweis3"].ToString());

                    q.QuestionDuration = new TimeSpan(0);
                    //Debug.Log("Bildpfad-Frage: " + zeile["FragenBild"].ToString());
                    q.ImagePath = null;
                    if (!string.IsNullOrEmpty(zeile["FragenBild"].ToString()))
                    {
                        q.ImagePath = "\\Resources\\Pictures\\" + zeile["FragenBild"].ToString();
                    }

                    q.Used = false;
                    //Debug.Log("Fragentext: " + zeile["Frage"].ToString());
                    q.QuestionText = zeile["Frage"].ToString();

                    q.Answers = new List<Question.Answer>(3)
                    {
                        new Question.Answer(),
                        new Question.Answer(),
                        new Question.Answer()
                    };
                    q.Answers[0].AnswerText = null;
                    q.Answers[1].AnswerText = null;
                    q.Answers[2].AnswerText = null;

                    //Antworten Mixer
                    var valueIsSet = new bool[3] {false, false, false};
                    var randomizer = new System.Random((int) DateTime.Now.Ticks);
                    var i = 1;
                    do
                    {
                        var rdmInt = randomizer.Next(0, 3);
                        //Debug.Log("rdmint = " + rdmInt);
                        //Debug.Log("ValueIsSet[rdmInt] = " + valueIsSet[rdmInt]);
                        if (valueIsSet[rdmInt])
                        {
                            Debug.Log("Wert schon gesetzt nochmal probieren");
                            continue;
                        }
                        //Debug.Log("Antwort[" + rdmInt + "]: " + zeile["Antwort" + i.ToString()].ToString());
                        q.Answers[rdmInt].AnswerText = zeile["Antwort" + i.ToString()].ToString();
                        //Debug.Log("Antwort" + rdmInt + "Bildpfad: " + zeile["Antwort" + i.ToString() + "Bild"].ToString());
                        q.Answers[rdmInt].ImagePath = null;
                        if (!string.IsNullOrEmpty(zeile["Antwort" + i.ToString() + "Bild"].ToString()))
                        {
                            q.Answers[rdmInt].ImagePath = "\\Resources\\Pictures\\" +
                                                          zeile["Antwort" + i.ToString() + "Bild"].ToString();
                        }
                        if (i == 3) q.CorrectAnswer = rdmInt;
                        valueIsSet[rdmInt] = true;
                        i++;
                    } while (valueIsSet.Contains(false));
                    Debug.LogWarning("FRAGE ERFOLGREICH ERSTELLT");
                    Debug.Log(q);
                    Master.Instance().MyModule.AddQuestionToModule(q);
                }
                catch (KeyNotFoundException e) {
                    Debug.LogError(e);
                    Debug.Break();
                }
            }
        }
        #endregion

        public void AddQuestionToModule(Question q) {
            var moduleFile = Persist.Load<ModuleQuestions>( "Modules\\" + q.Modul);
            if (moduleFile == null) {
                throw new FileNotFoundException("Modulfile konnte nicht geladen/geöffnet werden!");
            }

            //Das Speichern sollte ab hier funktionieren - es müssen allerdings noch ein paar Werte angepasst werden:
            //Bilder hereinladen und Pfade ändern

            //Das ist doppelt beim Fragen hinzufügen wird dies von selbst aufgerufen und für den CSV import brauchen wir es nicht....

            //if (!string.IsNullOrEmpty(q.ImagePath)) { //Wenn es eine Frage-Bild gibt
            //    q.ImagePath = Persist.CopyPictureToResourcesFolder(q.ImagePath); //schreibt direkt den aktuellen Pfad herein
            //}

            //foreach (var a in q.Answers) {
            //    var answerImagePath = a.ImagePath;
            //    if (!string.IsNullOrEmpty(answerImagePath)) { //Wenn es ein Antwort-Bild gibt
            //        a.ImagePath = Persist.CopyPictureToResourcesFolder(answerImagePath); //siehe oben
            //    }
            //}

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

        private void Start()
        {
            //Initiales Laden der Module
            module = Persist.GetModuleFiles();
        }
    }
}