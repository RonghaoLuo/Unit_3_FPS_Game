using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
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
        }
        
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("Assets/Scenes/MainMenu.unity", LoadSceneMode.Additive);
    }

    public void ReturnToMainMenu()
    {
        // Unload the current level scene and load the main menu again
        SceneManager.UnloadSceneAsync("Assets/Scenes/Level1.unity");
        SceneManager.LoadSceneAsync("Assets/Scenes/MainMenu.unity", LoadSceneMode.Additive);
    }

    public void StartGame()
    {
        // Unload the menu and load Level1
        SceneManager.UnloadSceneAsync("Assets/Scenes/MainMenu.unity");
        SceneManager.LoadSceneAsync("Assets/Scenes/Level1.unity", LoadSceneMode.Additive);
    }

    public void RestartGame()
    {
        // Just reload Level1
        SceneManager.UnloadSceneAsync("Assets/Scenes/Level1.unity");
        SceneManager.LoadSceneAsync("Assets/Scenes/Level1.unity", LoadSceneMode.Additive);
    }
}
