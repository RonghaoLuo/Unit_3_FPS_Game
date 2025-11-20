using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    [SerializeField] private int currentSceneIndex;
    [SerializeField] private int mainMenuSceneIndex = 1;
    [SerializeField] private int levelSceneIndex = 2;
    [SerializeField] private int testSceneIndex = 3;

    public static BootstrapManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex, LoadSceneMode.Additive);
        currentSceneIndex = mainMenuSceneIndex;
    }

    public void ReturnToMainMenu()
    {
        // Unload the current level scene and load the main menu again
        SceneManager.UnloadSceneAsync(currentSceneIndex);
        SceneManager.LoadSceneAsync(mainMenuSceneIndex, LoadSceneMode.Additive);
        currentSceneIndex = mainMenuSceneIndex;
    }

    public void StartGame()
    {
        // Unload the menu and load Level1
        currentSceneIndex = levelSceneIndex;
        SceneManager.UnloadSceneAsync(mainMenuSceneIndex);
        SceneManager.LoadSceneAsync(levelSceneIndex, LoadSceneMode.Additive);
    }

    public void StartTest()
    {
        SceneManager.UnloadSceneAsync(mainMenuSceneIndex);
        SceneManager.LoadSceneAsync(testSceneIndex, LoadSceneMode.Additive);
        currentSceneIndex = testSceneIndex;
    }

    public void RestartGame()
    {
        // Just reload Level1
        SceneManager.UnloadSceneAsync(currentSceneIndex);
        SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
    }
}
