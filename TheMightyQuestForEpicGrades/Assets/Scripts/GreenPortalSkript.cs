using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GreenPortalSkript : MonoBehaviour {

    public static bool activated;
    public int levelIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GreenPortalSkript.activated == true && PinkPortalSkript.activated == true)
        {

            SceneManager.LoadScene(levelIndex);
        }
    }
}
