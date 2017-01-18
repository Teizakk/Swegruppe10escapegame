using System;
using System.Collections.Generic;
using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using UnityEngine;

namespace Assets.Code.Scripts.FeatureScripts {
    public class BoardBuilderScript : MonoBehaviour {

        private GameObject[,] _instantiatedObjects;
        private Transform boardHolder;
        
        private char[,] levelData;
        private int max_x;
        private int max_z;

        #region Prefabs
        [Header("BoardBuilder Prefabs")]
        public GameObject WallBlock;
        public GameObject FloorBlock;
        public GameObject ChestBlock;
        public GameObject[] PortalBlock = new GameObject[3];
        public GameObject StartBlock;
        public GameObject EndDoor;
        #endregion

        [HideInInspector] public Vector3 StartPosition;
        [HideInInspector] public Vector3 EndPosition;

        // Erstelle die Spielfläche
        private void SetupBoard() {
            // Um eine schöne Struktur zu wahren, werden alle Boardobjekte den Parent "Board" erhalten (boardHolder)
            boardHolder = new GameObject("Board").transform;
            _instantiatedObjects = new GameObject[max_x,max_z];

            var portalNumber = 0;
            var chestIndex = 0;
            GameObject toInstantiate = null;

            for (var x = 0; x < max_x; ++x) {
                //Zum Merken was links von dem aktuellen Block platziert wurde - resettet jede Reihe
                GameObject lastInstatiatedBlock = null;

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
                            break;
                        case 'f':
                        case 'F':
                        case ' ': //Floor
                        case 'd':
                        case 'D': //Door
                            height = 0.0f;
                            toInstantiate = FloorBlock;
                            break;
                        case 'e':
                        case 'E': // End
                            EndPosition = new Vector3(x, height, z);
                            toInstantiate = EndDoor;
                            break;
                        default: // Sollte niemals aufgerufen werden
                            toInstantiate = null;
                            break;
                    }

                    if (toInstantiate != null) {
                        var instance = Instantiate(toInstantiate, new Vector3(x, height, z), Quaternion.identity) as GameObject;

                        //Debug.Log("Name von toInstatiate: " + toInstantiate.name);
                        //Hier werden bei der gleichen Map die Blocks immer in der gleichen Reihenfolge landen und so ihren index bekommen
                        if (toInstantiate == ChestBlock && instance != null) {
                            instance.GetComponent<ChestScript>().Index = chestIndex;
                            if (Master.Instance().MyGameState.ChestCheckArray[chestIndex]) { //war im vorherigen Savegame bereits gelocked
                                    instance.GetComponent<ChestScript>().Lock();
                            }
                            Debug.LogWarning("Chestindex: " + chestIndex);
                            chestIndex++;
                        }

                        if (toInstantiate == PortalBlock[0] && instance != null) {
                            instance.GetComponent<PortalScript>().Color = PortalColor.Pink; //nicht zwingend erforderlich
                            if (Master.Instance().MyGameState.PortalStonePinkHasBeenUsed) { //wenn im letztetn Spiel aktiviert erneut aktivieren
                                instance.GetComponent<PortalScript>().Activate();
                            }
                        }
                        else if (toInstantiate == PortalBlock[1] && instance != null) {
                            instance.GetComponent<PortalScript>().Color = PortalColor.Blue; //nicht zwingend erforderlich
                            if (Master.Instance().MyGameState.PortalStoneBlueHasBeenUsed) { 
                                instance.GetComponent<PortalScript>().Activate();
                            }
                        }
                        else if (toInstantiate == PortalBlock[2] && instance != null) {
                            instance.GetComponent<PortalScript>().Color = PortalColor.Green; //nicht zwingend erforderlich
                            if (Master.Instance().MyGameState.PortalStoneGreenHasBeenUsed) { 
                                instance.GetComponent<PortalScript>().Activate();
                            }
                        }
                        
                        if (toInstantiate == EndDoor && (lastInstatiatedBlock == null || lastInstatiatedBlock == FloorBlock)) { //Prüft vermutlich nur auf Referenzgleichheit sollte aber ok sein
                            instance.transform.rotation = Quaternion.AngleAxis(90, Vector3.up); //wenn der EndDoor Block in einer vertikalen Wand ist dann um 90° drehen
                        }

                        if (instance == null) {
                            Debug.LogError("Fehler beim Erstellen des Spielfeldes! Instanzieren von: " + toInstantiate.gameObject.name + " ist fehlgeschlagen!");
                            throw new NullReferenceException();
                        }

                        instance.transform.SetParent(boardHolder);

                        //alles außer der Boden und Wände braucht noch einen Boden 
                        if (toInstantiate != FloorBlock && toInstantiate != WallBlock) {
                            instance = Instantiate(FloorBlock, new Vector3(x, 0.0f, z), Quaternion.identity) as GameObject; //falls problematisch hier besser differenzieren
                            if (instance == null) {
                                Debug.LogError("Fehler beim Erstellen des Spielfeldes! Instanzieren von: " + toInstantiate.gameObject.name + " ist fehlgeschlagen!");
                                throw new NullReferenceException();
                            }
                            instance.transform.SetParent(boardHolder);
                        }
                        
                        
                        //Für das spätere Optimieren
                        _instantiatedObjects[x, z] = instance;
                        
                        lastInstatiatedBlock = toInstantiate;
                    }
                    else
                        throw new Exception(
                            "Unerlaubtes Zeichen innerhalb der Leveldaten gefunden! Erlaubte Zeichen sind: {'#', 'C', 'P', 'S', 'E', 'F', 'D'} Zeichen war: " +
                            levelData[x, z]);
                }
            }
        }

        // Erstelle die Szene
        public void SetupScene() {
            levelData = Master.Instance().MyLevel.GetLevelData();

            // Die LevelDaten müssen gesetzt sein, sodass man die möglichen Positionen zum Spawnen setzen kann.
            if (levelData != null) {
                // GetLength(int x) ist dazu da, die Anzahl der möglichen Elemente innerhalb der "x."-Dimension zu erhalten
                max_x = levelData.GetLength(0);
                max_z = levelData.GetLength(1);
            }
            else
                throw new Exception("Das Level konnte nicht richtig eingelesen werden. Versuchen Sie es erneut.");
            
            SetupBoard();
            OptimizeMeshes();
        }

        private void OptimizeMeshes() {
            //Optimizing the Floor
            List<GameObject> floorCubesList = new List<GameObject>();
            List<GameObject> wallCubesList = new List<GameObject>();
            foreach (var myGameObject in _instantiatedObjects) {
                switch (myGameObject.name) {
                    case "FloorCube(Clone)":
                        floorCubesList.Add(myGameObject);
                        break;
                    case "WallCube(Clone)":
                        wallCubesList.Add(myGameObject);
                        break;
                }
            }

            //Ersten Block zum Parent machen
            var combinedFloorCube = floorCubesList[0];
            combinedFloorCube.transform.SetAsFirstSibling();
            for (int j = 1; j < floorCubesList.Count; j++) {
                floorCubesList[j].transform.SetParent(combinedFloorCube.transform);
            }
            combinedFloorCube.AddComponent<MeshCombinerScript>();

            combinedFloorCube.transform.position = new Vector3(0,0,0);

            for (int i = 1; i < floorCubesList.Count; i++) {
                Destroy(floorCubesList[i].gameObject);
            }
            
            floorCubesList.Clear();
            combinedFloorCube.gameObject.name = "CombinedFloorCubes";
            Destroy(combinedFloorCube.GetComponent<BoxCollider>());
            combinedFloorCube.AddComponent<MeshCollider>();
            combinedFloorCube.GetComponent<MeshCollider>().sharedMesh =
                    combinedFloorCube.GetComponent<MeshFilter>().mesh;

            //Ersten Block zum Parent machen
            var combinedWallBlocks = wallCubesList[0];
            combinedWallBlocks.transform.SetAsFirstSibling();
            for (int j = 1; j < wallCubesList.Count; j++)
            {
                wallCubesList[j].transform.SetParent(combinedWallBlocks.transform);
            }
            combinedWallBlocks.AddComponent<MeshCombinerScript>();

            combinedWallBlocks.transform.position = new Vector3(0, 0, 0);

            for (int i = 1; i < wallCubesList.Count; i++)
            {
                Destroy(wallCubesList[i].gameObject);
            }

            wallCubesList.Clear();
            combinedWallBlocks.transform.localScale = new Vector3(1,1,1);
            combinedWallBlocks.gameObject.name = "CombinedWallBlocks";

            Destroy(combinedWallBlocks.GetComponent<BoxCollider>());
            combinedWallBlocks.AddComponent<MeshCollider>();
            combinedWallBlocks.GetComponent<MeshCollider>().sharedMesh =
                    combinedWallBlocks.GetComponent<MeshFilter>().mesh;
        }
    }
}
