using Assets.Scripts.SceneController;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.FeatureScripts {
    public class ChestScript : MonoBehaviour
    {
        private bool ChestIsLocked = false;
        //private bool ChestIsOpen; //TODO diese Information ist redundant, weil sie sich aus ChestIsLocked ableiten lässt

        public void OnCollisionStay(Collider col) {
            if (Input.GetKeyDown("e") && !ChestIsLocked && col.gameObject.CompareTag("Player"))
            {
                QuestionDialogController.Instance().ShowQuestion();
                if (QuestionDialogController.Instance().AnswerCorrect())
                {
                    // TODO : Hintstein oder Portalstein geben
                    Debug.Log("Hintstein oder Portalstein bekommen");
                    ChestIsLocked = true;
                }

                //ChestIsOpen = true;
                //OpenChest();
                //ChestIsLocked = true; //Truhe abschließen
                //Destroy(gameObject); //Lässt Truhe verschwinden
            }
        }

        //public void OpenChest() {
        //    SceneManager.LoadScene( /*QuestionDialog?*/ null);
        //    Fragemenü aufrufen
        //    ChestIsOpen = false;
        //}
    }
}