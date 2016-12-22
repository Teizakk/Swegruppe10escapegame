using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Models;

namespace Assets.Code.Controller
{
    public class QuestionManager
    {
        private List<Question> Questions { get; set; }
        private static readonly QuestionManager qc = new QuestionManager();
        // Use this for initialization
        private QuestionManager()
        {

        }
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public static QuestionManager GetInstance()
        {
            try
            {
                return qc;
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Das Objekt wurde nicht instanziert.", e);
            }

        }
        void Start()
        {
            Questions = new List<Question>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void setQuestions(List<Question> qs)
        {
            this.Questions = qs;
        }
        /// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public Question GetQuestionNotInUse()
        {
            try
            {
                System.Random rand = new System.Random();
                var q = Questions.Where(x => !x.Used).ToList()[rand.Next(Questions.Count)];
                q.Used = true;
                return q;
            }
            catch (IndexOutOfRangeException e)
            {
                throw new Exception("Es wurde keine unbenutze Frage gefunden", e);
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Das Objekt wurde nicht instanziert.", e);
            }
        }
        /// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public Question GetQuestionNotInUse(Difficulties d)
        {
            try
            {
                System.Random rand = new System.Random();
                var q = Questions.Where(x => !x.Used && x.Difficulty == d).ToList()[rand.Next(Questions.Count)];
                q.Used = true;
                return q;
            }
            catch (IndexOutOfRangeException e)
            {
                throw new Exception("Es wurde keine unbenutze Frage, aus der entsprechenden Difficulty gefunden", e);
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Das Objekt wurde nicht instanziert.", e);
            }
        }
        /// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public Question GetQuestionNotInUse(int level)
        {
            try
            {
                System.Random rand = new System.Random();
                var q = Questions.Where(x => !x.Used && x.Level == level).ToList()[rand.Next(Questions.Count)];
                q.Used = true;
                return q;
            }

            catch (IndexOutOfRangeException e)
            {
                throw new Exception("Es wurde keine unbenutze Frage, aus dem entsprechenden level gefunden", e);
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Das Objekt wurde nicht instanziert.", e);
            }
        }
        /// <exception cref="IndexOutOfRangeException">Es wurde keine unbenutze Frage gefunden</exception>
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public Question GetQuestionNotInUse(int level, Difficulties d)
        {
            try
            {
                System.Random rand = new System.Random();
                var q = Questions.Where(x => !x.Used && x.Level == level && x.Difficulty == d).ToList()[rand.Next(Questions.Count)];
                q.Used = true;
                return q;
            }
            catch (IndexOutOfRangeException e)
            {

                throw new Exception("Es wurde keine unbenutze Frage gefunden, aus dem entsprechenden level und der entsprechenden Difficulty gefunden.", e);
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Das Objekt wurde nicht instanziert.", e);
            }
        }
        /// <exception cref="IndexOutOfRangeException">Es wurde keine Frage gefunden</exception>
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public Question GetQuestion()
        {
            try
            {
                System.Random rand = new System.Random();
                return Questions[rand.Next(Questions.Count)];
            }
            catch (IndexOutOfRangeException e)
            {

                throw new Exception("Es wurde keine Frage gefunden.", e);
            }
        }

        public Question GetQuestionNotInUse(int level, string module, Difficulties d)
        {
            try
            {
                System.Random rand = new System.Random();
                var q = Questions.Where(x => !x.Used && x.Level == level && x.Difficulty == d && x.Modul == module).ToList()[rand.Next(Questions.Count)];
                q.Used = true;
                return q;
            }
            catch (IndexOutOfRangeException e)
            {

                throw new Exception("Es wurde keine unbenutze Frage gefunden, aus dem entsprechenden level, von diesem Modul und der entsprechenden Difficulty gefunden.", e);
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Das Objekt wurde nicht instanziert.", e);
            }
        }
        /// <exception cref="IndexOutOfRangeException">Es wurde keine Fragen gefunden mit angegebenem Index</exception>
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public Question GetQuestion(int index)
        {
            try
            {
                return Questions[index];
            }
            catch (IndexOutOfRangeException e)
            {

                throw new Exception("Es wurde keine Fragen gefunden mit angegebenem Index.", e);
            }
        }
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public IList<Question> GetQuestions()
        {
            return Questions;
        }
    }
}
