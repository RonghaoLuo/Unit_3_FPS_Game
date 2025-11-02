using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image[] paintIcons;
    [SerializeField] private Image selectionOutline;

    private TimeSpan formattedTime;

    public void SetCountdown(float timesLeft)
    {
        formattedTime = new TimeSpan(0, 0, (int)timesLeft);
        countdownText.text = formattedTime.Minutes + ":" + formattedTime.Seconds;
    }

    public void SetOutlinePosition(Transform transform)
    {
        selectionOutline.transform.position = transform.position;
    }

    public void SetOutlinePosition(uint paintIconIndex)
    {
        SetOutlinePosition(paintIcons[paintIconIndex].transform);
    }

    private void Start()
    {
        UIManager.Instance.RegisterPlayerHUD(this);
        gameObject.SetActive(false);

        UIManager.Instance.OnUpdateSelectionOutline += SetOutlinePosition;
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnUpdateSelectionOutline -= SetOutlinePosition;
    }
}
