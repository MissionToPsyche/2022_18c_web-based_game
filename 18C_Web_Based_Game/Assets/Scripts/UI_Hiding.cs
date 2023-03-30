using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hiding : MonoBehaviour
{
   //Call this method to hide the Parameters UI after the Launch button is pressed
   //The UI will only be hidden if the 
    public void HideParameterUI()
    {
        //Find the UserInput GameObject and reference the UserInputToAsteroid script
        GameObject userInput = GameObject.Find("UserInput");
        UserInputToAsteroid script = userInput.GetComponent<UserInputToAsteroid>();
        bool buttonClicked = script.ClickedButton;

        //Now if buttonClicked is true actually hide the UI
        if (buttonClicked == true)
        {
            GameObject ParameterUI = GameObject.Find("HUD - User Parameters");
            ParameterUI.SetActive(false);
        }
    }
}
