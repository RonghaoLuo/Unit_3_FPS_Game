using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isDoorUnlocked;

    [SerializeField] Animator animator;

    public void OpenDoor()
    {
        animator.SetBool("IsOpen", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("IsOpen", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDoorUnlocked)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDoorUnlocked)
        {
            CloseDoor();
        }
    }
}
