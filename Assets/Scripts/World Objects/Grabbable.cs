using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody cubeRigidbody;

    private bool isGrabbed;
    private Transform grabPoint;

    public bool IsGrabbed
    {
        get { return isGrabbed; }
        private set { isGrabbed = value; }
    }

    public void Grab(Transform parent)
    {
        // started grabbing the cube
        cubeRigidbody.isKinematic = true;


        // set parent to be = camera/player
        cubeRigidbody.transform.SetParent(parent);

        // set position to be 1.5 m away form camera
        cubeRigidbody.transform.position = grabPoint.position + grabPoint.forward * 2f;
        cubeRigidbody.transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        // stopped grabbing the cube
        cubeRigidbody.isKinematic = false;

        // set parent to be the original parent
        cubeRigidbody.transform.SetParent(null);
    }

    public void OnInteract()
    {
        if (isGrabbed)
        {
            Release();
        }
        else
        {
            Grab(Camera.main.transform);
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
