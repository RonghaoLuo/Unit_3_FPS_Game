using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NpcManager : MonoBehaviour
{
    [SerializeField] float preySpawnFrequency = 1.0f;
    [SerializeField] int maxNumOfPrey = 1;
    [SerializeField] int numOfPreysSpawned;

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

    private void Start()
    {
        GameManager.Instance.OnPreySpawningBegin += StartPreySpawning;
        GameManager.Instance.OnPreySpawningEnd += StopPreySpawning;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPreySpawningBegin -= StartPreySpawning;
        GameManager.Instance.OnPreySpawningEnd -= StopPreySpawning;
    }

    public void SpawnNpc(Vector3 position)
    {
        numOfPreysSpawned++;
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
        numOfPreysSpawned--;
        PoolManager.Instance.ReturnToPool(npc);
    }

    public void DespawnNpc(IPoolable poolable)
    {
        numOfPreysSpawned--;
        PoolManager.Instance.ReturnToPool(poolable);
    }

    public void RegisterNpc(GameObject go, StateNpc npc)
    {
        gameObjectToNpcMap.Add(go, npc);
    }

    #region Coroutines
    private void StartPreySpawning(Transform[] spawnPoints)
    {
        StartCoroutine(PreySpawningCoroutine(spawnPoints));
    }

    private void StopPreySpawning()
    {
        StopCoroutine(PreySpawningCoroutine(null));
    }

    IEnumerator PreySpawningCoroutine(Transform[] spawnPoints)
    {
        while (true)
        {
            if (numOfPreysSpawned < maxNumOfPrey)
            {
                SpawnNpcRandomlyOnSpawnPoints(spawnPoints);
                yield return new WaitForSeconds(preySpawnFrequency);
            }
            yield return null;
        }
    }

    #endregion
}
