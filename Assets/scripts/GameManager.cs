using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool[] levelChecks;
    [SerializeField] private bool canTimeRun = false;
    [SerializeField] private float elapsedTime = 0f;
    public float ElapsedTime => elapsedTime;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    private void Update() {
        if(canTimeRun){
            elapsedTime += Time.deltaTime;
        }
    }

    public void PlayTime()
    {
        canTimeRun = true;
    }
    public void StopTime()
    {
        canTimeRun = false;
        elapsedTime = 0f;
    }

    public void FinishLevel(int level, bool isNotDamaged, bool isPhotoTaken, bool isInTime) {

        if (isNotDamaged) {
            levelChecks[level * 3] = true;
        } 
        if (isPhotoTaken) {
            levelChecks[level * 3 + 1] = true;
        }
        if (isInTime) {
            levelChecks[level * 3 + 2] = true;
        }
    }
}
