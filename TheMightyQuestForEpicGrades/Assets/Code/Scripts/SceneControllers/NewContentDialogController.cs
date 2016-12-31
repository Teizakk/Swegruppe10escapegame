using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewContentDialogController : MonoBehaviour {

        #region UI-Elemente
        public Dropdown ModuleDropdown;
        #endregion

        // Use this for initialization
        private void Start() {
            Master.Instance().MyModule.LoadFromFile();
            ModuleDropdown.AddOptions(Master.Instance().MyModule.GetModulesAsList());
        }

        public void SetModuleToEdit() {
            //Nimmt den Text der ausgewählten Option und übergibt ihn dem "Zwischenspeicher"
            Master.Instance().MyModule.ModuleToEdit = ModuleDropdown.options[ModuleDropdown.value].text;
            Debug.Log("Das zu bearbeitende Modul wurde auf: " + ModuleDropdown.options[ModuleDropdown.value].text + " gesetzt!");
        }
    }
}