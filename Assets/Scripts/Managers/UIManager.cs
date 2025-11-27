using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Action<int> OnUpdateSelectionOutline;
    public Action<int, Color> OnUpdatePaintIcon;
    public Action<bool> OnToggleInteractionPrompt;
    public Action<float> OnPowerUpCountdown;

    [SerializeField] private UIPlayerHUD playerHUD;
    [SerializeField] private UIPauseMenu pauseMenu;

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
        GameManager.Instance.OnIntroFinish += HandleIntroFinish;
        GameManager.Instance.OnTogglePauseGame += TogglePauseMenu;
        GameManager.Instance.OnResetManagers += ResetManager;
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCountdownChange -= UpdateCountdown;
        GameManager.Instance.OnIntroFinish -= HandleIntroFinish;
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

    private void HandleIntroFinish()
    {
        if (playerHUD == null) return;

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

    public void EnableInteractionPrompt()
    {
        OnToggleInteractionPrompt?.Invoke(true);
    }

    public void DisableInteractionPrompt()
    {
        OnToggleInteractionPrompt?.Invoke(false);
    }

    public void StartPowerUpCountdown(float duration)
    {
        OnPowerUpCountdown?.Invoke(duration);
    }

    #region Register Methods
    public void RegisterPlayerHUD(UIPlayerHUD hud)
    {
        playerHUD = hud;
    }

    public void RegisterPauseMenu(UIPauseMenu menu)
    {
        pauseMenu = menu;
    }

    #endregion
}
