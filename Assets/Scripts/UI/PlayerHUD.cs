using TMPro;
using UnityEngine;
using System;

public class PlayerHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI countdownText;

    private TimeSpan formattedTime;

    public void SetCountdown(float timesLeft)
    {
        formattedTime = new TimeSpan(0, 0, (int)timesLeft);
        countdownText.text = formattedTime.Minutes + ":" + formattedTime.Seconds;
    }



    private void Awake()
    {
        UIManager.Instance.RegisterPlayerHUD(this);
    }
}
