using System;
using System.Collections.Generic;
using System.IO;
using Assets.Code.GLOBALS;

namespace Assets.Code.Models {

    [Serializable]
    public class Question {
        public List<Answer> Answers { get; set; }
        public string QuestionText { get; set; }
        private string _imgPath { get; set; }

        public string ImagePath {
            get {
                if (!string.IsNullOrEmpty(_imgPath))
                    return Path.GetFullPath(_imgPath);
                return _imgPath;
            }
            set { _imgPath = value; }
        }

        public List<string> Hints { get; set; }
        public Difficulties Difficulty { get; set; }
        public string Modul { get; set; }
        public bool Used { get; set; }
        public string Chapter { get; set; }
        public int CorrectAnswer { get; set; }
        public TimeSpan QuestionDuration { get; set; }

        [Serializable]
        public class Answer {
            public string AnswerText { get; set; }
            private string _imgPath { get; set; }

            public string ImagePath {
                get {
                    if (!string.IsNullOrEmpty(_imgPath))
                        return Path.GetFullPath(_imgPath);
                    return _imgPath;
                }
                set { _imgPath = value; }
            }
        }
    }
}