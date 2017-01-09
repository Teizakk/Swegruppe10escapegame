using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.GLOBALS;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using Random = System.Random;

namespace Assets.Code.Manager {
    public class QuestionManager : MonoBehaviour {

        //TODO REWORK

        private List<Question> Questions { get; set; } //Wenn geladen nur enthält dies nur Fragen für das aktuell gewählte Modul
        private QuestionManager()
        {
            // TODO : wäre das nicht geeignet für Module bzw. Fragen?
            //Questions = ModuleManager.LoadQuestionsFromModul(GameStateManager.Instance().GameStateObject.GameOptions.Modul);
        }
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public static QuestionManager GetInstance() {
        //    try {
        //        return qc;
        //    }
        //    catch (NullReferenceException e) {
        //        throw new Exception("Das Objekt wurde nicht instanziert.", e);
        //    }
        //}

        //private void Start() {
        //    Questions = new List<Question>();
        //}


        //public void setQuestions(List<Question> qs) {
        //    Questions = qs;
        //}

        ///// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        ///// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public Question GetQuestionNotInUse() {
        //    try {
        //        var rand = new Random();
        //        var q = Questions.Where(x => !x.Used).ToList()[rand.Next(Questions.Count)];
        //        q.Used = true;
        //        return q;
        //    }
        //    catch (IndexOutOfRangeException e) {
        //        throw new Exception("Es wurde keine unbenutze Frage gefunden", e);
        //    }
        //    catch (NullReferenceException e) {
        //        throw new Exception("Das Objekt wurde nicht instanziert.", e);
        //    }
        //}

        ///// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        ///// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public Question GetQuestionNotInUse(Difficulties d) {
        //    try {
        //        var rand = new Random();
        //        var q = Questions.Where(x => !x.Used && (x.Difficulty == d)).ToList()[rand.Next(Questions.Count)];
        //        q.Used = true;
        //        return q;
        //    }
        //    catch (IndexOutOfRangeException e) {
        //        throw new Exception("Es wurde keine unbenutze Frage, aus der entsprechenden Difficulty gefunden", e);
        //    }
        //    catch (NullReferenceException e) {
        //        throw new Exception("Das Objekt wurde nicht instanziert.", e);
        //    }
        //}

        ///// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        ///// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public Question GetQuestionNotInUse(int level) {
        //    try {
        //        var rand = new Random();
        //        //TODO FIX var q = Questions.Where(x => !x.Used && (x.Chapter == level)).ToList()[rand.Next(Questions.Count)];
        //        Question q = new Question(); //BULLSHIT COMPILER FIX
        //        q.Used = true;
        //        return q;
        //    }

        //    catch (IndexOutOfRangeException e) {
        //        throw new Exception("Es wurde keine unbenutze Frage, aus dem entsprechenden level gefunden", e);
        //    }
        //    catch (NullReferenceException e) {
        //        throw new Exception("Das Objekt wurde nicht instanziert.", e);
        //    }
        //}

        ///// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        ///// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public Question GetQuestionNotInUse(int level, Difficulties d) {
        //    try {
        //        var rand = new Random();
        //        //TODO FIX var q = Questions.Where(x => !x.Used && (x.Chapter == level) && (x.Difficulty == d)).ToList()[rand.Next(Questions.Count)];
        //        Question q = new Question(); //BULLSHIT COMPILER FIX
        //        q.Used = true;
        //        return q;
        //    }
        //    catch (IndexOutOfRangeException e) {
        //        throw new Exception(
        //            "Es wurde keine unbenutze Frage gefunden, aus dem entsprechenden level und der entsprechenden Difficulty gefunden.",
        //            e);
        //    }
        //    catch (NullReferenceException e) {
        //        throw new Exception("Das Objekt wurde nicht instanziert.", e);
        //    }
        //}

        ///// <exception cref="IndexOutOfRangeException">Es wurde keine Frage gefunden</exception>
        ///// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public Question GetQuestion() {
        //    try {
        //        var rand = new Random();
        //        return Questions[rand.Next(Questions.Count)];
        //    }
        //    catch (IndexOutOfRangeException e) {
        //        throw new Exception("Es wurde keine Frage gefunden.", e);
        //    }
        //}

        //public Question GetQuestionNotInUse(int level, string module, Difficulties d) {
        //    try {
        //        var rand = new Random();
        //        //TODO FIX var q = Questions.Where(x => !x.Used && (x.Chapter == level) && (x.Difficulty == d) && (x.Modul == module)).ToList()[rand.Next(Questions.Count)];
        //        Question q = new Question(); //BULLSHIT COMPILER FIX
        //        q.Used = true;
        //        return q;
        //    }
        //    catch (IndexOutOfRangeException e) {
        //        throw new Exception(
        //            "Es wurde keine unbenutze Frage gefunden, aus dem entsprechenden level, von diesem Modul und der entsprechenden Difficulty gefunden.",
        //            e);
        //    }
        //    catch (NullReferenceException e) {
        //        throw new Exception("Das Objekt wurde nicht instanziert.", e);
        //    }
        //}

        //internal void LoadQuestionsByChapter(string chapterInUse) {
        //    throw new NotImplementedException();
        //}

        ///// <exception cref="IndexOutOfRangeException">Es wurde keine Fragen gefunden mit angegebenem Index</exception>
        ///// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public Question GetQuestion(int index) {
        //    try {
        //        return Questions[index];
        //    }
        //    catch (IndexOutOfRangeException e) {
        //        throw new Exception("Es wurde keine Fragen gefunden mit angegebenem Index.", e);
        //    }
        //}

        ///// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        //public IList<Question> GetQuestions() {
        //    return Questions;
        //}

        public string[] GetAllChapters() {
            //TODO GetAllChapters
            return new string[] {"Test1", "Test2", "Test3"};
        }

        public bool CreateNewModuleFile(string newModuleName) {
            //.dat Datei zur Speicherung der Fragen anlegen
            var moduleSaveFile = new ModuleQuestions(newModuleName);
            Persist.Save(moduleSaveFile, "\\Modules" + newModuleName);
            return Persist.GetModuleFiles().Contains(newModuleName);
        }

    }
}
