using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] private GameObject thisScreen, nextScreen;
    public void Next()
    {
        thisScreen.SetActive(false);
        if(nextScreen){
            nextScreen.SetActive(true);
        }
    }
}
