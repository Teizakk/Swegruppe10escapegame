using System;
using System.Collections.Generic;
using System.IO;
using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Assets.Code.GLOBALS;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;

namespace Assets.Code.Scripts.SceneControllers
{
    public class NewQuestionDialogController : MonoBehaviour
    {
        #region UnityObjects

        public Text lblChosenModule;
        public Text inQuestion;
        public Text inChapter;
        public List<Text> answers;
        public List<Text> hints;
        public Toggle tglAnswer1;
        public ToggleGroup toggleGroup;
        public Dropdown dpdDifficulty;
        #endregion

        #region properties

        private string _imagePath;
        private List<Question.Answer> _answers = null;
        private List<string> _hints = null;

        [DllImport("user32.dll")]
        private static extern void OpenFileDialog();

        private OpenFileDialog ofd;

        #endregion

        private int _correctAnswerIndex = 1;


        // Use this for initialization
        void Awake()
        {
            lblChosenModule.text = Master.Instance().MyModule.ModuleToEdit;  // sollte so klappen
            
            // ToggleGroup initialisieren
            toggleGroup.RegisterToggle(tglAnswer1);

            /* answers und hints initialisieren */
            if (_answers == null)
            {
                _answers = new List<Question.Answer>(answers.Count);
                for (int i = 0; i < answers.Count; i++)
                {
                    _answers.Add(new Question.Answer());
                }
            }
            if (_hints == null)
            {
                _hints = new List<string>(hints.Count);
                for (int i = 0; i < hints.Count; i++)
                {
                    _hints.Add(string.Empty);
                }
            }
        }
        

        public void AddQuestion()
        {
            if (IsOneFieldEmpty())
            {
                MessageBox.Show("Alle Felder müssen ausgefüllt werden.");
                return;
            }

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
                Difficulty = (Difficulties) dpdDifficulty.value + 1,
                CorrectAnswer = _correctAnswerIndex
            };
            Debug.Log("ImagePath: " + q.ImagePath);
            Debug.Log("Answer ImagePath: " + q.Answers[0].ImagePath);

            //var list = Persist.Load<List<Question>>("Module\\" + q.Modul);
            //if(list == null)
            //    list = new List<Question>();
            //list.Add(q);
            //Persist.Save(list, "Module\\" + q.Modul);
            
            Master.Instance().MyModule.AddQuestionToModule(q);

            MessageBox.Show("Die Frage wurde erfolgreich hinzugefügt.");
            ClearFields();
            // die erste Antwort auswählen
            toggleGroup.ActiveToggles().FirstOrDefault().isOn = false;
            tglAnswer1.isOn = true;
            toggleGroup.NotifyToggleOn(tglAnswer1);
        }

        public void AddBild(int index)
        {
            /* Dialog anzeigen */
            if (ofd == null)
            {
                ofd = new OpenFileDialog()
                {
                    InitialDirectory = Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users\%USERNAME%"),
                    Filter =
                        "png Dateien (*.png)|*.png|jpg Dateien (*.jpg)|*.jpg|bitmap (*.bmp)|*.bmp|jpeg Dateien (*.jpeg)|*.jpeg",
                    AutoUpgradeEnabled = true,
                    Multiselect = false,
                    RestoreDirectory = true
                };
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (index > 0)
                {
                    _answers[index - 1].ImagePath = Path.Combine(ofd.InitialDirectory, ofd.FileName);
                }
                else
                {
                    _imagePath = Path.Combine(ofd.InitialDirectory, ofd.FileName);
                }
            }
        }

        // ändert die richtige Antwort
        public void CorrectAnswerChanged(int index)
        {
            _correctAnswerIndex = index;
            Debug.Log(_correctAnswerIndex + ". Antwort ausgewählt!");
        }

        private bool IsOneFieldEmpty()
        {
            if (inQuestion.text.Equals(string.Empty) /*|| inChapter.text.Equals(string.Empty)*/)
                return true;
            for (int i = 0; i < answers.Count; i++)
            {
                if (answers[i].text.Equals(string.Empty) || hints[i].text.Equals(string.Empty))
                    return true;
            }
            return false;
        }

        private void ClearFields()
        {
            inQuestion.text = string.Empty;
            inChapter.text = string.Empty;
            for(int i = 0; i < answers.Count;i++)
            {
                answers[i].text = string.Empty;
                hints[i].text = string.Empty;
            }
            
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
