using Assets.Controller;
using UnityEngine;

namespace Assets.Scripts.FeatureScripts {
    public class GameLoaderScript : MonoBehaviour {
        public GameManager GameController;
        public PlayerScript playerController;

        // Use this for initialization
        private void Awake() {
            if (PlayerScript.instance == null)
                Instantiate(playerController);
            if (GameManager.instance == null)
                Instantiate(GameController);
        }
    }
}