using UnityEngine;
using UnityEngine.EventSystems;

public class SoundButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    public bool horning;

    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        horning = true;
        audioSource.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        audioSource.Stop();
        horning = false;
    }
}
