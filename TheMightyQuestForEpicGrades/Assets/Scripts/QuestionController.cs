using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class QuestionController
    {
        private List<Question> Questions { get; set; }
        private static readonly QuestionController qc = new QuestionController();
        // Use this for initialization
        private QuestionController()
        {
            Questions = Persist.LoadQuestions("QuestionJSON");
        }
        /// <exception cref="NullReferenceException">Objekt wurde nicht instanziert.</exception>
        public static QuestionController GetInstance()
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
                var q = Questions.Where(x => !x.inUse).ToList()[rand.Next(Questions.Count)];
                q.inUse = true;
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
                var q = Questions.Where(x => !x.inUse && x.Difficulty == d).ToList()[rand.Next(Questions.Count)];
                q.inUse = true;
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
                var q = Questions.Where(x => !x.inUse && x.Level == level).ToList()[rand.Next(Questions.Count)];
                q.inUse = true;
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
                var q = Questions.Where(x => !x.inUse && x.Level == level && x.Difficulty == d).ToList()[rand.Next(Questions.Count)];
                q.inUse = true;
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
