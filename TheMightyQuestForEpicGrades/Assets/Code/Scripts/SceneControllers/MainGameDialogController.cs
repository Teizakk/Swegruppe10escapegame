using Assets.Code.Manager;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;

namespace Assets.Code.Scripts.SceneControllers {
    public class MainGameDialogController : MonoBehaviour {

        //TODO DAS BRAUCHT NOCH EINE LOGISCHE KOMPLETT ÜBERARBEITUNG

        [Header("BoardBuilder Prefab")]
        public BoardBuilderScript BoardBuilder;
        [Header("Player Prefab")]
        public PlayerScript Player;

        // Use this for initialization
        private void Awake() {
            if (PlayerScript.instance == null)
                Instantiate(Player);
            if (BoardBuilder != null) { //BS aber jajaja erstmal egal
                Master.Instance().MyLevel.BoardBuilder_TMP = Instantiate(BoardBuilder);
                Master.Instance().MyLevel.LoadFromFile(1);
                Master.Instance().MyLevel.BoardBuilder_TMP.SetupScene();
            }
        }
    }
}
