using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rb : MonoBehaviour
{
    [SerializeField] private float acceleration, rotationVelocity, collisionForce, maxAngularSpeed, velocity, stopAngularVelocityTime, maxSpeed;
    public int damage = 0;
    [SerializeField] private Vector3 movementInput, rotationDirection;
    [SerializeField] private int[] velocitiesValues;
    [SerializeField] private Rigidbody myRB;
    [SerializeField] private BoxCollider[] colliderList;
    [SerializeField] private GameObject[] goList;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private FinishLine finishLine;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ParticleSystem collisionParticles;

    public TrailRenderer[] trailMarks;

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
            float speedFactor = Mathf.Clamp01(myRB.velocity.magnitude / maxSpeed);
            myRB.AddTorque(rotationDirection * rotationVelocity * speedFactor);
        } else {
            myRB.angularVelocity = Vector3.Lerp(myRB.angularVelocity, Vector3.zero, stopAngularVelocityTime * Time.fixedDeltaTime);
            if (myRB.angularVelocity.magnitude < 0.05f) {
                myRB.angularVelocity = Vector3.zero;
            }
        }

        if (myRB.angularVelocity.magnitude > maxAngularSpeed) {
            myRB.angularVelocity = myRB.angularVelocity.normalized * maxAngularSpeed;
        }

        CheckDrift();
    }

    private void CheckDrift()
    {
        if(myRB.velocity.magnitude > 0) { StartEmitter(); }
        else { StopEmitter(); }
    }

    private void StartEmitter()
    {
        foreach(TrailRenderer t in trailMarks)
        {
            t.emitting = true;
        }
    }

    private void StopEmitter()
    {
        foreach(TrailRenderer t in trailMarks)
        {
            t.emitting = false;
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
                ParticleSystem ps = Instantiate(collisionParticles, collision.contacts[0].point, Quaternion.identity);
ps.Play();
Destroy(ps.gameObject, ps.main.duration);

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
