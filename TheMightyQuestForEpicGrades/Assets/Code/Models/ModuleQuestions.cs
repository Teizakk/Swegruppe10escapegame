using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Models {
    [Serializable]
    public class ModuleQuestions {
        public List<Question> QuestionsEasy { get; private set; }
        public List<Question> QuestionsMedium { get; private set; }
        public List<Question> QuestionsHard { get; private set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; }

        private DateTime _lastUpdated;
        public DateTime LastUpdated {
            get { return _lastUpdated; }
            set {
                if (value > _lastUpdated) {
                    _lastUpdated = value;
                }
                throw new ArgumentOutOfRangeException("Das angegebene Datum ist älter als das gespeicherte... ");
            }
        }

        public ModuleQuestions(string moduleName) {
            QuestionsEasy = new List<Question>();
            QuestionsEasy.Clear();

            QuestionsMedium = new List<Question>();
            QuestionsMedium.Clear();

            QuestionsHard = new List<Question>();
            QuestionsHard.Clear();

            Name = moduleName;
            DateCreated = DateTime.Now.ToLocalTime();
            LastUpdated = DateCreated;
        }
    }
}
