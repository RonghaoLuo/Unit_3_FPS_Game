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
    public struct PoolDefinition 
    { 
        public PoolableType type; 
        public GameObject prefab; 
        public int initialSize; 
    }

    private Dictionary<PoolableType, SimplePool> poolMap = 
        new Dictionary<PoolableType, SimplePool>();

    [SerializeField] private PoolDefinition[] definedPools;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        for (int i = 0; i < definedPools.Length; i++)
        {
            PoolDefinition def = definedPools[i];
            poolMap[def.type] = new SimplePool(def.prefab, gameObject.transform, def.initialSize);
        }
    }

    public GameObject Spawn(PoolableType type, Vector3 pos, Quaternion rot, Vector3 velocity, Transform parent = null)
    {
        if (!poolMap.TryGetValue(type, out var pool))
        {
            Debug.LogError($"[PoolManager] No pool for {type}");
            return null;
        }
        GameObject go = pool.Get();

        go.transform.SetParent(parent);
        go.transform.position = pos;
        go.transform.rotation = rot;

        if (go.TryGetComponent<Rigidbody>(out var rb)) 
            rb.linearVelocity = velocity;

        return go;
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