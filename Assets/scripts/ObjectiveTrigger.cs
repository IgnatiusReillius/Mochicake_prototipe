using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private FinishLine finishLine;

    private void Awake()
    {
        finishLine = GameObject.Find("Finish Line").GetComponent<FinishLine>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            finishLine.photoIsTaken();
        }
    }
}
