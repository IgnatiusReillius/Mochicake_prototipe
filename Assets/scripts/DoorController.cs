using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private SoundButton hornSound;

    [SerializeField] private Transform leftPanel, rightPanel;
    [SerializeField] private float slideDistance = 2f, speed = 2f;

    [SerializeField] private bool shipInRange = false, isOpen = false;
    [SerializeField] private Vector3 leftClosed, rightClosed, leftOpen, rightOpen;

    void Awake() {
        hornSound = GameObject.Find("Horn").GetComponent<SoundButton>();

        leftClosed = leftPanel.localPosition;
        rightClosed = rightPanel.localPosition;
        leftOpen = leftClosed + Vector3.left * slideDistance;
        rightOpen = rightClosed + Vector3.right * slideDistance;
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            shipInRange = true;
        }
    }

    void OgerExit(Collider other) {
        if (other.CompareTag("Player")) {
            shipInRange = false;
        }
    }

    void Update() {
        bool shouldOpen = shipInRange && hornSound.horning;

        if (shouldOpen && !isOpen) {
            isOpen = true;
            StopAllCoroutines();
            StartCoroutine(MovePanels(leftOpen, rightOpen));
        } else if (!shouldOpen && isOpen) {
            isOpen = false;
            StopAllCoroutines();
            StartCoroutine(MovePanels(leftClosed, rightClosed));
        }
    }

    private IEnumerator MovePanels(Vector3 leftTarget, Vector3 rightTarget) {
        while (Vector3.Distance(leftPanel.localPosition, leftTarget) > 0.001f) {
            leftPanel.localPosition = Vector3.MoveTowards(leftPanel.localPosition, leftTarget, speed * Time.deltaTime);
            rightPanel.localPosition = Vector3.MoveTowards(rightPanel.localPosition, rightTarget, speed * Time.deltaTime);
            yield return null;
        }
        leftPanel.localPosition = leftTarget;
        rightPanel.localPosition = rightTarget;
    }
}
