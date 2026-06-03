using UnityEngine;
using UnityEngine.UI;
using System.Collections; 

public class EndLevelChecks : MonoBehaviour
{
    [SerializeField] private int level, goScale;
    [SerializeField] private Sprite checkWin, checkFail;

    [SerializeField] private Image inTimeIcon, photoTakenIcon, notDamagedIcon;

    [SerializeField] private float delayBetweenIcons = 0.2f;
    [SerializeField] private float animDuration = 0.35f;
    [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


    private void OnEnable() {
        bool[] checks = GameManager.Instance.levelChecks;

        notDamagedIcon.sprite = checks[level * 3]     ? checkWin : checkFail;
        photoTakenIcon.sprite = checks[level * 3 + 1] ? checkWin : checkFail;
        inTimeIcon.sprite     = checks[level * 3 + 2] ? checkWin : checkFail;

        StartCoroutine(AnimateIcons());
    }

    private IEnumerator AnimateIcons() {
        Image[] icons = { inTimeIcon, photoTakenIcon, notDamagedIcon };

        foreach (var icon in icons) {
            icon.transform.localScale = Vector3.zero;
        }
        foreach (var icon in icons) {
            yield return StartCoroutine(ScaleIn(icon.transform));
            yield return new WaitForSeconds(delayBetweenIcons);
        }
    }

    private IEnumerator ScaleIn(Transform t) {
        float elapsed = 0f;

        while (elapsed < animDuration) {
            elapsed += Time.deltaTime;
            float scale = scaleCurve.Evaluate(elapsed / animDuration);
            t.localScale = Vector3.one * scale * goScale;
            yield return null;
        }

        t.localScale = Vector3.one * goScale;
    }

}
