using UnityEngine;

public class LevelInit : MonoBehaviour
{
    [SerializeField] private bool waitForTutorial = false;
    private bool tutorialDone = false;
    private bool started = false;

    public void OnTutorialComplete()
    {
        tutorialDone = true;
    }

    private void Update()
    {
        if (!started && (!waitForTutorial || tutorialDone) && Input.GetMouseButtonDown(0))
        {
            started = true;
            GameManager.Instance.PlayTime();
        }
    }
}
