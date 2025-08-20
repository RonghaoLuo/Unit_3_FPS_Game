using UnityEngine;


public class CubeGrab : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody cubeRigidbody;

    private bool isGrabbed;

    private Transform grabPoint;

    public void OnInteract()
    {
        if (isGrabbed)
        {
            // stopped grabbing the cube
            cubeRigidbody.isKinematic = false;

            // set parent to be the original parent
            cubeRigidbody.transform.SetParent(null);
        }
        else
        {
            // started grabbing the cube
            cubeRigidbody.isKinematic= true;


            // set parent to be = camera/player
            cubeRigidbody.transform.SetParent(Camera.main.transform);

            // set position to be 1.5 m away form camera
            cubeRigidbody.transform.position = grabPoint.position + grabPoint.forward * 2f;
            cubeRigidbody.transform.localRotation = Quaternion.identity;
        }

        isGrabbed = !isGrabbed;
    }

    public void OnInteractionHoverEnter()
    {
        
    }

    public void OnInteractionHoverExit()
    {
        
    }

    public void SetGrabPointOrigin(Transform point)
    {
        grabPoint = point;
    }
}
