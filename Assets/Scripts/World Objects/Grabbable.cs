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
        //Debug.Log("Hide Press F");
        UIManager.Instance.DisableInteractionPrompt();

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
        if (isGrabbed) return;

        //Debug.Log("Show Press F");
        UIManager.Instance.EnableInteractionPrompt();
    }

    public void OnInteractionHoverExit()
    {
        if (isGrabbed) return;
        //Debug.Log("Hide Press F");
        UIManager.Instance.DisableInteractionPrompt();
    }

    public void SetGrabPointOrigin(Transform point)
    {
        grabPoint = point;
    }
}
