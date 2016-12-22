using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PinkPortalSkript : MonoBehaviour {

    public static bool Activated;
    public int LevelIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GreenPortalSkript.Activated == true && PinkPortalSkript.Activated == true)
        {

            SceneManager.LoadScene(LevelIndex);
        }
    }
}
