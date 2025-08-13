using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] float strength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
        myRigidbody.AddForce(transform.forward * strength, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
