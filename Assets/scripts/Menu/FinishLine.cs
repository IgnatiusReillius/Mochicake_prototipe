using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private rb rbVelocity;
    [SerializeField] private int level;
    [SerializeField] private bool isNotDamaged = true, isPhotoTaken = false, isInTime = true;

    public void haveBeenDamaged() {
        isNotDamaged = false;
    }
    public void photoIsTaken() {
        isPhotoTaken = true;
    }
    public void outOfTime() {
        isInTime = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.FinishLevel(level, isNotDamaged, isPhotoTaken, isInTime);
            GameManager.Instance.StopTime();
            victoryScreen.SetActive(true);
            rbVelocity.SetAccelerationByIndex(1);
        }
    }
}
