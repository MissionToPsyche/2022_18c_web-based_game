using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreStar : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public TMP_Text scorePoints;
    private int numberofStars;
    private int points;
    // Start is called before the first frame update
    void Start()
    {
        points = Random.Range(1, 1000);
        numberofStars = Random.Range(1, 3);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scorePoints.text=points.ToString();
        if(numberofStars==1)
            star1.SetActive(true);
        else if(numberofStars==2)
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
    }
}
