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

		private bool HasEnoughQuestionsInChapter(Dictionary<string, int>.ValueCollection vc, int QuestionCount, int enough) {
			int x = 0;

			foreach (int n in vc) {
				if (n >= QuestionCount)
					++x;
			}
				
			return (x >= enough);
		}

		public bool HasEnoughQuestionsPerChapter() {
			Dictionary<string, int> chaptersEasy = new Dictionary<string, int> ();
			Dictionary<string, int> chaptersMedium = new Dictionary<string, int> ();
			Dictionary<string, int> chaptersHard = new Dictionary<string, int> ();
			chaptersEasy.Clear ();
			chaptersMedium.Clear ();
			chaptersHard.Clear ();

			int max = Math.Max (QuestionsEasy.Count, Math.Max (QuestionsMedium.Count, QuestionsHard.Count));
			for (int i = 0; i < max; ++i) {
				if (i < QuestionsEasy.Count) {
					if (chaptersEasy.ContainsKey (QuestionsEasy [i].Chapter))
						chaptersEasy [QuestionsEasy [i].Chapter] += 1;
					else {
						chaptersEasy.Add (QuestionsEasy [i].Chapter, 1);
					}
				}

				if (i < QuestionsMedium.Count) {
					if (chaptersMedium.ContainsKey (QuestionsMedium [i].Chapter))
						chaptersMedium [QuestionsMedium [i].Chapter] += 1;
					else {
						chaptersMedium.Add (QuestionsMedium [i].Chapter, 1);
					}
				}

				if (i < QuestionsHard.Count) {
					if (chaptersHard.ContainsKey (QuestionsHard [i].Chapter))
						chaptersHard [QuestionsHard [i].Chapter] += 1;
					else {
						chaptersHard.Add (QuestionsHard [i].Chapter, 1);
					}
				}
			}

			return (HasEnoughQuestionsInChapter (chaptersEasy.Values, 10, 3) &&
					HasEnoughQuestionsInChapter (chaptersMedium.Values, 10, 3) &&
					HasEnoughQuestionsInChapter (chaptersHard.Values, 10, 3));
		}

		public bool HasEnoughQuestionsPerDifficulty() {
			bool ok = false;

			if (QuestionsEasy.Count >= 30 && QuestionsMedium.Count >= 30 && QuestionsHard.Count >= 30)
				ok = true;

			return ok;
		}

        public bool HasEnoughQuestions() {
            //return (GetCombinedNumberOfQuestions() >= 90);
			return (HasEnoughQuestionsPerDifficulty() && HasEnoughQuestionsPerChapter());
        }
    }
}
