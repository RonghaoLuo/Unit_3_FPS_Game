using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Door door;
    [SerializeField] private MeshRenderer buttonRenderer;
    [SerializeField] private Material hoverEnterMat;
    [SerializeField] private Material hoverExitMat;

    public void OnInteract()
    {
        door.isDoorUnlocked = !door.isDoorUnlocked;
    }

    public void OnInteractionHoverEnter()
    {
        buttonRenderer.material = hoverEnterMat;
    }

    public void OnInteractionHoverExit()
    {
        buttonRenderer.material = hoverExitMat;
    }
}
