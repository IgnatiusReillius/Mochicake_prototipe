using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCurrent : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private float force = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("dentro");
            Rigidbody rb = other.transform.root.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(direction.normalized * force, other.bounds.center, ForceMode.Force);
        }
    }
}
