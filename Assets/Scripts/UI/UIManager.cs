using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        TimeSpan formattedTime = new TimeSpan(0, 0, (int) timesLeft);
        countdownText.text = formattedTime.Minutes + ":" + formattedTime.Seconds;
    }
}
