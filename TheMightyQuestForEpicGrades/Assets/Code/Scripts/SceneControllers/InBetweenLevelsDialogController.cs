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
        public float waitTimeInSeconds = -1.0f;
        #endregion

        public static bool _firstTimeUseOfScript = true;
        public static bool _loadingASaveGame = false;
        private bool _ableToProceed;
        private DateTime startTime;
        private TimeSpan timeToProceed;


        private void Awake() {
            //Der Player Clone darf diese Szene nicht überleben =)
            if (PlayerScript.GetInstance() != null) {
                Destroy(PlayerScript.GetInstance().gameObject);
            }

            if (_firstTimeUseOfScript) {
                if (_loadingASaveGame) { //Spiel laden
                    Debug.Log("keine neuen Werte und nichts wegspeichern");
                    Master.Instance().MyGameState.SetUpNextLevel(false, false);
                    _loadingASaveGame = false;
                }
                else { //Neuer Spielstart
                    Debug.Log("neue Werte aber nichts wegspeichern");
                    Master.Instance().MyGameState.SetUpNextLevel(true, false);
                }
                _firstTimeUseOfScript = false;
            }
            else { //Nächstes Level laden in laufendem Durchgang
                Debug.Log("sowohl neue Werte als auch alte wegspeichern");
                Master.Instance().MyGameState.SetUpNextLevel(true, true);
            }
            KapitelNummer.text = Master.Instance().MyGameState.StageCurrent.ToString();
            KapitelName.text = Master.Instance().MyGameState.ChapterInUse;
            WeiterText.enabled = false;

            startTime = DateTime.Now;
            timeToProceed = new TimeSpan(0, 0, 0, 0, (int) waitTimeInSeconds * 1000);
        }


        private void Update() {
            if (waitTimeInSeconds == -1.0f) { //Falls Wert nicht über Inspektor gesetzt
                waitTimeInSeconds = 1.5f;
                timeToProceed = new TimeSpan(0, 0, 0, 0, (int)waitTimeInSeconds*1000);
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
        #endregion
    }
}
