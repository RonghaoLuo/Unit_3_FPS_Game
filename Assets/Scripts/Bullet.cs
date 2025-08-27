using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] float strength;

    private BulletPooling poolOwner;

    public void UseBullet()
    {
        Invoke("ResetBullet", 5f);
        myRigidbody.AddForce(transform.forward * strength, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);
    }

    public void InitializePooledBullet(BulletPooling owner)
    {
        poolOwner = owner;
        // do anything else as initializing
    }

    private void ResetBullet()
    {
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;

        poolOwner.ReturnBullet(this);

        gameObject.SetActive(false);
    }
}
