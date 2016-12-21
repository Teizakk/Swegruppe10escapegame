using Assets.Models;
using UnityEngine;

//TODO benutzt?
namespace Assets.Scripts.FeatureScripts
{
    public class PopupController : MonoBehaviour
    {
        private ImagePopupScript _imagePopupScript;
        public Question usedQuestion { get; set; }

        private void Awake()
        {
            Debug.Log("Awake on PopupController called");
            _imagePopupScript = ImagePopupScript.Instance();
        }

        //Das ImagePopupScript mit Daten füttern um es zu testen
        public void TestImagePopupRaw()
        {
            var display_text = "Dies ist eine Frage";
            var image_path = "~/Assets/Samples+Placeholder/Beispielbid.png";
            var Hint1Text = "Ich bin Hinweis 1";
            var Hint2Text = "Ich bin Hinweis 2";
            var Hint3Text = "Ich bin Hinweis 3";

            _imagePopupScript.ConfigureAndShow(display_text, image_path, Hint1Text, Hint2Text, Hint3Text);
            //_imagePopupScript.ConfigureAndShow(display_text, image_path);
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
    }
}