using UnityEngine;

public abstract class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float lifeTime = 5f;
    [SerializeField] protected float spawnTime;
    [SerializeField] private PoolableType type;

    public PoolableType Type => type;

    public GameObject GameObject => gameObject;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void OnSpawn()
    {
        gameObject.SetActive(true);
        spawnTime = Time.time;

        if (rb != null) 
        { 
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero; 
            rb.WakeUp(); 
        }
        // reset any visual or owner info here
    }

    public virtual void OnDespawn()
    {
        // Stop particle systems, disable effects
        gameObject.SetActive(false);

        if (rb != null) 
        { 
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero; 
            rb.Sleep(); 
        }
    }

    protected virtual void Update()
    {
        if (Time.time - spawnTime >= lifeTime) 
            PoolManager.Instance.ReturnToPool(this);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        // apply damage/effects...
    }
}

