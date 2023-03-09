using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject Pause_menu;
    public void LoadScene(string sceneName)
    {
	    SceneManager.LoadScene(sceneName);
    }
    public void Pause()
    {
        Pause_menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Pause_menu.SetActive(false);
        Time.timeScale = 1f;
    }
}
