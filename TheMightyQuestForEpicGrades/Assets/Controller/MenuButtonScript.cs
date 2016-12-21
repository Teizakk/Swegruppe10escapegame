using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {

    public void DebugLogOnClick(string str)
    {
        Debug.Log(str);
    }

	public void NextWindowOnClick(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void CloseWindowOnClick()
    {
        Application.Quit();
    }
}
