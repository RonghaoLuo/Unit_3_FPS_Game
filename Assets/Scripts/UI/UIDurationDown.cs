using UnityEngine;
using UnityEngine.UI;

public class UIDurationDown : MonoBehaviour
{
    [SerializeField] private Image filledImage;
    private float duration;
    private float timer;
    private bool isActive;

    public void StartCountdown(float duration)
    {
        this.duration = duration;
        timer = this.duration;
        isActive = true;
        filledImage.fillAmount = 1f; // start full
    }

    private void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        filledImage.fillAmount = Mathf.Clamp01(timer / duration);

        if (timer <= 0f)
        {
            isActive = false;
            filledImage.fillAmount = 0f;
        }
    }
}
