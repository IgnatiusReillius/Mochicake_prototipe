using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("trigger");
        if(collision.tag == "Player")
        {
            Debug.Log("player");
            winScreen.SetActive(true);
        }
    }
}
