using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public enum Difficulties { Easy, Medium, Hard }
    [Serializable]
    public class Question
    {
        public List<Answer> Answers { get; set; }
        public String QuestionText { get; set; }
        public String ImagePath { get; set; }
        public List<String> Hints { get; set; }
        public Difficulties Difficulty { get; set; }
        public String Modul { get; set; }
        public Boolean inUse { get; set; }
        public int Level { get; set; }
        public int CorrectAnswer { get; set; }
        public TimeSpan QuestionDuration { get; set; }

        [Serializable]
        public class Answer
        {
            public String AnswerText { get; set; }
            private String _imgPath { get; set; }
            public String ImagePath
            {
                get
                {
                    return Path.GetFullPath(_imgPath);
                }
                set
                {
                    _imgPath = value;
                }
            }
        }
    }
}
