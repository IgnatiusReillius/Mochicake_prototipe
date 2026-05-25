using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    public bool horning;

    public void OnPointerDown(PointerEventData eventData) {
        horning = true;
        StartCoroutine(LoopSound());
    }

    public void OnPointerUp(PointerEventData eventData) {
        horning = false;
    }

    private IEnumerator LoopSound() {
        while (horning) {
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
    }
}

