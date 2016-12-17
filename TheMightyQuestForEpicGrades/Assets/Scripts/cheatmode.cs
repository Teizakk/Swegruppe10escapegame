using UnityEngine;
using UnityEngineInternal;

public class cheatmode : MonoBehaviour {

    private GameStateHolder GameStateHolderInstance;

    private bool h;
    private bool a;

    private bool HaxActivated = false; //TODO Ausweichlösung - bis wir uns unten geeinigt haben

    public void Start() {
        GameStateHolderInstance = GameStateHolder.Instance();
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

                //TODO hier folgt der TripleKill...
                if (HaxActivated) {
                    Debug.Log("Cheatmode deactivated");
                    HaxActivated = false;
                    GameManager.cheat = false;
                    GameStateHolderInstance.GameStateObject.LevelState.Cheatmode = false;
                }
                else {
                    Debug.Log("Cheatmode activated");
                    HaxActivated = true;
                    GameManager.cheat = true;
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
