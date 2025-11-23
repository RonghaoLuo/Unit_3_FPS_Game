using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        Vector3 lookPos = transform.position + cam.forward;
        Vector3 up = Vector3.up;
        transform.LookAt(lookPos, up);
    }
}
