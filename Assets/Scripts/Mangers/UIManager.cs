using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private PauseMenu pauseMenu;

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
        GameManager.Instance.OnCountdownChange += UpdateCountdown;
        GameManager.Instance.OnIntroFinish += HandleGameStart;
        GameManager.Instance.OnTogglePauseGame += TogglePauseMenu;
        GameManager.Instance.OnResetManagers += ResetManager;
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCountdownChange -= UpdateCountdown;
        GameManager.Instance.OnIntroFinish -= HandleGameStart;
        GameManager.Instance.OnTogglePauseGame -= TogglePauseMenu;
        GameManager.Instance.OnResetManagers -= ResetManager;
    }

    private void UpdateCountdown(float timesLeft)
    {
        if (playerHUD == null)
        {
            return;
        }

        playerHUD.SetCountdown(timesLeft);
    }

    private void HandleGameStart()
    {
        playerHUD.gameObject.SetActive(true);
    }

    private void TogglePauseMenu()
    {
        if (pauseMenu == null) return;

        if (GameManager.Instance.IsGameRunning)
        {
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }

    private void ResetManager()
    {
        playerHUD = null;
        pauseMenu = null;
    }

    #region Register Methods
    public void RegisterPlayerHUD(PlayerHUD hud)
    {
        playerHUD = hud;
    }

    public void RegisterPauseMenu(PauseMenu menu)
    {
        pauseMenu = menu;
    }

    #endregion
}
