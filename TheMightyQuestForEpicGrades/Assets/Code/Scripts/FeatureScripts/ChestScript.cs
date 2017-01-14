﻿using Assets.Code.Manager;
using Assets.Code.Scripts.SceneControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Scripts.FeatureScripts {
    public class ChestScript : MonoBehaviour {
        //private bool ChestIsLocked = false;
        //private bool ChestIsOpen; //diese Information ist redundant, weil sie sich aus ChestIsLocked ableiten lässt

        public int Index { get; set; }

        public void Lock() {
            //GetComponentInChildren<Light>().enabled = true;
            Renderer rend = GetComponent<Renderer>();
            rend.material.SetColor("_Color",Color.red);
            GetComponent<SphereCollider>().enabled = false;
                //Damit keine Tooltips mehr kommen und man es nicht mehr öffnen kann
            if (Master.Instance().CurrentDialogController != null)
            {
                //ja ist dirty aber tuts
                Master.Instance().CurrentDialogController.GetComponent<MainGameDialogController>().DeactivateTooltip();
            }
        }

        private void Awake() {
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}