using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    //[SerializeField] float preySpawnFrequency = 1.0f;
    //[SerializeField] int maxNumOfPrey = 1;
    [SerializeField] int totalNumOfPreysSpawned;

    private Dictionary<GameObject, StateNpc> gameObjectToNpcMap = new();
    private Dictionary<RoomKeeper, Coroutine> roomToSpawnCoroutineMap = new();
    private Dictionary<RoomKeeper, HashSet<Prey>> roomToPreysMap = new();
    //private Coroutine runningSpawnCoroutine;

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
        GameManager.Instance.OnChallengeBegin += StartChallengeSpawning;
        GameManager.Instance.OnChallengeEnd += StopChallengeSpawning;
        GameManager.Instance.OnResetManagers += ResetManager;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnChallengeBegin -= StartChallengeSpawning;
        GameManager.Instance.OnChallengeEnd -= StopChallengeSpawning;
        GameManager.Instance.OnResetManagers -= ResetManager;
    }

    private void StartChallengeSpawning(RoomKeeper room, Transform[] spawnPoints,
        int maxNumOfPreys, float spawnInterval)
    {
        StartPreySpawning(room, spawnPoints, maxNumOfPreys, spawnInterval);
    }

    private void StopChallengeSpawning(RoomKeeper room)
    {
        StopPreySpawning(room);
    }

    private void ResetManager()
    {
        totalNumOfPreysSpawned = 0;

        foreach (var kvp in roomToSpawnCoroutineMap)
        {
            StopCoroutine(kvp.Value);
        }
        roomToSpawnCoroutineMap.Clear();

        foreach (var kvp in roomToPreysMap)
        {
            foreach (Prey prey in kvp.Value)
            {
                PoolManager.Instance.ReturnToPool(prey);
            }
        }
        roomToPreysMap.Clear();
    }

    public StateNpc SpawnNpc(RoomKeeper room, Vector3 position)
    {
        totalNumOfPreysSpawned++;
        GameObject go = PoolManager.Instance.Spawn(PoolableType.Prey);

        if (!gameObjectToNpcMap.TryGetValue(go, out StateNpc npc))
        {
            Debug.LogError("[NpcManager] Spawned NPC not registered!");
            return null;
        }

        npc.SetPosition(position);
        npc.SetCurrentRoom(room);
        return npc;
    }

    public StateNpc SpawnNpcRandomlyOnSpawnPoints(RoomKeeper room, Transform[] spawnPoints)
    {
        return SpawnNpc(room, spawnPoints[ Random.Range( 0, spawnPoints.Length - 1 ) ].position);
    }

    public void DespawnNpc(StateNpc npc)
    {
        totalNumOfPreysSpawned--;
        PoolManager.Instance.ReturnToPool(npc);
        roomToPreysMap[npc.GetCurrentRoom()].Remove(npc as Prey);
    }

    public void DespawnNpc(IPoolable poolable)
    {
        totalNumOfPreysSpawned--;
        PoolManager.Instance.ReturnToPool(poolable);
        roomToPreysMap[(poolable as StateNpc).GetCurrentRoom()].Remove(poolable as Prey);
    }

    public void RegisterNpc(GameObject go, StateNpc npc)
    {
        gameObjectToNpcMap.Add(go, npc);
    }

    #region Coroutines
    public void StartPreySpawning(RoomKeeper room, Transform[] spawnPoints, 
        int maxNumOfPreys, float spawnInterval)
    {
        if (roomToPreysMap.ContainsKey(room))
        {
            Debug.LogWarning("[NpcManager] duplicate room for prey count");
            roomToPreysMap[room].Clear();
            return;
        }
        roomToPreysMap.Add(room, new());

        if (roomToSpawnCoroutineMap.ContainsKey(room))
        {
            Debug.LogWarning("[NpcManager] duplicate room for prey spawning");
            roomToSpawnCoroutineMap[room] = StartCoroutine(PreySpawningCoroutine(
                room, spawnPoints, maxNumOfPreys, spawnInterval));
            return;
        }
        roomToSpawnCoroutineMap.Add(room, StartCoroutine(PreySpawningCoroutine(
            room, spawnPoints, maxNumOfPreys, spawnInterval)));
    }

    public void StopPreySpawning(RoomKeeper room)
    {
        if (!roomToSpawnCoroutineMap.TryGetValue(room, out Coroutine spawnCoroutine))
        {
            return;
        }

        StopCoroutine(spawnCoroutine);
    }

    IEnumerator PreySpawningCoroutine(RoomKeeper room, Transform[] spawnPoints, 
        int maxNumOfPreys, float spawnInterval)
    {
        WaitForSeconds wait = new WaitForSeconds(spawnInterval);

        while (true)
        {
            if (roomToPreysMap[room].Count < maxNumOfPreys)
            {
                Prey prey = SpawnNpcRandomlyOnSpawnPoints(room, spawnPoints) as Prey;
                if (prey == null)
                {
                    Debug.LogError("[NpcManager] Failed to spawn Prey");
                    yield return wait;
                    continue;
                }

                roomToPreysMap[room].Add(prey);
            }

            // Always wait — even if you didn't spawn
            yield return wait;
        }
    }

    #endregion
}
