using UnityEngine;

public class RudderVisuals : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] RudderCtrl parentTest;
    [SerializeField] private float maxAngle, multiplier;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.localEulerAngles = new Vector3(0f, 0f, parentTest.currentAngle * multiplier);
    }

}
