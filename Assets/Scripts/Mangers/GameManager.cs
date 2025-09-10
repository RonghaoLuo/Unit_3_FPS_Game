using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameStop;
    public Action<float> OnCountdownChange;

    [SerializeField] private float gameCountdown;
    [SerializeField] private bool gameRunning;

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
