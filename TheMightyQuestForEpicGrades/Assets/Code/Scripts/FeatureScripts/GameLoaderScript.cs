﻿using Assets.Code.Controller;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class GameLoaderScript : MonoBehaviour {
        public GameManager gameManager;
        public PlayerScript player;

        // Use this for initialization
        private void Awake() {
            if (PlayerScript.instance == null)
                Instantiate(player);
            if (GameManager.instance == null)
                Instantiate(gameManager);
        }
    }
}
