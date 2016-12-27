using System;
using System.Collections.Generic;
using Assets.Code.Manager;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class BoardBuilderScript : MonoBehaviour {

        // TODO Überlegen, ob man die gridPositions tatsächlich braucht?
        private readonly List<Vector3> gridPositions = new List<Vector3>();
        private Transform boardHolder;
        private char[,] levelData;
        private int max_x;
        private int max_z;

        public GameObject WallBlock;
        public GameObject FloorBlock;
        public GameObject ChestBlock;        
        public GameObject[] PortalBlock;
        public GameObject StartBlock;
        public GameObject EndBlock;

        [HideInInspector] public Vector3 StartPosition;
        [HideInInspector] public Vector3 EndPosition;

        // Initialisiere die Liste mit den möglichen Positionen
        private void InitializeList() {
            gridPositions.Clear();

            for (var x = 0; x < max_x; ++x)
                for (var z = 0; z < max_z; ++z)
                    gridPositions.Add(new Vector3(x, 1.0f, z));
        }

        // Erstelle die Spielfläche
        private void BoardSetup() {
            // Um eine schöne Struktur zu wahren, werden alle Boardobjekte den Parent "Board" erhalten (boardHolder)
            boardHolder = new GameObject("Board").transform;

            GameObject toInstantiate = null;

            var portalNumber = 0;

            for (var x = 0; x < max_x; ++x)
                for (var z = 0; z < max_z; ++z) {
                    var height = 1.0f;
                    switch (levelData[x, z]) {
                        case '#': // Wall
                            toInstantiate = WallBlock;
                            break;
                        case 'c':
                        case 'C': // Chest
                            toInstantiate = ChestBlock;
                            break;
                        case 'p':
                        case 'P': // Portal
                            toInstantiate = PortalBlock[portalNumber++];
                            break;
                        case 's':
                        case 'S': // Start
                            StartPosition = new Vector3(x, height, z);
                            toInstantiate = StartBlock;
                            var startInst =
                                    Instantiate(FloorBlock, new Vector3(x, 0.0f, z), Quaternion.identity) as GameObject;
                            startInst.transform.SetParent(boardHolder);
                            break;
                        case 'f':
                        case 'F':
                            height = 0.0f;
                            toInstantiate = FloorBlock;
                            break;
                        case 'e':
                        case 'E': // End
                            EndPosition = new Vector3(x, height, z);
                            toInstantiate = EndBlock;
                            var endInst =
                                    Instantiate(FloorBlock, new Vector3(x, 0.0f, z), Quaternion.identity) as GameObject;
                            endInst.transform.SetParent(boardHolder);
                            break;
                        default: // Sollte niemals aufgerufen werden
                            toInstantiate = null;
                            break;
                    }

                    if (toInstantiate != null) {
                        var instance =
                                Instantiate(toInstantiate, new Vector3(x, height, z), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                    }
                    else
                        throw new Exception(
                            "Unerlaubtes Zeichen innerhalb der Leveldaten gefunden! Erlaubte Zeichen sind: {'#', 'C', 'P', 'S', 'E', 'F'}");
                }
        }

        // Erstelle die Szene
        public void SetupScene(int level) {
            levelData = GetComponent<LevelManager>().loadLevel(level);

            // Die LevelDaten müssen gesetzt sein, sodass man die möglichen Positionen zum Spawnen setzen kann.
            if (levelData != null) {
                // GetLength(int x) ist dazu da, die Anzahl der möglichen Elemente innerhalb der "x."-Dimension zu erhalten
                max_x = levelData.GetLength(0);
                max_z = levelData.GetLength(1);
            }
            else
                throw new Exception("Das Level konnte nicht richtig eingelesen werden. Versuchen Sie es erneut.");

            BoardSetup();
            InitializeList();
        }
    }
}
