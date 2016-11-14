using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    private char[,] levelData;

    // TODO
    // Initialisiere die Liste mit den möglichen Positionen
    void InitializeList()
    {
        
    }

    // TODO
    // Erstelle die Wände und den Boden
    void BoardSetup()
    {

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

        BoardSetup();
    }
}
