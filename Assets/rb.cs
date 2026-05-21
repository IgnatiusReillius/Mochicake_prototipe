using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rb : MonoBehaviour
{
    [SerializeField] private float acceleration, maxSpeed = 15f, rotationVelocity;
    [SerializeField] private Vector3 movementInput, rotationDirection;
    [SerializeField] private int[] velocitiesValues = {1, 2, 4, 6};
    private Rigidbody myRB;
    
    private void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        movementInput.z = 1;
    }

    private void Update() {
        
        if(Input.GetKey(KeyCode.BackQuote)) {
            acceleration = -velocitiesValues[0];
        }
        if(Input.GetKey(KeyCode.Alpha0)) {
            acceleration = 0;
        }
        if(Input.GetKey(KeyCode.Alpha1)) {
            acceleration = velocitiesValues[1];
        }
        if(Input.GetKey(KeyCode.Alpha2)) {
            acceleration = velocitiesValues[2];
        }
        if(Input.GetKey(KeyCode.Alpha3)) {
            acceleration = velocitiesValues[3];
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            rotationDirection = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationDirection = Vector3.down;
        }
        else
        {
            rotationDirection = Vector3.zero;
        }

    }

    private void FixedUpdate()
    {
        myRB.AddTorque(rotationDirection * rotationVelocity);
        
        myRB.AddRelativeForce(movementInput * acceleration, ForceMode.Force);

        if(myRB.velocity.magnitude > maxSpeed)
        {
            myRB.velocity = myRB.velocity.normalized * maxSpeed;
        }
    }
}
