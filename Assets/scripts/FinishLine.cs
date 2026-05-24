using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private rb rbVelocity;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            victoryScreen.SetActive(true);
            rbVelocity.SetAccelerationByIndex(1);
        }
    }
}
