using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers
{
    public class QuestionDialogController : MonoBehaviour
    {
        #region UnityObjects

        public Text TimerText;
        public GameObject popup;
        public GameObject questionDialogPopup;
        public Text popupText;
        public Text outQuestion;
        public Toggle tglAnswer1;
        public ToggleGroup toggleGroup;
        public GameObject Answers;
        public GameObject LabelTipps;
        public GameObject OutTipps;
        public List<GameObject> btnPictures;

        #endregion

        #region Properties

        private static QuestionDialogController _questionDialogController;
        private int tippsShowed;
        private int chosenAnswerIndex;
        private DateTime startTime;
        private DateTime endTime;
        private TimeSpan usedTime;
        private String[] imagePaths;
        private static Question q;
        private bool _answerCorrect = false;
        #endregion

        #region Popup Variablen

        private CanvasGroup _questionDialogCanvasGroup;
        private CanvasGroup _popupCanvasGroup;

        #endregion

        private QuestionDialogController()
        {

        }

        #region public Funktionen

        public static QuestionDialogController Instance()
        {
            if (!_questionDialogController)
            {
                _questionDialogController = GameObject.Find("QuestionDialogController").GetComponent<QuestionDialogController>();
                if (!_questionDialogController)
                    Debug.LogError(
                        "Es muss ein aktives QuestionDialogController Skript auf einem GameObject in der Scene existieren");
            }
            return _questionDialogController;
        }

        // zeigt die Frage an
        public void ShowQuestion()
        {
            blockAndUnblockMovement();

            Debug.Log("Frage anzeigen");

            // Werte initialisieren
            tippsShowed = 0;
            chosenAnswerIndex = 1;
            startTime = DateTime.Now;

            // Frage in die Felder laden
            LoadQuestion();

            // QuestionDialog anzeigen
            questionDialogPopup.SetActive(true);
        }

        // überprüft, ob die Antwort richtig war
        public bool AnswerCorrect()
        {
            return _answerCorrect;
        }

        // leitet die Frage weiter (eventuel unnötig)
        public static Question ForwardQuestion()
        {
            return q;
        }
        #endregion

        #region Scenefunktionen

        // Use this for initialization
        void Start()
        {
            // ToggleGroup initialisieren
            toggleGroup.RegisterToggle(tglAnswer1);
        }

        //Update is called once per frame
        void Update()
        {
            usedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime.Ticks);
            TimerText.text = String.Format("{0:hh\\:mm\\:ss\\:ff}", usedTime);
        }

        // ändert die ausgewählte Antwort
        public void AnswerChanged(int index)
        {
            chosenAnswerIndex = index;
            Debug.Log(chosenAnswerIndex + ". Antwort ausgewählt!");
        }

        // beantwortet die Frage
        public void AnswerQuestion()
        {
            // in beiden Fällen Zeit zur Gesamtzeit addieren
            endTime = DateTime.Now;
            usedTime = new TimeSpan(endTime.Ticks - startTime.Ticks);

            Master.Instance().MyGameState.AddTime(usedTime); //TODO gewünschter Effekt?
            Debug.Log("Zeit zur Gesamtzeit addieren");

            if (chosenAnswerIndex == q.CorrectAnswer)
            {
                _answerCorrect = true;
                Debug.Log("Frage korrekt beantwortet!");
                // Popup anzeigen
                ShowPopup("Frage korrekt beantwortet!");

                // Punkte addieren
                var punkte = 0;
                switch (q.Difficulty)
                {
                    case Difficulties.Easy:
                        punkte = 1;
                        break;
                    case Difficulties.Medium:
                        punkte = 2;
                        break;
                    case Difficulties.Hard:
                        punkte = 3;
                        break;
                }

                Master.Instance().MyGameState.AddPointsToScore(punkte);
                Debug.Log(punkte + " Punkt(e) erhalten!");
            }
            else
            {
                _answerCorrect = false;
                if (Master.Instance().MyGameState.LivesRemaining > 0)
                {
                    ShowPopup("Frage wurde falsch beantwortet!");
                    Debug.Log("Frage wurde falsch beantwortet!");
                    Debug.Log("Leben - 1");
                    Master.Instance().MyGameState.LoseOneLive();
                }
                else
                {
                    ShowPopup("Game Over!");
                    // TODO : return to Main Menu
                }
            }
        }

        // Tipp anzeigen
        public void ShowTipp()
        {
            if (tippsShowed < 3 && Master.Instance().MyGameState.HintstonesRemaining > 0)
            {
                tippsShowed++;
                Master.Instance().MyGameState.UseHintStone();

                var lblTipp = LabelTipps.GetComponentsInChildren<Text>();
                var outTipp = OutTipps.GetComponentsInChildren<Text>();
                // Tipp anzeigen
                lblTipp[tippsShowed - 1].enabled = true;
                outTipp[tippsShowed - 1].enabled = true;

                Debug.Log("Hintstein eingesetzt!");
                Debug.Log("Tipp" + tippsShowed + " wurde angezeigt");
            }
        }
        
        // Frage und Antworten in den Dialog laden
        void LoadQuestion()
        {
            q = new Question
            {
                QuestionText = "Was ist das Internet?",
                Difficulty = Difficulties.Easy,
                Chapter = "Einstieg",
                ImagePath = Path.GetFullPath("Assets/Samples+Placeholder/Beispielbild.png"),
                Answers =
                    new List<Question.Answer>()
                    {
                            new Question.Answer()
                            {
                                AnswerText = "Ein Netz",
                                ImagePath = ""
                            },
                            new Question.Answer()
                            {
                                AnswerText = "Nur physikalisch vorhanden",
                                ImagePath = "Assets/Samples+Placeholder/Bild2.png"
                            },
                            new Question.Answer()
                            {
                                AnswerText = "Ein Netz von Netzen",
                                ImagePath = ""
                            },
                    },
                CorrectAnswer = 3,
                Hints = new List<string> { "inter", "connected", "networks" }
            };
            // TODO : Frage aus der Datenhaltung holen
 			q = Master.Instance().MyQuestion.GetQuestion(0);

            // Fragentext laden
            outQuestion.text = q.QuestionText;

            imagePaths = new string[q.Answers.Count + 1];

            // Bildpfad zu Frage
            imagePaths[0] = q.ImagePath;

            // Bild vorhanden?
            if (!string.IsNullOrEmpty(imagePaths[0]) && File.Exists(Path.GetFullPath(imagePaths[0])))
            {
                // Button anzeigen
                btnPictures[0].SetActive(true);
            }

            // Antworten laden
            int i = 1;
            var outAnswer = Answers.GetComponentsInChildren<Text>().Where(x => !x.name.Equals("Text")).ToArray(); // alle Antworten holen (ohne Button-Text)
            foreach (var answer in q.Answers)
            {
                outAnswer[i - 1].text = answer.AnswerText;
                imagePaths[i] = answer.ImagePath;
                // Bild vorhanden?
                if (!string.IsNullOrEmpty(imagePaths[i]) && File.Exists(Path.GetFullPath(imagePaths[i])))
                {
                    // Button anzeigen
                    btnPictures[i].SetActive(true);
                }

                i++;
            }

            // Tipps laden
            i = 0;
            var outTipp = OutTipps.GetComponentsInChildren<Text>();
            foreach (var tipp in q.Hints)
            {
                outTipp[i].text = tipp;
                i++;
            }
        }

        #region Popups
        // TODO : update PopupController -> ?
        // zeigt Bild (ImagePopup) an
        //public void ShowPicture(int index)
        //{
        //    var popupController = GameObject.Find("PopupController").GetComponent<PopupController>();
        //    popupController.usedQuestion = q;
        //    if (index > 0)
        //    {
        //        popupController.SetUpImagePopupAnswer(tippsShowed, index - 1);
        //    }
        //    else
        //    {
        //        popupController.SetUpImagePopupQuestion(tippsShowed);
        //    }
        //    Debug.Log("Bild '" + imagePaths[index] + "' anzeigen!");
        //}

        // zeigt PopUp mit Text an für Frage richtig/falsch
        public void ShowPopup(string text)
        {
            popupText.text = text;
            popup.SetActive(true);
        }

        public void HidePopup()
        {
            popup.SetActive(false);
            // und Frage schliessen
            CloseQuestion();
        }

        private void CloseQuestion()
        {
            // die erste Antwort auswählen
            toggleGroup.ActiveToggles().FirstOrDefault().isOn = false;
            tglAnswer1.isOn = true;
            toggleGroup.NotifyToggleOn(tglAnswer1);

            // QuestionDialog schliessen
            questionDialogPopup.SetActive(false);

            var lblTipp = LabelTipps.GetComponentsInChildren<Text>();
            var outTipp = OutTipps.GetComponentsInChildren<Text>();

            // Tipps zurücksetzen
            for (int i = 0; i < lblTipp.Length; i++)
            {
                lblTipp[i].enabled = false;
                outTipp[i].enabled = false;
            }

            blockAndUnblockMovement();
        }
        #endregion

        #endregion

        #region Helper

        //"Nachrichten"-Funktion an PlayerController
        private void blockAndUnblockMovement()
        {
            PlayerScript.instance.switchControlBlock();
        }

        #endregion
    }
}