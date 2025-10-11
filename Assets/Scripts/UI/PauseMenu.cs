using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.RegisterPauseMenu(this);
        gameObject.SetActive(false);
    }

    public void OnMainMenuButton()
    {
        GameManager.Instance.ReturnToMainMenu();
    }

    public void OnRestartButton()
    {
        GameManager.Instance.RestartLevel();
    }

    public void OnCloseButton()
    {
        GameManager.Instance.TogglePauseGame();
    }
}
