using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIPlayerHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image[] paintIcons;
    [SerializeField] private Image selectionOutline;
    [SerializeField] private TextMeshProUGUI interactionPrompt;

    private TimeSpan formattedTime;

    public void SetCountdown(float timesLeft)
    {
        formattedTime = new TimeSpan(0, 0, (int)timesLeft);
        countdownText.text = formattedTime.Minutes + ":" + formattedTime.Seconds;
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

    private void Awake()
    {
        UIManager.Instance.RegisterPlayerHUD(this);
        gameObject.SetActive(false);

        UIManager.Instance.OnUpdateSelectionOutline += SetOutlinePosition;
        UIManager.Instance.OnUpdatePaintIcon += SetPaintIconColour;
        UIManager.Instance.OnToggleInteractionPrompt += SetInteractionPromptVisibility;
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnUpdateSelectionOutline -= SetOutlinePosition;
        UIManager.Instance.OnUpdatePaintIcon -= SetPaintIconColour;
        UIManager.Instance.OnToggleInteractionPrompt -= SetInteractionPromptVisibility;
    }
}
