using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewContentDialogController : MonoBehaviour
    {
        #region properties

        private bool setDisabled;

        #endregion

        #region UI-Elemente
        public Dropdown ModuleDropdown;
        public Button btnNewQuestion;
        #endregion

        private static NewContentDialogController _instance;

        public static NewContentDialogController Instance()
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(NewContentDialogController)) as NewContentDialogController;
                if (!_instance)
                    Debug.LogError(
                        "Es muss ein aktives ImagePopupScript Skript auf einem GameObject in der Scene existieren");
            }
            return _instance;
        }

        // Use this for initialization
        private void Awake() {
            ModuleDropdown.AddOptions(Master.Instance().MyModule.GetModulesWithEnoughQuestionWarningAsList());
            // wenn keine Module vorhanden Button deaktivieren
            if (ModuleDropdown.options.Count == 0)
            {
                btnNewQuestion.interactable = false;
            }
            // Neues Modul... hinzufügen
            ModuleDropdown.options.Add(new Dropdown.OptionData("Neues Modul..."));
            // onValueChangedListener hinzufügen
            ModuleDropdown.onValueChanged.RemoveAllListeners();
            ModuleDropdown.onValueChanged.AddListener(NewModule);
        }

        public void SetModuleToEdit() {
            //Nimmt den Text der ausgewählten Option und übergibt ihn dem "Zwischenspeicher"
            Master.Instance().MyModule.ModuleToEdit = ModuleDropdown.options[ModuleDropdown.value].text.Split(' ')[0]; //Darf keine Leerzeichen enthalten.
            Debug.Log("Das zu bearbeitende Modul wurde auf: " + ModuleDropdown.options[ModuleDropdown.value].text.Split(' ')[0] + " gesetzt!");
        }

        // wenn Neues Modul... ausgewählt
        public void NewModule(int index)
        {
            Debug.Log("ausgewählt: "+ index);
            if (index == ModuleDropdown.options.Count - 1)
            {
                SceneManager.LoadScene(5); // NewModule Scene laden
            }
            SetModuleToEdit();
        }

        #region Master-Link
        private void Start()
        {
            Master.Instance().CurrentDialogController = this.gameObject;
        }
        #endregion
    }
}