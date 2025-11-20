using UnityEngine;

public class MainMenu : MonoBehaviour
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
