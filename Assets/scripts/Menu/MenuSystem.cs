using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, levelMenu, pauseMenu, panelUI;
    [SerializeField] private int goToLevel = 1;
    public int SelectedLevel => goToLevel;
    [SerializeField] private GameObject[] checks, levelButtons, lockIcons;
    [SerializeField] private GameManager gameManager;


    public void Awake()
    {
        if (gameManager == null) {
            gameManager = GameObject.Find("Game manager").GetComponent<GameManager>();
        }

        for(int i = 0; i < checks.Length; i++) {
            if (gameManager.levelChecks[i]) {
                checks[i].SetActive(true);
            }
        }

        for (int i = 1; i < levelButtons.Length; i++) {
            bool unlocked = gameManager.levelChecks[(i - 1) * 3];
            lockIcons[i].SetActive(!unlocked);
            levelButtons[i].SetActive(unlocked);
        }
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            bool pausing = !pauseMenu.activeSelf;
            pauseMenu.SetActive(pausing);
            SetPaused(pausing);
        }
    }

    public void Play() {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
    public void GoBack() {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    private void SetPaused(bool paused) {
        Time.timeScale = paused ? 0f : 1f;
        panelUI.SetActive(!paused);
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        SetPaused(false);
    }

    public void SetLevelTo(int selectedLevel) {
        Debug.Log("Set level to " + selectedLevel);
        goToLevel = selectedLevel;
    }

    public void GoToLevel() {
        SceneManager.LoadScene(goToLevel);
    }

    public void NextLevel() {
        GameManager.Instance.StopTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayAgain() {
        SetPaused(false);
        GameManager.Instance.StopTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu() {
        SetPaused(false);
        GameManager.Instance.StopTime();
        SceneManager.LoadScene(0);
    }

    public void Exit() {
        Debug.Log("Saliendo");
        Application.Quit();
    }
}
