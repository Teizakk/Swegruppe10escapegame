using Assets.Code.Manager;
using Assets.Code.Scripts.SceneControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Scripts.FeatureScripts {
    public class ChestScript : MonoBehaviour {
        public int Index { get; set; }

        public void Lock() {
            GetComponent<Renderer>().material.SetColor("_Color",Color.red);
            GetComponent<SphereCollider>().enabled = false; //Damit keine Tooltips mehr kommen und man es nicht mehr öffnen kann
            if (Master.Instance().CurrentDialogController != null) { 
                Master.Instance().CurrentDialogController.GetComponent<MainGameDialogController>().DeactivateTooltip();
            }
        }

        private void Awake() {
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}