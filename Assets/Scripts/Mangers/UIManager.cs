using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private PlayerHUD playerHUD;

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
        GameManager.Instance.OnCountdownChange += UpdateCountdown;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCountdownChange -= UpdateCountdown;
    }

    private void UpdateCountdown(float timesLeft)
    {
        if (playerHUD == null)
        {
            return;
        }

        playerHUD.SetCountdown(timesLeft);
    }

    #region Register Methods
    public void RegisterPlayerHUD(PlayerHUD hud)
    {
        playerHUD = hud;
    }
    #endregion
}
