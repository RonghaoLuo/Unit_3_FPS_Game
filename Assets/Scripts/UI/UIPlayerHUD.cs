using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIPlayerHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI TopBarText;
    [SerializeField] private Image[] paintIcons;
    [SerializeField] private Image selectionOutline;
    [SerializeField] private TextMeshProUGUI interactionPrompt;
    [SerializeField] private UIDurationDown powerUpDurationDown;
    [SerializeField] private TextMeshProUGUI HotbarText;

    private TimeSpan formattedTime;

    public void SetCountdown(float timesLeft)
    {
        formattedTime = new TimeSpan(0, 0, (int)timesLeft);
        TopBarText.text = formattedTime.Minutes + ":" + formattedTime.Seconds;
    }

    private void SetOutlinePosition(Transform transform)
    {
        selectionOutline.transform.position = transform.position;
    }

    private void SetOutlinePosition(int paintIconIndex)
    {
        SetOutlinePosition(paintIcons[paintIconIndex].transform);
    }

    private void SetPaintIconColour(int iconIndex, Color colour)
    {
        paintIcons[iconIndex].color = colour;
    }

    private void SetInteractionPromptVisibility(bool isVisible)
    {
        interactionPrompt.gameObject.SetActive(isVisible);
    }

    private void StartPowerUpCountdown(float duration)
    {
        powerUpDurationDown.StartCountdown(duration);
    }

    private void SetHotbarText(string text)
    {
        HotbarText.text = text;
    }

    private void Awake()
    {
        UIManager.Instance.RegisterPlayerHUD(this);
        gameObject.SetActive(false);

        UIManager.Instance.OnUpdateSelectionOutline += SetOutlinePosition;
        UIManager.Instance.OnUpdatePaintIcon += SetPaintIconColour;
        UIManager.Instance.OnToggleInteractionPrompt += SetInteractionPromptVisibility;
        UIManager.Instance.OnPowerUpCountdown += StartPowerUpCountdown;
        UIManager.Instance.OnUpdateHotbarText += SetHotbarText;
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnUpdateSelectionOutline -= SetOutlinePosition;
        UIManager.Instance.OnUpdatePaintIcon -= SetPaintIconColour;
        UIManager.Instance.OnToggleInteractionPrompt -= SetInteractionPromptVisibility;
        UIManager.Instance.OnPowerUpCountdown -= StartPowerUpCountdown;
        UIManager.Instance.OnUpdateHotbarText -= SetHotbarText;
    }
}
