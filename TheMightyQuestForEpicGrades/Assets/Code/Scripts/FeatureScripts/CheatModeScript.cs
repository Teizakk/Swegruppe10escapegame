using Assets.Code.Manager;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class CheatModeScript : MonoBehaviour {
        
        private bool h;
        private bool a;

        public Canvas CheatInfoPrefab;
        private Canvas CheatInfo;

        private bool HaxActivated; //für schnellere Abfragen ohne Master.Instance()... bemühen zu müssen

        //Nur für Fehlermeldung falls nicht gesetzt
        public void Awake() {
            if (CheatInfoPrefab == null) {
                Debug.LogError("CheatInfo-Text-Prefab muss gesetzt sein!");
            }
        }
        // Update is called once per frame
        private void Update() {
            if (Input.anyKey) {
                if (Input.GetKey("h")) {
                    h = true;
                    Debug.Log("h");
                }
                else if (Input.GetKey("a") && h) {
                    a = true;
                    Debug.Log("a");
                }
                else if (Input.GetKey("x") && h && a) {
                    Debug.Log("x");

                    if (HaxActivated) {
                        HaxActivated = false;
                        Master.Instance().MyGameState.CheatmodeActive = false;
                        if (!Master.Instance().MyGameState.CheatmodeActive) {
                            Debug.Log("Cheatmode deactivated");
                        }
                        CheatInfo.enabled = false;
                        Destroy(CheatInfo.gameObject);
                    }
                    else {
                        HaxActivated = true;
                        Master.Instance().MyGameState.CheatmodeActive = true;
                        if (Master.Instance().MyGameState.CheatmodeActive) {
                            Debug.Log("Cheatmode activated");
                        }
                        CheatInfo = Instantiate(CheatInfoPrefab);
                        DontDestroyOnLoad(CheatInfo.gameObject);
                        CheatInfo.enabled = true;
                    }
                    h = false;
                    a = false;
                }
                else {
                    //Debug.Log("false Key"); //sollte auskommentiert sein, spammt sonst zu viel :)
                    h = false;
                    a = false;
                }
            }
        }
    }
}
