using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameStop;
    public Action<float> OnCountdownChange;

    [SerializeField] private float gameCountdown;
    [SerializeField] private bool gameRunning;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private SignalReceiver signalReceiver;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameCountdown >= 0 && gameRunning)
        {
            gameCountdown -= Time.deltaTime;
            OnCountdownChange?.Invoke(gameCountdown);

            if (gameCountdown < 0)
            {
                StopGame();
            }
        }

        #region User Inputs
        if (playableDirector != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                playableDirector.time = playableDirector.duration;
            }
        }

        #endregion
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
        gameRunning = true;
    }

    public void StopGame()
    {
        gameRunning = false;
        OnGameStop?.Invoke();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
