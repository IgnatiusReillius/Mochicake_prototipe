using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, levelMenu, pauseMenu;
    [SerializeField] private int goToLevel = 1;
    [SerializeField] private CanvasGroup panelUI;
    [SerializeField] private GameObject[] checks;
    [SerializeField] private GameManager gameManager;

    public void Awake()
    {
        if (gameManager == null) {
            gameManager = GameObject.Find("Game manager").GetComponent<GameManager>();
        }

        for(int i = 0; i < checks.Length; i++)
        {
            if (gameManager.levelChecks[i])
            {
                checks[i].SetActive(true);
            }
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

    private void SetPaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
        panelUI.interactable = !paused;
        panelUI.blocksRaycasts = !paused;
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
        gameManager.PlayTime();
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.PlayTime();
    }

    public void PlayAgain() {
        SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameManager.PlayTime();
    }

    public void BackToMenu() {
        SetPaused(false);
        SceneManager.LoadScene(0);
    }

    public void Exit() {
        Debug.Log("Saliendo");
        Application.Quit();
    }
}
