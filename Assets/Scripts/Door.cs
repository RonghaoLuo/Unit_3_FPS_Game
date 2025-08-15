using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isDoorUnlocked;

    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (isDoorUnlocked)
        {
            animator.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDoorUnlocked)
        {
            animator.SetBool("IsOpen", false);
        }
    }
}
