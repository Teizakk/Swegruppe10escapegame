using UnityEngine;

namespace Assets.Scripts.FeatureScripts
{
    public class GameLoaderScript : MonoBehaviour
    {
        public GameController GameController;
        public PlayerScript PlayerScript;

        // Use this for initialization
        private void Awake()
        {
            if (PlayerScript.instance == null)
                Instantiate(PlayerScript);
            if (GameController.instance == null)
                Instantiate(GameController);
        }
    }
}