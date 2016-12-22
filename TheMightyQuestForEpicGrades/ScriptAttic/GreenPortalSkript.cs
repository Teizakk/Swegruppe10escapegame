using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GreenPortalSkript : MonoBehaviour {

    public static bool Activated = false;
    public int LevelIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GreenPortalSkript.Activated == true && PinkPortalSkript.Activated == true)
        {

            SceneManager.LoadScene(LevelIndex);
        }
    }
}
