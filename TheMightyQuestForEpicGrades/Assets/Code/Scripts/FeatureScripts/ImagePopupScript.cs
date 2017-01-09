using System;
using System.Diagnostics;
using System.IO;
using Assets.Code.Models;
using Assets.Code.Scripts.SceneControllers;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

//TODO ich glaube hier kann man das Singleton-Pattern entfernen

namespace Assets.Code.Scripts.FeatureScripts
{
    public class ImagePopupScript : MonoBehaviour
    {
        public Text TimerText;
        private DateTime startTime;
        private TimeSpan usedTime;

        private static ImagePopupScript _imagePopupScript;
        public Question usedQuestion { get; set; }
        public Button BackButton;
        public Text Hint1Text;
        public Text Hint2Text;
        public Text Hint3Text;

        public GameObject imagePopupObject;
        public RawImage QOrAImage;
        public Text QOrAText;

        //modifiziertes Singleton-Pattern
        public static ImagePopupScript Instance()
        {
            if (!_imagePopupScript)
            {
                _imagePopupScript = FindObjectOfType(typeof(ImagePopupScript)) as ImagePopupScript;
                if (!_imagePopupScript)
                    Debug.LogError(
                        "Es muss ein aktives ImagePopupScript Skript auf einem GameObject in der Scene existieren");
            }
            return _imagePopupScript;
        }

        //Update is called once per frame
        void Update()
        {
            usedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime.Ticks);
            TimerText.text = String.Format("{0:hh\\:mm\\:ss\\:ff}", usedTime);
        }

        //Funktionen
        public void ConfigureAndShow(
            string q_or_a_text,
            string q_or_a_image_path,
            string hint_1_text = "",
            string hint_2_text = "",
            string hint_3_text = "")
        {
            // Timer initialisieren
            startTime = QuestionDialogController.Instance().startTime;
            usedTime = QuestionDialogController.Instance().usedTime;

            //Fenster aktivieren
            imagePopupObject.SetActive(true);

            //BackButton konfigurieren
            BackButton.onClick.RemoveAllListeners();
            BackButton.onClick.AddListener(closePopup); //müsste so gehen

            //Elemente setzen / anzeigen lassen
            QOrAText.gameObject.SetActive(false);
            QOrAText.text = q_or_a_text;
            QOrAText.gameObject.SetActive(true); //Könnte sein, dass man das nicht braucht
            BackButton.gameObject.SetActive(true);

            //Bild konfigurieren
            if (File.Exists(q_or_a_image_path))
            {
                var bytes = File.ReadAllBytes(q_or_a_image_path);
                var new_texture = new Texture2D(15, 15);
                //new_texture.filterMode
                new_texture.LoadImage(bytes);
                QOrAImage.texture = new_texture;
                QOrAImage.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Die Bilddatei gibt es nicht.");
            }


            //Prüfen ob Hints mitgegeben wurden
            if (hint_1_text == "")
            {
                Hint1Text.text = "DONTSHOWME_1";
                Hint1Text.gameObject.SetActive(false);
            }
            else
            {
                Hint1Text.text = "Hinweis 1: " + hint_1_text;
                Hint1Text.gameObject.SetActive(true);
            }

            if (hint_2_text == "")
            {
                Hint2Text.text = "DONTSHOWME_2";
                Hint2Text.gameObject.SetActive(false);
            }
            else
            {
                Hint2Text.text = "Hinweis 2: " + hint_2_text;
                Hint2Text.gameObject.SetActive(true);
            }

            if (hint_3_text == "")
            {
                Hint3Text.text = "DONTSHOWME_3";
                Hint3Text.gameObject.SetActive(false);
            }
            else
            {
                Hint3Text.text = "Hinweis 3: " + hint_3_text;
                Hint3Text.gameObject.SetActive(true);
            }
        }

        public void SetUpImagePopupQuestion(int hintsToShow)
        {
            switch (hintsToShow)
            {
                case 0:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath);
                    break;
                case 1:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath,
                        usedQuestion.Hints[0]);
                    break;
                case 2:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath,
                        usedQuestion.Hints[0], usedQuestion.Hints[1]);
                    break;
                case 3:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath,
                        usedQuestion.Hints[0], usedQuestion.Hints[1], usedQuestion.Hints[2]);
                    break;
            }
        }

        public void SetUpImagePopupAnswer(int hintsToShow, int index)
        {
            switch (hintsToShow)
            {
                case 0:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.Answers[index].AnswerText,
                        usedQuestion.Answers[index].ImagePath);
                    break;
                case 1:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.Answers[index].AnswerText,
                        usedQuestion.Answers[index].ImagePath, usedQuestion.Hints[0]);
                    break;
                case 2:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.Answers[index].AnswerText,
                        usedQuestion.Answers[index].ImagePath, usedQuestion.Hints[0], usedQuestion.Hints[1]);
                    break;
                case 3:
                    _imagePopupScript.ConfigureAndShow(usedQuestion.Answers[index].AnswerText,
                        usedQuestion.Answers[index].ImagePath, usedQuestion.Hints[0], usedQuestion.Hints[1],
                        usedQuestion.Hints[2]);
                    break;
            }
        }

        private void closePopup()
        {
            imagePopupObject.SetActive(false);
        }
    }
}