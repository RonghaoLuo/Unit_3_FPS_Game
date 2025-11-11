using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NpcManager : MonoBehaviour
{
    private Dictionary<GameObject, StateNpc> gameObjectToNpcMap = new();

    public static NpcManager Instance { get; private set; }

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
    }

    public void SpawnNpc(Vector3 position)
    {
        GameObject go = PoolManager.Instance.Spawn(PoolableType.Prey);

        if (!gameObjectToNpcMap.TryGetValue(go, out StateNpc npc))
        {
            return;
        }

        npc.SetPosition(position);
    }

    public void SpawnNpcRandomlyOnSpawnPoints(Transform[] spawnPoints)
    {
        SpawnNpc(spawnPoints[ Random.Range( 0, spawnPoints.Length - 1 ) ].position);
    }

    public void DespawnNpc(StateNpc npc)
    {
        PoolManager.Instance.ReturnToPool(npc);
    }

    public void DespawnNpc(IPoolable poolable)
    {
        PoolManager.Instance.ReturnToPool(poolable);
    }

    public void RegisterNpc(GameObject go, StateNpc npc)
    {
        gameObjectToNpcMap.Add(go, npc);
    }

}
