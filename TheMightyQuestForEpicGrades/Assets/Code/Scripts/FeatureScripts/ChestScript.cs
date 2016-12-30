using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Scripts.FeatureScripts {
    public class ChestScript : MonoBehaviour {
        private bool ChestIsOpen = true;

        public void OnTriggerStay(Collider col) {
            if (Input.GetKeyDown("e") && ChestIsOpen) //col.gameObject.tag == "Player")
            {
                OpenChest();
                ChestIsOpen = false; //Truhe abschließen
                //Destroy(gameObject); //Lässt Truhe verschwinden //...das machen wir nicht, dann ist die nämlich ganz weg.. so ganz ganz
            }
        }

        public void OpenChest() {
            SceneManager.LoadScene( /*QuestionDialog?*/ null);
            //Fragemenü aufrufen
            ChestIsOpen = false;
        }
    }
}