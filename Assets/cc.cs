using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cc : MonoBehaviour
{
    float speed = 5f;
    CharacterController charCtlr;
    public Vector3 movementInput;

    void Awake() {
        charCtlr = GetComponent<CharacterController>();
    }

    void Update() {
        movementInput = Vector3.zero;
        
        if(Input.GetKey(KeyCode.W)) {
            movementInput.z = 1;
        }
        else if(Input.GetKey(KeyCode.S)) {
            movementInput.z = -1;
        }

        if(Input.GetKey(KeyCode.D)) {
            movementInput.x = 1;
        }
        else if(Input.GetKey(KeyCode.A)) {
            movementInput.x = -1;
        }

        Move(movementInput);
    }

    void Move(Vector3 direction)
    {
        charCtlr.SimpleMove(direction.normalized * speed);
    }
}
