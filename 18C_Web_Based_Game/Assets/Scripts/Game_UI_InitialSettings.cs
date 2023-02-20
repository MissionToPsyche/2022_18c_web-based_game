using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_UI_InitialSettings : MonoBehaviour
{
    public GameObject UserParameters_HUD;
    void Start()
    {
        UserParameters_HUD.SetActive(false);
    }

    public void ActivateUserParameters_HUD()
    {
        UserParameters_HUD.SetActive(true);
    }
}
