using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{

    public GameManager gameManager;
    public PlayerController playerController;

    // Use this for initialization
    void Awake()
    {
        if (PlayerController.instance == null)
            Instantiate(playerController);
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}
