using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpdateScore : MonoBehaviour
{
    private Slider slider;
    TextMeshProUGUI scoretxt;

    // need to determine how to assign points
    void Start()
    {
        scoretxt = GameObject.Find("Canvas/Background/ScoreTotal").GetComponent<TMPro.TextMeshProUGUI>();
        scoretxt.text = "1"; // temp starting value
    }

    // currently adds 1 to score on every click of mouse
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            scoretxt = GameObject.Find("Canvas/Background/ScoreTotal").GetComponent<TMPro.TextMeshProUGUI>();

            // string to int to string again
            int temp = Int32.Parse(scoretxt.text);
            temp += 1;
            scoretxt.text = temp.ToString();

            slider = GameObject.Find("Canvas/Background/ProgressBar").GetComponent<Slider>();
            slider.value += (float)1;
        }
    }
}
 