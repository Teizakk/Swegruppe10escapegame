using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.FeatureScripts {
    public class ChestScript : MonoBehaviour {
        private bool ChestIsLocked;
        private bool ChestIsOpen; //TODO diese Information ist redundant, weil sie sich aus ChestIsLocked ableiten lässt

        public void OnTriggerStay(Collider col) {
            if (Input.GetKeyDown("e") && !ChestIsLocked) //col.gameObject.tag == "Player")
            {
                ChestIsOpen = true;
                OpenChest();
                ChestIsLocked = true; //Truhe abschließen
                Destroy(gameObject); //Lässt Truhe verschwinden
            }
        }

        public void OpenChest() {
            SceneManager.LoadScene( /*QuestionDialog?*/ null);
            //Fragemenü aufrufen
            ChestIsOpen = false;
        }
    }
}