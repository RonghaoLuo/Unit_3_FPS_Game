using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action OnIntroFinish;
    public Action OnGameOver;
    public Action<float> OnCountdownChange;
    public Action OnTogglePauseGame;
    public Action OnResetManagers;

    [SerializeField] private float gameCountdown;
    [SerializeField] private bool gameRunning;
    [SerializeField] private PlayableDirector introDirector;
    [SerializeField] private PlayerInput playerInput;

    private float gameCountdownInitialNumber;

    public bool IsGameRunning
    {
        get { return gameRunning; }
    }

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
    }

    void Start()
    {
        gameCountdownInitialNumber = gameCountdown;
    }

    private void OnDestroy()
    {
    }

    void Update()
    {
        if (gameCountdown >= 0 && gameRunning)
        {
            gameCountdown -= Time.deltaTime;
            OnCountdownChange?.Invoke(gameCountdown);

            if (gameCountdown < 0)
            {
                GameOver();
            }
        }

        #region User Inputs
        if (introDirector != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                introDirector.time = introDirector.duration;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePauseGame();
        }

        #endregion
    }

    public void FinishIntro()
    {
        OnIntroFinish?.Invoke();
        gameRunning = true;
    }

    public void TogglePauseGame()
    {
        gameRunning = !gameRunning;
        Time.timeScale = gameRunning ? 1f : 0f;  // Pause/unpause gameplay
        Cursor.visible = !gameRunning;
        Cursor.lockState = gameRunning ? CursorLockMode.Locked : CursorLockMode.None;

        playerInput.enabled = gameRunning;

        OnTogglePauseGame?.Invoke();
    }

    private void GameOver()
    {
        gameRunning = false;
        OnGameOver?.Invoke();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToMainMenu()
    {
        BootstrapManager.Instance.ReturnToMainMenu();
        ResetAllManagers();
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        BootstrapManager.Instance.RestartGame();
        ResetAllManagers();
        Time.timeScale = 1f;
    }

    private void ResetAllManagers()
    {
        OnResetManagers?.Invoke();

        gameCountdown = gameCountdownInitialNumber;
        playerInput = null;
        introDirector = null;
    }

    #region Register
    public void RegisterIntroDirector(PlayableDirector director)
    {
        introDirector = director;
    }

    public void RegisterPlayerInput(PlayerInput input)
    {
        playerInput = input;
    }

    #endregion
}
