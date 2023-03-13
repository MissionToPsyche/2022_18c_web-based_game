using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField]GameObject firstScene;
    [SerializeField]GameObject secondScene;
    [SerializeField]GameObject thirdScene;
    [SerializeField]GameObject fourthScene;
    [SerializeField]GameObject fifthScene;
    [SerializeField]GameObject sixthScene;
   
    public Button Back,Next;
    public List<GameObject> Pages;
    int PageNum;
    void Start () {
		Back.onClick.AddListener(PrevPage);
        Next.onClick.AddListener(NextPage);
        Pages.Add(firstScene);
        Pages.Add(secondScene);
        Pages.Add(thirdScene);
        Pages.Add(fourthScene);
        Pages.Add(fifthScene);
        Pages.Add(sixthScene);
	}
  void PrevPage(){
    for (var i = 0; i < Pages.Count; i++) {
      if(Pages[i].activeSelf)
        PageNum=i+1;
    }
    switch(PageNum){
      case 1:
        break;
      case 2: firstScene.SetActive(true);
        secondScene.SetActive(false);
        break;
      case 3: secondScene.SetActive(true);
        thirdScene.SetActive(false);
        break;
      case 4: thirdScene.SetActive(true);
        fourthScene.SetActive(false);
        break;
      case 5: fourthScene.SetActive(true);
        fifthScene.SetActive(false);
        break;
      case 6: fifthScene.SetActive(true);
        sixthScene.SetActive(false);
        break;
    }
	}
	void NextPage(){
		for (var i = 0; i < Pages.Count; i++) {
      if(Pages[i].activeSelf)
        PageNum=i+1;
    }
    switch(PageNum){
      case 1: firstScene.SetActive(false);
        secondScene.SetActive(true);
        break;
      case 2: secondScene.SetActive(false);
        thirdScene.SetActive(true);
        break;
      case 3:thirdScene.SetActive(false);
        fourthScene.SetActive(true);
        break;
      case 4:fourthScene.SetActive(false);
        fifthScene.SetActive(true);
        break;
      case 5:fifthScene.SetActive(false);
        sixthScene.SetActive(true);
        break;
      case 6:
        break;
    }
	}
    
}
