using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    private Transform boardHolder;
    // TODO
    // Überlegen, ob man die gridPositions tatsächlich braucht?
    private List<Vector3> gridPositions = new List<Vector3>();
    private char[,] levelData = null;
    private int max_x = 0;
    private int max_z = 0;

    public GameObject WallBlock;
    public GameObject FloorBlock;
    public GameObject ChestBlock;
    public GameObject PortalBlock;
    public GameObject StartBlock;
    public GameObject EndBlock;
    [HideInInspector]
    public Vector3 StartPosition;
    [HideInInspector]
    public Vector3 EndPosition;

    // Initialisiere die Liste mit den möglichen Positionen
    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 0; x < max_x; ++x)
        {
            for (int z = 0; z < max_z; ++z)
            {
                // Das sind die möglichen Positionen für Wände, Truhen, Portalsteine, Gegner (die wir nicht implementieren müssen) usw.
                gridPositions.Add(new Vector3((float)x, 1.0f, (float)z));
            }
        }
    }

    // Erstelle die Wände und die Objekte, die über dem Boden stehen
    void UpperBoardSetup()
    {
        // Um eine schöne Struktur zu wahren, werden alle Boardobjekte den Parent "Board" erhalten (boardHolder)
        boardHolder = new GameObject("Board").transform;

        GameObject toInstantiate = null;
        

        for (int x = 0; x < max_x; ++x)
        {
            for (int z = 0; z < max_z; ++z)
            {
                float height = 1.0f;
                switch(levelData[x,z])
                {
                    case '#':   // Wall
                        toInstantiate = WallBlock;
                        break;
                    case 'c':
                    case 'C':   // Chest
                        toInstantiate = ChestBlock;
                        break;
                    case 'p':
                    case 'P':   // Portal
                        toInstantiate = PortalBlock;
                        break;
                    case 's':
                    case 'S':   // Start
                        StartPosition = new Vector3(x, height, z);
                        toInstantiate = StartBlock;
                        GameObject startInstance = Instantiate(FloorBlock, new Vector3(x, 0.0f, z), Quaternion.identity) as GameObject;
                        startInstance.transform.SetParent(boardHolder);
                        break;
                    case 'f':
                    case 'F':
                        height = 0.0f;
                        toInstantiate = FloorBlock;
                        break;
                    case 'e':
                    case 'E':   // End
                        EndPosition = new Vector3(x, height, z);
                        toInstantiate = EndBlock;
                        GameObject endInstance = Instantiate(FloorBlock, new Vector3(x, 0.0f, z), Quaternion.identity) as GameObject;
                        endInstance.transform.SetParent(boardHolder);
                        break;
                    default:    // Sollte niemals aufgerufen werden
                        toInstantiate = null;
                        break;
                }

                if (toInstantiate != null)
                {
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, height, z), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
                else
                    throw new System.Exception("Unerlaubtes Zeichen innerhalb der Leveldaten gefunden! Erlaubte Zeichen sind: {'#', 'C', 'P', 'S', 'E', 'F'}");
            }
        }
    }

    // Setze den Boden
    void LowerBoardSetup()
    {
        // 64 Einheiten lang und 64 Einheiten breit
        FloorBlock.transform.localScale = new Vector3(64.0f, 0.0f, 64.0f);
        Instantiate(FloorBlock, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

    // TODO
    // Erhalte eine zufällige Position für eine Truhe
    Vector3 RandomPosition()
    {
        return new Vector3();
    }

    // TODO
    // Setze das Objekt (Truhe) an der RandomPosition
    void LayoutObjectAtRandom(GameObject gameObj, int min, int max)
    {

    }

    // TODO
    // Erstelle die Szene
    public void SetupScene(int level)
    {
        levelData = GetComponent<LevelLoader>().loadLevel(level);

        // Die LevelDaten müssen gesetzt sein, sodass man die möglichen Positionen zum Spawnen setzen kann.
        if (levelData != null)
        {
            // GetLength(int x) ist dazu da, die Anzahl der möglichen Elemente innerhalb der "x."-Dimension zu erhalten
            max_x = levelData.GetLength(0);
            max_z = levelData.GetLength(1);
        }
        else
            throw new System.Exception("Das Level konnte nicht richtig eingelesen werden. Versuchen Sie es erneut.");

        //LowerBoardSetup();
        UpperBoardSetup();
        InitializeList();

        
    }
}
