using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Door door;

    public void OnInteract()
    {
        door.isDoorUnlocked = !door.isDoorUnlocked;
    }

    public void OnInteractionHoverEnter()
    {
        Debug.Log("Press F to interact");
    }

    public void OnInteractionHoverExit()
    {
        Debug.Log("Exited Door Button");
    }
}
