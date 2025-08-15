using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform eyeOrigin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(eyeOrigin.position, eyeOrigin.forward * 5f);
        RaycastHit hitInfo;
        if (Physics.Raycast(eyeOrigin.position, eyeOrigin.forward, out hitInfo, 5f))
        {
            
        }
    }
}
