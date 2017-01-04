using Assets.Code.Manager;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class MainGameDialogController : MonoBehaviour {

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

            //BoardBuilder initialisieren und Szene bauen lassen
            Master.Instance().MyLevel.BoardBuilder_TMP = Instantiate(BoardBuilder);
            Master.Instance().MyLevel.BoardBuilder_TMP.SetupScene();
            //Danach wird er vom PlayerScript wenn er damit fertig ist gekillt, weil er nicht mehr gebraucht wird.
            
            //Tooltip per default auf nicht sichtbar
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
