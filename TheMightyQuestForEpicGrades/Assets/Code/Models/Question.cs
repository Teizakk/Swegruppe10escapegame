using System;
using System.Collections.Generic;
using System.IO;
using Assets.Code.GLOBALS;
using UnityEngine;
using System.Text.RegularExpressions;
namespace Assets.Code.Models {

    [Serializable]
    public class Question {
        public List<Answer> Answers { get; set; }
        public string QuestionText { get; set; }

        public string ImagePath { get; set; }
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

            public string ImagePath { get; set; }
        }

        public override string ToString()
        {
            return ("Frage: " + QuestionText +
                    "\nFragenbildpfad: " + ImagePath +
                        "\nAntwort1: " + Answers[0].AnswerText +
                        "\nAntwort1Bildpfad: " + Answers[0].ImagePath +
                        "\nAntwort2: " + Answers[1].AnswerText +
                        "\nAntwort2Bildpfad: " + Answers[1].ImagePath +
                        "\nAntwort3: " + Answers[2].AnswerText +
                        "\nAntwort3Bildpfad: " + Answers[2].ImagePath +
                        "\nHinweis1: " + Hints[0] +
                        "\nHinweis2: " + Hints[1] +
                        "\nHinweis3: " + Hints[2] +
                        "\nRichtige Antwort: " + CorrectAnswer +
                        "\nModul: " + Modul +
                        "\nSchwierigkeitsgrad: " + Difficulty +
                        "\nKapitel: " + Chapter +
                        "\nGenutzt: " + Used +
                        "\nZeit: " + QuestionDuration);
        }
    }
}