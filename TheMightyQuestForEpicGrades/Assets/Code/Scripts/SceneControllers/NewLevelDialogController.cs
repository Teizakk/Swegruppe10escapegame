using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace Assets.Code.Scripts.SceneControllers {
    public class NewLevelDialogController : MonoBehaviour {
        private void Awake() {
            FilePathsAndStatus = new List<FileNameHelper>();
            FilePathsAndStatus.Clear();
            PreselectedFolderPath = "";
            FileDisplayContents = new List<GameObject>();
            FileDisplayContents.Clear();
        }

        public void RefreshSelectedFiles(bool deleteFalseOnes = true) {
            //TODO Soll die angezeigten Dateien aktualiseren wenn ungültig -> löschen (könnte man auch aufrufen, nachdem neue hinzugefügt wurden)
            //Anzeige leer machen
            foreach (var displayLine in FileDisplayContents) Destroy(displayLine);
            FileDisplayContents.Clear();
            //Wenn Pfade angezeigt werden...
            if (FilePathsAndStatus.Count != 0)
                for (var i = 0; i < FilePathsAndStatus.Count; i++) {
                    var file = FilePathsAndStatus[i];
                    //Debug.Log("Anzahl Elemente in Filepath...: " + FilePathsAndStatus.Count);
                    /*Debug.Log("Gerade behandeltes Objekt: " + file.FullFilePath + " IsValid = " + file.isValid + "\n" +
                           " isChecked = " + file.isChecked + " HasBeenShown = " + file.statusHasBeenShown + " Index: " + i
                           ); */
                    //alle die noch nicht gezeigt wurden anzeigen
                    if (!file.statusHasBeenShown) {
                        AddToFileDisplay(file.FullFilePath, file.isChecked, file.isValid);
                        file.statusHasBeenShown = true;
                        //Debug.Log("Bisher nicht gezeigte Datei wird gezeigt: " + file.FullFilePath);
                    }
                    else {
                        //alle validen weiterhin anzeigen
                        if (file.isValid) {
                            AddToFileDisplay(file.FullFilePath, file.isChecked, file.isValid);
                            //Debug.Log("Richtige Datei, die bereits gezeigt wurde wird gezeigt: " + file.FullFilePath);
                        }
                        //invalide löschen, die bereits 1x angezeigt wurden.
                        else {
                            if (deleteFalseOnes) {
                                FilePathsAndStatus.RemoveAt(i); //müsste so passen...ich mag diese Syntax nicht..
                                i--; //Index zurücksetzen, weil sonst letztes Element überschlagen wird....
                                //Debug.Log("Ungültige Datei wird gelöscht und nicht gezeigt: " + file.FullFilePath);
                            }
                            else {
                                AddToFileDisplay(file.FullFilePath, file.isChecked, file.isValid);
                                //Debug.Log("Falsche Datei, die bereits gezeigt wurde wird gezeigt: " + file.FullFilePath);
                            }
                        }
                    }
                }
        }

        public void checkFiles() {
            //TODO falls wir das wollen, sollte das hier implementiert werden (Tobis Weihnachtsaufgabe?)
            //wenn File korrekt bool auf true -> Anzeige im Scroll View ändern
            for (var i = 0; i < FilePathsAndStatus.Count; i++) {
                var file = FilePathsAndStatus[i];

                //Bereits geprüfte muss man nicht nochmals prüfen
                if (!file.isChecked) {
                    var isValid = i%2 == 0; //checkLevelFile(file); //Gerade ist einfach jede zweite Datei richtig...
                    if (isValid)
                        FilePathsAndStatus[i].isValid = true;
                    else FilePathsAndStatus[i].isValid = false;
                    FilePathsAndStatus[i].isChecked = true;
                }
            }
            //Anzeige aktualisieren
            RefreshSelectedFiles(false);

            Debug.LogError("DIESE FUNKTION GIBT ES NOCH NICHT - PSEUDO-FUNKTIONALITÄT");
        }

        public void FileDialogOpener() {
            //Wenn der FolderBrowser noch nie geöffnet wurde
            if (fbd == null)
                fbd = new FolderBrowserDialog
                {
                    RootFolder = Environment.SpecialFolder.Desktop,
                    ShowNewFolderButton = false
                };

            //Folder Browsing Dialog wird immer angezeigt, wenn die Funktion aufgerufen wurde
            var fbdDialogResult = fbd.ShowDialog();

            //Nur wenn erfolgreich abgeschlossen gehts weiter, sonst abbruch
            if (fbdDialogResult != DialogResult.OK) {
                //Resettet die Optik des Buttons (wird nicht mehr als gedrückt angezeigt)
                BrowseButton.enabled = false;
                BrowseButton.enabled = true;
                return;
            }

            PreselectedFolderPath = fbd.SelectedPath;

            //Debug.Log("Im FolderBrowser ausgewählter Pfad: " + PreselectedFolderPath);

            //In ausgewähltem Ordner den FileOpenDialog konfigurieren
            var ofd = new OpenFileDialog
            {
                Filter = "Textdatei (*.txt) | *.txt",
                AutoUpgradeEnabled = true,
                Multiselect = true,
                InitialDirectory = PreselectedFolderPath,
                RestoreDirectory = true //könnte sein, dass man das braucht daher packen wir das einfach dazu
            };

            //Debug.Log(ofd.InitialDirectory);

            //Dialog öffnen
            var ofdDialogResult = ofd.ShowDialog();

            //Auch hier muss man abbrechen, weil man sonst nachdem man einen Ordner falsch ausgewählt hat dies nicht mehr korrigieren kann
            if (ofdDialogResult != DialogResult.OK) {
                BrowseButton.enabled = false;
                BrowseButton.enabled = true;
                return;
            }

            Debug.Log(Directory.GetCurrentDirectory());

            var fileNames = ofd.SafeFileNames;

            //Wenn Dateien ausgewählt wurden
            if (fileNames.Length != 0)
                foreach (var file in fileNames)
                    if (FileNameHelper.filePathList.Contains(PreselectedFolderPath + "\\" + file)) {
                        //Wenn Pfad bereits vorhanden
                        AddToFileDisplay(PreselectedFolderPath + "\\" + file, true, false, true);
                        //Debug.Log("Bereits vorhandene Datei hinzugefügt");
                    }
                    else {
                        var fileNameHelperObject = new FileNameHelper(PreselectedFolderPath + "\\" + file, false, false);
                        //Der Liste der gespeicherten Pfade hinzufügen
                        FilePathsAndStatus.Add(fileNameHelperObject);
                        AddToFileDisplay(PreselectedFolderPath + "\\" + file, false, false);
                    }
            else Debug.Log("Keine Dateien ausgewählt...");
            BrowseButton.enabled = false;
            BrowseButton.enabled = true;
        }

        public void CopyFilesToLevelFolder() {
            var counter = 0;
            foreach (var file in FilePathsAndStatus) {
                Master.Instance().MyLevel.CopyFileToLevelFolder(file.FullFilePath);
                counter++;
            }
            Debug.Log("Insgesamt "  + counter + " Files zum Levelordner hinzugefügt");
        }

        // K L E I N E   H E L P E R   F U N K T I O N E N 
        private void AddToFileDisplay(string path, bool isChecked, bool valid = false, bool duplicate = false) {
            //Instanziere neue Textline nach prefab Vorlage mit FileDisplayContentWindow als Parent
            var displayLine = Instantiate(FileTextPrefab);
            FileDisplayContents.Add(displayLine);
            displayLine.transform.SetParent(FileDisplayContentWindow.transform, false);
            var textfields = displayLine.GetComponentsInChildren<Text>();
            if (textfields.Length == 2) {
                textfields[0].text = path;
                if (isChecked)
                    if (valid) {
                        textfields[1].text = "(OK)";
                        textfields[1].color = Color.green;
                    }
                    else {
                        if (duplicate) {
                            textfields[1].text = "(Duplikat)";
                            textfields[1].color = new Color(1.0f, 0.6f, 0.0f);
                        }
                        else {
                            textfields[1].text = "(Fehler)";
                            textfields[1].color = Color.red;
                        }
                    }
                else textfields[1].text = "";
            }
        }

        #region Helper Klasse(n)

        private class FileNameHelper {
            public static readonly List<string> filePathList = new List<string>();
            public readonly string FullFilePath;
            public bool isChecked;
            public bool isValid;
            public bool statusHasBeenShown;

            public FileNameHelper(string path, bool check, bool displayed) {
                FullFilePath = path;
                isChecked = check;
                statusHasBeenShown = displayed;
                filePathList.Add(FullFilePath);
            }
        }

        #endregion

        #region UI Elemente

        public Button BrowseButton;
        public GameObject FileDisplayContentWindow;
        public Button CheckButton;
        public Button RefreshButton;
        public Button BackButton;
        public GameObject FileTextPrefab;
        //für die Zwischenspeicherung
        private List<GameObject> FileDisplayContents;

        #endregion

        #region File/Folder Browser specifics

        private string PreselectedFolderPath;

        private List<FileNameHelper> FilePathsAndStatus; //Aufbau siehe oben

        [DllImport("user32.dll")]
        private static extern void FolderBrowsingDialog();

        private FolderBrowserDialog fbd;

        #endregion

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