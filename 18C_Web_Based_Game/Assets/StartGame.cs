using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // go to game
    public string LevelName;

    public void LoadLevel()
    {
	SceneManager.LoadScene(LevelName);
    }
}
