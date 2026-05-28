using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rb : MonoBehaviour
{
    [SerializeField] private float acceleration, rotationVelocity, collisionForce, maxAngularSpeed, velocity, stopAngularVelocityTime;
    public int damage = 0;
    [SerializeField] private Vector3 movementInput, rotationDirection;
    [SerializeField] private int[] velocitiesValues;
    [SerializeField] private Rigidbody myRB;
    [SerializeField] private BoxCollider[] colliderList;
    [SerializeField] private GameObject[] goList;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private FinishLine finishLine;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        movementInput.z = 1;
        finishLine = GameObject.Find("Finish Line").GetComponent<FinishLine>();
        gameManager = GameObject.Find("Game manager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        myRB.AddRelativeForce(movementInput * acceleration, ForceMode.Force);

        if(acceleration != 0) {
            myRB.AddTorque(rotationDirection * rotationVelocity);
        } else {
            myRB.angularVelocity = Vector3.Lerp(myRB.angularVelocity, Vector3.zero, stopAngularVelocityTime * Time.fixedDeltaTime);
            if (myRB.angularVelocity.magnitude < 0.05f) {
                myRB.angularVelocity = Vector3.zero;
            }
        }

        if (myRB.angularVelocity.magnitude > maxAngularSpeed) {
            myRB.angularVelocity = myRB.angularVelocity.normalized * maxAngularSpeed;
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
                finishLine.haveBeenDamaged();
                break;
            }
        }

        if(damage == 10)
        {
            acceleration = 0;
            myRB.velocity = Vector3.zero;
            myRB.angularVelocity = Vector3.zero;
            deathScreen.SetActive(true);
            gameManager.StopTime();
        }
        myRB.AddForce(collision.contacts[0].normal * collisionForce, ForceMode.Impulse);
    }

}
