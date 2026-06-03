using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private SoundButton hornSound;

    [SerializeField] private float openDuration = 1f, animTime = 0f;

    [SerializeField] private bool shipInRange = false, isOpen = false;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string animClipName = "door animation";
    [SerializeField] private GameObject destructionVFX;


    void Awake() {
        hornSound = GameObject.Find("Horn").GetComponent<SoundButton>();
        doorAnimator.Play(animClipName, 0, 0f);
        doorAnimator.speed = 0f;
    }
    
    void Update() {
        bool shouldOpen = shipInRange && hornSound.horning;
        if (shouldOpen != isOpen) isOpen = shouldOpen;

        float target = isOpen ? 1f : 0f;
        animTime = Mathf.MoveTowards(animTime, target, Time.deltaTime / openDuration);
        doorAnimator.Play(animClipName, 0, animTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Horn")) {
            shipInRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Horn")) {
            shipInRange = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Instantiate(destructionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
