using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Win_Scene_Manager : MonoBehaviour
{
    public TMP_Text totalScore;

    private void Start()
    {
        totalScore.text = ScoringController.totalScore_static.ToString();
    }
}
