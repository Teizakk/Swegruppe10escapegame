using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class QuestionController
    {
        private List<Question> Questions { get; set; }
        // Use this for initialization
        void Start()
        {
            Questions = new List<Question>();
        }

        // Update is called once per frame
        void Update()
        {
        }
        void setQuestions(List<Question> qs)
        {
            Questions = qs;
        }
        Question getQuestionNotInUse()
        {
            System.Random rand = new System.Random();
            var q = Questions.Where(x => !x.inUse).ToList()[rand.Next(Questions.Count)];
            q.inUse = true;
            return q;
        }
        Question getQuestionNotInUse(Difficulties d)
        {
            System.Random rand = new System.Random();
            var q = Questions.Where(x => !x.inUse && x.Difficulty == d).ToList()[rand.Next(Questions.Count)];
            q.inUse = true;
            return q;
        }
        Question getQuestionNotInUse(int level)
        {
            System.Random rand = new System.Random();
            var q = Questions.Where(x => !x.inUse && x.Level == level).ToList()[rand.Next(Questions.Count)];
            q.inUse = true;
            return q;
        }
        Question getQuestionNotInUse(int level, Difficulties d)
        {
            System.Random rand = new System.Random();
            var q = Questions.Where(x => !x.inUse && x.Level == level && x.Difficulty == d).ToList()[rand.Next(Questions.Count)];
            q.inUse = true;
            return q;
        }
        Question getQuestion()
        {
            System.Random rand = new System.Random();
            return Questions[rand.Next(Questions.Count)];
        }
    }
}
