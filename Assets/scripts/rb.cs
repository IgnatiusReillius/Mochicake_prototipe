using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rb : MonoBehaviour
{
    [SerializeField] private float acceleration, rotationVelocity, collisionForce;
    [SerializeField] private int damage;
    [SerializeField] private Vector3 movementInput, rotationDirection;
    [SerializeField] private int[] velocitiesValues;
    [SerializeField] private Rigidbody myRB;
    [SerializeField] private BoxCollider[] colliderList;
    [SerializeField] private GameObject[] goList;
    [SerializeField] private GameObject deathScreen;

    private void Awake()
    {
        movementInput.z = 1;
    }

    private void FixedUpdate()
    {
        myRB.AddRelativeForce(movementInput * acceleration, ForceMode.Force);

        if(myRB.velocity.magnitude > 0)
        {
            myRB.AddTorque(rotationDirection * rotationVelocity);
        }
    }

    public void SetAccelerationByIndex(int index)
    {
        acceleration = velocitiesValues[index];
    }

    public void SetWheelRotation(float normalized)
    {
        if(myRB.velocity.magnitude > 0) {
            rotationDirection = Vector3.up * normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < colliderList.Length; i++)
        {
            Collider hitCollider = collision.contacts[0].thisCollider;
            if (hitCollider  == colliderList[i])
            {
                colliderList[i].enabled = false;
                goList[i].SetActive(false);
                damage++;
                break;
            }
        }

        if(damage == 10)
        {
            deathScreen.SetActive(true);
        }
        myRB.AddForce(collision.contacts[0].normal * collisionForce, ForceMode.Impulse);
    }

}
