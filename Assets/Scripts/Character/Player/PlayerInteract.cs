using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform eyeOrigin;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float raycastLength = 4f;

    private IInteractable currentInteraction;
    private RaycastHit hitInfo;

    void Update()
    {
        Debug.DrawRay(eyeOrigin.position, eyeOrigin.forward * raycastLength, Color.yellow);

        IInteractable interactable = PerformRaycast();
        HandleHoverState(interactable);

        if (Input.GetKeyDown(KeyCode.F) && currentInteraction != null)
            Interact();
    }

    private IInteractable PerformRaycast()
    {
        if (Physics.Raycast(eyeOrigin.position, eyeOrigin.forward, out hitInfo, raycastLength, interactionLayer))
        {
            return hitInfo.collider.GetComponent<IInteractable>();
        }

        return null;
    }

    private void HandleHoverState(IInteractable newInteraction)
    {
        if (newInteraction == currentInteraction)
            return; // nothing changed

        // EXIT previous
        if (currentInteraction != null)
            currentInteraction.OnInteractionHoverExit();

        currentInteraction = newInteraction;

        // ENTER new
        if (currentInteraction != null)
            currentInteraction.OnInteractionHoverEnter();
    }

    private void Interact()
    {
        if (currentInteraction is Grabbable grabbable)
            grabbable.SetGrabPointOrigin(eyeOrigin);

        currentInteraction.OnInteract();
    }
}
