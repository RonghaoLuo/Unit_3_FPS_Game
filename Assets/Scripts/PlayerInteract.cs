using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform eyeOrigin;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float raycastLength;

    public IInteractable currentInteraction = null;

    private RaycastHit hitInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(eyeOrigin.position, eyeOrigin.forward * raycastLength);
        bool isObject = Physics.Raycast(eyeOrigin.position, eyeOrigin.forward, out hitInfo, raycastLength, interactionLayer);
        //Debug.Log("Currently looking at " + hitInfo.collider);
        //Debug.Log("Currently looking at interaction " + currentInteraction);

        if (isObject)
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

            // interactable to diff interactable
            if (interactable != null && currentInteraction != null && currentInteraction != interactable)
            {
                currentInteraction.OnInteractionHoverExit();
                currentInteraction = interactable;
                currentInteraction.OnInteractionHoverEnter();
            }
            // interactable to object
            else if (interactable == null && currentInteraction != null)
            {
                currentInteraction.OnInteractionHoverExit();
                currentInteraction = null;
            }
            // object or void to interactable
            else if (currentInteraction == null && interactable != null)
            {
                currentInteraction = interactable;
                currentInteraction.OnInteractionHoverEnter();
            }
        }
        else
        {
            currentInteraction = null;
        }
        
        
    }
}
