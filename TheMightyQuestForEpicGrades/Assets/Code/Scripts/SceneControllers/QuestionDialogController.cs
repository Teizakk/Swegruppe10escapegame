using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
        public List<Outline> tglAnswers;
        public List<GameObject> btnPictures;

        #endregion

        #region Properties

        private static QuestionDialogController _questionDialogController;
        private int tippsShowed;
        private int chosenAnswerIndex;
        public DateTime startTime;
        private DateTime endTime;
        public TimeSpan usedTime;
        private String[] imagePaths;
        private static Question q;
        private bool _answerCorrect;
        private bool _updateTimer;
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
                _questionDialogController = FindObjectOfType(typeof(QuestionDialogController)) as QuestionDialogController;
                if (!_questionDialogController)
                    Debug.LogError(
                        "Es muss ein aktives QuestionDialogController Skript auf einem GameObject in der Scene existieren");
            }
            return _questionDialogController;
        }

        // zeigt die Frage an
        public void ShowQuestion()
        {
            FindObjectOfType<PauseMenuScript>().BlockUnblockPauseMenu();

            blockAndUnblockMovement();

            Debug.Log("Frage anzeigen");

            // Werte initialisieren
            tippsShowed = 0;
            chosenAnswerIndex = 0;
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
        void Awake()
        {
            // ToggleGroup initialisieren
            toggleGroup.RegisterToggle(tglAnswer1);
        }

        //Update is called once per frame
        void Update()
        {
            usedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime.Ticks);
            if (_updateTimer) {
                //TimerText.text = String.Format("{0:hh\\:mm\\:ss\\:ff}", usedTime);
                TimerText.text = String.Format("{0:hh\\:mm\\:ss}", usedTime);
            }
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
            _updateTimer = false;

            FindObjectOfType<PauseMenuScript>().BlockUnblockPauseMenu();

            // "Beantworten" button blockieren
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);

            //imagepaths zurücksetzen
            imagePaths[0] = null;
            btnPictures[0].SetActive(false);
            imagePaths[1] = null;
            btnPictures[1].SetActive(false);
            imagePaths[2] = null;
            btnPictures[2].SetActive(false);
            imagePaths[3] = null;
            btnPictures[3].SetActive(false);

            Master.Instance().MyGameState.AddTime(usedTime);
            Debug.Log("Zeit zur Gesamtzeit addieren");

            if (chosenAnswerIndex == q.CorrectAnswer)
            {
                _answerCorrect = true;

                // Success Sound
                System.Media.SystemSounds.Beep.Play();
                // Popup anzeigen
                ShowPopup("Frage korrekt beantwortet!");
             
                Debug.Log(Master.Instance().MyGameState.AddPointsToScore(usedTime) + " Punkt(e) erhalten!");
                DrawStone();
            }
            else
            {
                _answerCorrect = false;
                // Error Sound
                System.Media.SystemSounds.Hand.Play();
		        if (Master.Instance().MyGameState.LivesRemaining > 0)
                {
                    ShowPopup("Frage wurde falsch beantwortet!");
                    Debug.Log("Frage wurde falsch beantwortet!");
                    Debug.Log("Leben - 1");
                    Master.Instance().MyGameState.LoseOneLive();
                    //Master.Instance().MyGameState.CloseChest(_answerCorrect);
                }
            }
        }

        private void DrawStone() {
            //Stein vergeben
            var AnzahlHints = Master.Instance().MyGameState.anzahlHinweissteine;
            var portal1 = Master.Instance().MyGameState.portalStein1;
            var portal2 = Master.Instance().MyGameState.portalStein2;
            var portal3 = Master.Instance().MyGameState.portalStein3;
            System.Random rnd = Master.Instance().MyGameState.rnd;

            int zahl = rnd.Next(1, 12);
            //Hintstone
            if (zahl != 5 && zahl != 10)
            {
                if (AnzahlHints > 0)
                {
                    AnzahlHints--;
                    //Gebe dem spieler einen Hintstone
                    Master.Instance().MyGameState.WinHintstone();
                    Master.Instance().MyGameState.anzahlHinweissteine = AnzahlHints;
                    Debug.Log("hinweistein");
                }
                else
                {
                    bool gezogen = false;

                    while (!gezogen)
                    {
                        int stone = rnd.Next(1, 4);
                        if (stone == 1 && portal1 == 1 && gezogen == false)
                        {
                            portal1--;
                            gezogen = true;
                            //Gebe dem Spieler Portalstein1
                            Master.Instance().MyGameState.WinPortalStone(PortalColor.Pink);
                            Master.Instance().MyGameState.portalStein1 = 0;
                            Debug.Log("portalstein1");
                        }
                        if (stone == 2 && portal2 == 1 && gezogen == false)
                        {
                            portal2--;
                            gezogen = true;
                            //Gebe dem Spieler Portalstein2
                            Master.Instance().MyGameState.WinPortalStone(PortalColor.Blue);
                            Master.Instance().MyGameState.portalStein2 = 0;
                            Debug.Log("portalstein2");
                        }
                        if (stone == 3 && portal3 == 1 && gezogen == false)
                        {
                            portal3--;
                            gezogen = true;
                            Master.Instance().MyGameState.WinPortalStone(PortalColor.Green);
                            Master.Instance().MyGameState.portalStein3 = 0;
                            Debug.Log("portalstein3");
                        }
                        if (portal1 == 0 && portal2 == 0 && portal3 == 0)
                        {
                            //keine steine mehr vorhanden
                            Debug.Log("keine steine mehr vorhanden");
                            break;
                        }
                    }
                }
            }
            else
            {
                bool gezogen = false;

                while (!gezogen)
                {
                    int stone = rnd.Next(1, 4);
                    if (stone == 1 && portal1 == 1 && gezogen == false)
                    {
                        portal1--;
                        gezogen = true;
                        //Gebe dem Spieler Portalstein1
                        Master.Instance().MyGameState.WinPortalStone(PortalColor.Pink);
                        Master.Instance().MyGameState.portalStein1 = 0;
                        Debug.Log("portalstein1");
                    }
                    if (stone == 2 && portal2 == 1 && gezogen == false)
                    {
                        portal2--;
                        gezogen = true;
                        //Gebe dem Spieler Portalstein2
                        Master.Instance().MyGameState.WinPortalStone(PortalColor.Blue);
                        Master.Instance().MyGameState.portalStein2 = 0;
                        Debug.Log("portalstein2");
                    }
                    if (stone == 3 && portal3 == 1 && gezogen == false)
                    {
                        portal3--;
                        gezogen = true;
                        Master.Instance().MyGameState.WinPortalStone(PortalColor.Green);
                        Master.Instance().MyGameState.portalStein3 = 0;
                        //Gebe dem Spieler Portalstein3
                        Debug.Log("portalstein3");
                    }
                    else if (portal1 == 0 && portal2 == 0 && portal3 == 0)
                    {
                        if (AnzahlHints > 0)
                        {
                            AnzahlHints--;
                            gezogen = true;
                            //Gebe Hinweisstein
                            Master.Instance().MyGameState.WinHintstone();
                            Master.Instance().MyGameState.anzahlHinweissteine = AnzahlHints;
                            Debug.Log("hinweistein");
                        }
                        else
                        {
                            //keine Steine mehr vorhanden
                            Debug.Log("keine steine mehr vorhanden");
                            break;
                        }
                    }
                }
            }
            Master.Instance().MyGameState.CloseChest(_answerCorrect);
        }

        // Tipp anzeigen
        public void ShowTipp()
        {
            if (tippsShowed < 3 && Master.Instance().MyGameState.HintstonesRemaining > 0)
            {
                tippsShowed++;

                var lblTipp = LabelTipps.GetComponentsInChildren<Text>();
                var outTipp = OutTipps.GetComponentsInChildren<Text>();
                // Tipp anzeigen
                lblTipp[tippsShowed - 1].enabled = true;
                outTipp[tippsShowed - 1].enabled = true;

                Master.Instance().MyGameState.UseHintStone();

                Debug.Log("Hintstein eingesetzt!");
                Debug.Log("Tipp" + tippsShowed + " wurde angezeigt");
            }
        }

        // Frage und Antworten in den Dialog laden
        void LoadQuestion()
        {
            /*q = new Question {
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
              };*/

            //Neue Fragen laden
            q = Master.Instance().MyQuestion.ProvideUnusedQuestion(Master.Instance().MyGameState.ChapterInUse);

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

            // CheatMode : richtige Antwort markieren
            if (Master.Instance().MyGameState.CheatmodeActive)
            {
                tglAnswers[q.CorrectAnswer].enabled = true;
            }

            // Tipps laden
            i = 0;
            var outTipp = OutTipps.GetComponentsInChildren<Text>();
            foreach (var tipp in q.Hints)
            {
                outTipp[i].text = tipp;
                i++;
            }
            //Timer auf soll-geupdated-werden setzen
            _updateTimer = true;
        }

        #region Popups
        //zeigt Bild(ImagePopup) an
        public void ShowPicture(int index)
        {
            var imagePopup = Master.Instance().CurrentDialogController.GetComponent<ImagePopupScript>();
            //var imagePopup = ImagePopupScript.Instance();
            imagePopup.usedQuestion = q;
            if (index > 0)
            {
                imagePopup.SetUpImagePopupAnswer(tippsShowed, index - 1);
            }
            else
            {
                imagePopup.SetUpImagePopupQuestion(tippsShowed);
            }
            Debug.Log("Bild '" + imagePaths[index] + "' anzeigen!");
        }

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

            // Cheatmodus : zuletzt markierte richtige Antwort deaktivieren
            if (Master.Instance().MyGameState.CheatmodeActive)
            {
                tglAnswers[q.CorrectAnswer].enabled = false;
            }

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
            PlayerScript.GetInstance().SwitchControlBlock();
        }

        private void LeaveToMainMenu()
        {
            Master.KILLME();
            SceneManager.LoadScene("MainMenu");
        }

        #endregion

        #region Master-Link
        private void Start()
        {
            Master.Instance().CurrentDialogController = this.gameObject;
        }
        #endregion
    }
}