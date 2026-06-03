using UnityEngine;
using UnityEngine.UI;

public class deadCount : MonoBehaviour
{
    [SerializeField] private rb shipRb;
    [SerializeField] private Text deadCountText;

    [SerializeField] private int baseNumber = 1000;
    [SerializeField] private int randomMin = 1, randomMax = 999;

    private void OnEnable()
{
    if (shipRb.damage == 0)
    {
        gameObject.SetActive(false);
        return;
    }

    int count = baseNumber * shipRb.damage + Random.Range(randomMin, randomMax);
    deadCountText.text = "Ha habido " + count.ToString() + " muertos.";
}
}
