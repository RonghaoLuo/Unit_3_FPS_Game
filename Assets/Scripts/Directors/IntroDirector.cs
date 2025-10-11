using UnityEngine;
using UnityEngine.Playables;

public class IntroDirector : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.RegisterIntroDirector(gameObject.GetComponent<PlayableDirector>());
    }

    public void HandleFinishIntro()
    {
        GameManager.Instance.FinishIntro();
    }
}
