using Assets.Code.Manager;
using UnityEngine;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewQuestionDialogController : MonoBehaviour {

        // Use this for initialization
        void Awake () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
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
