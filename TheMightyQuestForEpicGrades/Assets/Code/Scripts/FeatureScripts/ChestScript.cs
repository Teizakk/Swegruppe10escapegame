using Assets.Code.Manager;
using Assets.Code.Scripts.SceneControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Scripts.FeatureScripts {
    public class ChestScript : MonoBehaviour {
        //private bool ChestIsLocked = false;
        //private bool ChestIsOpen; //diese Information ist redundant, weil sie sich aus ChestIsLocked ableiten lässt

        public int Index { get; set; }

        //public void Lock() {
        //    GetComponentInChildren<Light>().enabled = true;
        //    GetComponent<SphereCollider>().enabled = false; //Damit keine Tooltips mehr kommen und man es nicht mehr öffnen kann
        //}

        private void Awake() {
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}