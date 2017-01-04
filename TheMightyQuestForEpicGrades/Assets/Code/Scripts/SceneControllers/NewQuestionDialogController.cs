using System;
using System.Collections.Generic;
using System.IO;
using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Windows.Forms;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;

namespace Assets.Code.Scripts.SceneControllers
{
    public class NewQuestionDialogController : MonoBehaviour
    {
        #region UnityObjects

        public Text inQuestion;
        public Text inChapter;
        public List<Text> answers;
        public List<Text> hints;

        #endregion

        #region properties

        private string _imagePath;
        private List<Question.Answer> _answers = new List<Question.Answer>(3);
        private List<string> _hints = new List<string>(3);

        #endregion

        private int _correctAnswerIndex = 1;


        // Use this for initialization
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddQuestion()
        {
            for (int i = 0; i < this.answers.Count; i++)
            {
                _answers[i].AnswerText = this.answers[i].text;
                _hints[i] = this.hints[i].text;
            }
            var q = new Question()
            {
                QuestionText = inQuestion.text,
                ImagePath = _imagePath,
                Chapter = inChapter.text,
                Modul = Master.Instance().MyModule.ModuleToEdit,
                Answers = _answers,
                Hints = _hints,
                CorrectAnswer = _correctAnswerIndex
            };

            // TODO : in Persist speichern
        }

        public void AddBild(int index)
        {
            // TODO : funktioniert so nicht
            // Dialog anzeigen
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users\%USERNAME%");
            Debug.Log(open.InitialDirectory);
            open.Filter =
                "png Dateien (*.png)|*.png|jpg Dateien (*.jpg)|*.jpg|bitmap (*.bmp)|*.bmp|jpeg (*.jpeg)|*.jpeg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (index > 0)
                {
                    _answers[index - 1].ImagePath = Path.Combine(open.InitialDirectory, open.FileName);
                }
                else
                {
                    _imagePath = Path.Combine(open.InitialDirectory, open.FileName);
                }
            }
        }

        // ändert die richtige Antwort
        public void CorrectAnswerChanged(int index)
        {
            _correctAnswerIndex = index;
            Debug.Log(_correctAnswerIndex + ". Antwort ausgewählt!");
        }

        #region Master-Link
        private void Start()
        {
            Master.Instance().CurrentDialogController = this.gameObject;
        }

        private void OnDestroy()
        {
            Master.Instance().CurrentDialogController = null;
        }
        #endregion
    }
}
