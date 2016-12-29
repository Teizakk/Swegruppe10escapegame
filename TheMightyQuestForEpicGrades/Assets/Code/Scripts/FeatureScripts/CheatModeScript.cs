using Assets.Code.Manager;
using Assets.Code.Scripts.SceneControllers;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class CheatModeScript : MonoBehaviour {
        
        private bool h;
        private bool a;

        private bool HaxActivated; //TODO Ausweichlösung - bis wir uns unten geeinigt haben

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

                    //TODO hier folgt der TripleKill...
                    if (HaxActivated) {
                        Debug.Log("Cheatmode deactivated");
                        HaxActivated = false;
                        GameStateManager.cheat = false;
                        GameStateHolderInstance.GameStateObject.LevelState.Cheatmode = false;
                    }
                    else {
                        Debug.Log("Cheatmode activated");
                        HaxActivated = true;
                        GameStateManager.cheat = true;
                        GameStateHolderInstance.GameStateObject.LevelState.Cheatmode = true;
                    }
                    h = false;
                    a = false;
                }
                else {
                    Debug.Log("false Key");
                    h = false;
                    a = false;
                }
            }
        }
    }
}
