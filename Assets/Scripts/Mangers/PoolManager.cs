using System.Collections.Generic;
using UnityEngine;

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

    public PoolDefinition[] definedPools;
    Dictionary<PoolableType, SimplePool> poolMap = new Dictionary<PoolableType, SimplePool>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        for (int i = 0; i < definedPools.Length; i++)
        {
            PoolDefinition def = definedPools[i];
            poolMap[def.type] = new SimplePool(def.prefab, this.transform, Mathf.Max(1, def.initialSize));
        }
    }

    public GameObject Spawn(PoolableType type, Vector3 pos, Quaternion rot, Vector3 velocity, Transform parent = null)
    {
        if (!poolMap.TryGetValue(type, out var pool))
        {
            Debug.LogError($"No pool for {type}");
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

    public void ReturnToPool(GameObject go)
    {
        if (go == null) return;
        if (!poolMap.TryGetValue(go.GetComponent<IPoolable>().Type, out var pool))
        {
            Debug.LogWarning("No pool for this object type, destroying.");
            Destroy(go.gameObject);
            return;
        }
        pool.Return(go);
    }
}


public enum PoolableType
{
    Paintball, Bullet, Enemy
}