using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectorMark : MonoBehaviour
{
    [SerializeField] private GameObject[] levelsPosition;
    [SerializeField] private MenuSystem menuSystem;
    [SerializeField] private float moveSpeed = 15f;

    private int currentLevel;
    private Vector3 targetPosition;

    void Start() {
        if (menuSystem == null)
            menuSystem = FindObjectOfType<MenuSystem>();

        currentLevel = menuSystem.SelectedLevel;
        targetPosition = levelsPosition[currentLevel - 1].transform.position;
        transform.position = targetPosition;
    }

    void Update() {
        int selectedLevel = menuSystem.SelectedLevel;
        if (selectedLevel != currentLevel) {
            currentLevel = selectedLevel;
            targetPosition = levelsPosition[currentLevel - 1].transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
