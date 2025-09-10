using UnityEngine;

public class ColliderReaction : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask targetLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //RaycastHit hitInfo;

        //if (Physics.SphereCast(transform.position, detectionRange, Vector3.up, out hitInfo, 100f, targetLayer))
        //{
        //    Debug.Log(hitInfo.collider.name);
        //    if (hitInfo.collider.CompareTag("Enemy"))
        //    {
        //        // creating a state of "Investitating" and set destination to be transform.position
                
        //    }
        //}

        // can also make the object interact with interactibles
    }
}
