using Assets.Code.Manager;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class MainGameDialogController : MonoBehaviour {

        //TODO DAS BRAUCHT NOCH EINE LOGISCHE KOMPLETT ÜBERARBEITUNG

        [Header("BoardBuilder Prefab")]
        public BoardBuilderScript BoardBuilder;
        [Header("Player Prefab")]
        public PlayerScript Player;
        [Header("Tooltip Prompt")]
        public GameObject TooltipPanel;

        // Use this for initialization
        private void Awake() {
            if (PlayerScript.instance == null)
                Instantiate(Player);
            if (BoardBuilder != null) { //BS aber jajaja erstmal egal
                Master.Instance().MyLevel.BoardBuilder_TMP = Instantiate(BoardBuilder);
                Master.Instance().MyLevel.LoadFromFile(1);
                Master.Instance().MyLevel.BoardBuilder_TMP.SetupScene();
            }
            TooltipPanel.SetActive(false);
        }

        private void ActivateTooltip(string message) {
            TooltipPanel.GetComponentInChildren<Text>().text = "Taste 'E': " + message;
            TooltipPanel.SetActive(true);
        }
        private void DeactivateTooltip() {
            TooltipPanel.SetActive(false);
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
