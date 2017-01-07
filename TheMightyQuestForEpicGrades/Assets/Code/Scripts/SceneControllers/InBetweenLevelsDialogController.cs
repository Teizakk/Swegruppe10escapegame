using System;
using System.Collections;
using Assets.Code.Manager;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class InBetweenLevelsDialogController : MonoBehaviour {

        #region UI-Elemente
        public Text KapitelNummer;
        public Text KapitelName;
        public Text WeiterText;
        public int waitTimeInSeconds = -1;
        #endregion

        public static bool _firstTimeUseOfScript = true;
        private bool _ableToProceed;
        private DateTime startTime;
        private TimeSpan timeToProceed;


       private void Awake () {
            //Der Player Clone darf diese Szene nicht überleben =)
            if (PlayerScript.GetInstance() != null) {
                Destroy(PlayerScript.GetInstance().gameObject);
            }
            if (_firstTimeUseOfScript)
            {
                Master.Instance().MyGameState.SetUpNextLevel(true);
                _firstTimeUseOfScript = false;
            }
            else
            {
                Master.Instance().MyGameState.SetUpNextLevel();
            }
            KapitelNummer.text = Master.Instance().MyGameState.StageCurrent.ToString();
            KapitelName.text = Master.Instance().MyGameState.ChapterInUse;
            WeiterText.enabled = false;

            startTime = DateTime.Now;
            timeToProceed = new TimeSpan(0, 0, 0, waitTimeInSeconds, 0);
        }


        private void Update() {
            if (waitTimeInSeconds == -1) { //Falls Wert nicht über Inspektor gesetzt
                waitTimeInSeconds = 2;
                timeToProceed = new TimeSpan(0, 0, 0, waitTimeInSeconds, 0);
            }
            if (!_ableToProceed) {
                //Debug.Log(DateTime.Now - startTime <= timeToProceed);
                if (DateTime.Now - startTime <= timeToProceed) return;
                WeiterText.enabled = true;
                _ableToProceed = true;
                return;
            }

            if (Input.anyKeyDown) {
                GoToNextStage();
            }
        }

        private void GoToNextStage() {
            SceneManager.LoadScene("MainGame");
        }
	
        #region Master-Link
        private void Start() {
            Master.Instance().CurrentDialogController = this.gameObject;
        }

        private void OnDestroy() {
            Master.Instance().CurrentDialogController = null;
        }
        #endregion
    }
}
