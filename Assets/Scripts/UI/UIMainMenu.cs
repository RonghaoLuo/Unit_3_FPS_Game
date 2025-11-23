using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public void OnStartGameButton()
    {
        GameManager.Instance.StartGame();
    }

    public void OnStartTestButton()
    {
        GameManager.Instance.StartTest();
    }
}
