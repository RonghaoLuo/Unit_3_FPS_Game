using System;
using System.Collections;
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
    public Action<Transform[]> OnPreySpawningBegin;
    public Action OnPreySpawningEnd;

    [SerializeField] private float challengeCountdown;
    [SerializeField] private bool gameRunning;
    [SerializeField] private bool countdownRunning;
    [SerializeField] private PlayableDirector introDirector;
    [SerializeField] private PlayerInput playerInput;

    [ContextMenu("Debug Start Game")] private void DebugStartGame()
    {
        FinishIntro();
    }

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
            return;
        }
    }

    void Start()
    {
        gameCountdownInitialNumber = challengeCountdown;
    }

    private void OnDestroy()
    {
    }

    void Update()
    {
        // For Challenge
        if (challengeCountdown >= 0 && gameRunning && countdownRunning)
        {
            challengeCountdown -= Time.deltaTime;
            OnCountdownChange?.Invoke(challengeCountdown);

            if (challengeCountdown < 0)
            {
                ChallengeFail();
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
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    countdownRunning = !countdownRunning;
        //}

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

    public void StartChallenge(Transform[] spawnPoints)
    {
        // do some effects
        Debug.Log("Starting Challenge...");

        StartPreySpawning(spawnPoints);
        countdownRunning = true;
    }

    public void StopChallenge()
    {
        StopPreySpawning();
        countdownRunning = false;
    }

    public void ChallengeComplete()
    {
        StopChallenge();
        // do some effect
        // win cutscene
        //win screen
        Debug.Log("You Won");

        GameOver();
    }

    public void ChallengeFail()
    {
        StopChallenge();
        // do some effect
        // challenge lost cutscene
        // challenge lost screen
        Debug.Log("You Lost");

        GameOver();
    }

    private void GameOver()
    {
        gameRunning = false;
        countdownRunning = false;
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

        challengeCountdown = gameCountdownInitialNumber;
        playerInput = null;
        introDirector = null;
    }

    private void StartPreySpawning(Transform[] spawnPoints)
    {
        OnPreySpawningBegin?.Invoke(spawnPoints);
    }

    private void StopPreySpawning()
    {
        OnPreySpawningEnd?.Invoke();
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
