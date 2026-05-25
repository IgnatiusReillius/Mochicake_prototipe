using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraCtrl : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3[] eulerRotations;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private int currentIndex = 0;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, smoothSpeed * Time.deltaTime);

        Quaternion target = Quaternion.Euler(eulerRotations[currentIndex]);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, smoothSpeed * Time.deltaTime);
    }

    public void RotateRight() {
        currentIndex = (currentIndex + 1) % eulerRotations.Length;
    }

    public void RotateLeft()
    {
        currentIndex = (currentIndex - 1 + eulerRotations.Length) % eulerRotations.Length;
    }
}
