using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip menuMusic, levelMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            PlayForScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayForScene(scene.buildIndex);
    }

    private void PlayForScene(int sceneIndex)
    {
        AudioClip target = sceneIndex == 0 ? menuMusic : levelMusic;

        if (audioSource.clip == target) return;

        audioSource.clip = target;
        audioSource.loop = true;
        audioSource.Play();
    }
}
