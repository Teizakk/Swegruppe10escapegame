using UnityEngine;
using System.Collections;
using Assets.Code.Manager;
using UnityEngine.UI;

public class InBetweenLevelsDialogController : MonoBehaviour {

    #region UI-Elemente
    public Text KapitelNummer;
    public Text KapitelName;
    public Text WeiterText;
    #endregion

    private bool _ableToProceed;

    private IEnumerator WaitABitThenShowMessage() {
        yield return new WaitForSeconds(3);
        WeiterText.enabled = true;
        _ableToProceed = true;
    }

    private void Awake () {
        KapitelNummer.text = Master.Instance().MyLevel.GetLoadedLevelIndex().ToString();
        KapitelName.text = Master.Instance().MyGameState.ChapterCurrent;
        WeiterText.enabled = false;
        StartCoroutine(WaitABitThenShowMessage());
    }

    private void Update() {
        if (!_ableToProceed) return;
        if (Input.anyKeyDown) {
            Master.Instance().MyGameState.ProceedToNextLevel();
        }
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
