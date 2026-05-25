using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RudderCtrl : MonoBehaviour, IDragHandler
{
    public float currentAngle = 0f;
    private RectTransform rectTransform;
    [SerializeField] private rb playerRB;
    [SerializeField] private float maxAngle;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentAngle = Mathf.Clamp(currentAngle - eventData.delta.x, -maxAngle, maxAngle);
        rectTransform.localEulerAngles = new Vector3(0f, 0f, currentAngle);
        playerRB.SetWheelRotation(currentAngle / maxAngle);
    }
}
