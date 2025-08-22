using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] float strength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("ResetBullet", 5f);
        myRigidbody.AddForce(transform.forward * strength, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);
    }

    private void ResetBullet()
    {
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }
}
