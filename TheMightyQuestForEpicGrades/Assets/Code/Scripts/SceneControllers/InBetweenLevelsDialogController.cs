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
        #endregion

        private static bool _firstTimeUseOfScript = true;
        private bool _ableToProceed;

        private IEnumerator WaitABitThenShowMessage() {
            yield return new WaitForSeconds(2);
            WeiterText.enabled = true;
            _ableToProceed = true;
        }

        private void Awake () {
            //Der Player Clone darf diese Szene nicht überleben =)
            if (PlayerScript.instance != null) {
                Destroy(PlayerScript.instance.gameObject);
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
        
            StartCoroutine(WaitABitThenShowMessage());
        }

        private void Update() {
            if (!_ableToProceed) return;
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
