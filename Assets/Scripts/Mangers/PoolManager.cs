using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Can pool any GameObject
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [System.Serializable]
    private struct PoolDefinition 
    { 
        public PoolableType type; 
        public GameObject prefab; 
        public int initialSize;
        [System.NonSerialized]
        public Transform poolParent;
    }

    private Dictionary<PoolableType, SimplePool> poolMap = 
        new Dictionary<PoolableType, SimplePool>();

    [SerializeField] private PoolDefinition[] definedPools;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        { 
            Destroy(gameObject); 
            return; 
        }

        for (int i = 0; i < definedPools.Length; i++)
        {
            PoolDefinition def = definedPools[i];

            GameObject poolParent = new(def.type.ToString());
            poolParent.transform.SetParent(this.transform, false);
            poolMap[def.type] = new SimplePool(def.prefab, poolParent.transform, def.initialSize);
        }
    }

    public GameObject Spawn(PoolableType type, Vector3 pos, Quaternion rot, Vector3 velocity)
    {
        if (!poolMap.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] No pool for {type}");
            return null;
        }
        GameObject go = pool.Get();

        go.transform.position = pos;
        go.transform.rotation = rot;

        if (go.TryGetComponent<Rigidbody>(out var rb)) 
            rb.linearVelocity = velocity;

        return go;
    }

    public GameObject Spawn(PoolableType type, Transform shootOrigin, float shootSpeed)
    {
        return Spawn(type, shootOrigin.transform.position, 
            shootOrigin.transform.rotation, 
            shootOrigin.forward * shootSpeed);
    }

    public void ReturnToPool(IPoolable poolable)
    {
        if (!poolMap.TryGetValue(poolable.Type, out var pool))
        {
            Debug.LogWarning($"[PoolManager] No pool for {poolable.Type}, destroying.");
            Destroy(poolable.GameObject);
            return;
        }

        pool.Return(poolable);
    }
}


public enum PoolableType
{
    Ball, Paintball, Bullet, Enemy
}