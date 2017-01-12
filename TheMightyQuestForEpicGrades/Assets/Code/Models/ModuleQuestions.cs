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
                if (value.Ticks >= _lastUpdated.Ticks) {
                    _lastUpdated = value;
                }
                Debug.LogWarning("Wert der eingetragen werden soll: " + value + "\nWert der drin steht: " + _lastUpdated);
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
            _lastUpdated = DateCreated;
        }

        public ModuleQuestions() {
            // nur benötigt um über Load herein zu laden
        }

        public int GetCombinedNumberOfQuestions() {
            return (QuestionsEasy.Count + QuestionsMedium.Count + QuestionsHard.Count);
        }

        public bool HasEnoughQuestions() {
            return (GetCombinedNumberOfQuestions() >= 90);
        }
    }
}
