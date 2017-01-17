using System;
using System.Collections.Generic;
using System.IO;
using Assets.Code.GLOBALS;
using UnityEngine;

namespace Assets.Code.Models {

    [Serializable]
    public class Question {
        public List<Answer> Answers;
        public string QuestionText;
        public string ImagePath;
        public List<string> Hints;
        public Difficulties Difficulty;
        public string Modul;
        public bool Used;
        public string Chapter;
        public int CorrectAnswer;
        public TimeSpan QuestionDuration;

        [Serializable]
        public class Answer {
            public string AnswerText;
            public string ImagePath;
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