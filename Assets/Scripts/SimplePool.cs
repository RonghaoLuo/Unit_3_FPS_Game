using System.Collections.Generic;
using UnityEngine;

public class SimplePool
{
    // All assigned in constructor
    readonly GameObject prefab;
    readonly Transform parent;
    readonly Queue<GameObject> queue = new Queue<GameObject>();
    readonly int initialSize;

    public SimplePool(GameObject prefab, Transform parent = null, int initialSize = 10)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.initialSize = initialSize;
        Prewarm(initialSize);
    }

    void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject createdObject = Object.Instantiate(prefab, parent);
            createdObject.SetActive(false);
            queue.Enqueue(createdObject);
        }
    }

    public GameObject Get()
    {
        GameObject go;
        if (queue.Count > 0)
        {
            go = queue.Dequeue();
        }
        else
        {
            go = Object.Instantiate(prefab, parent);
        }
        go.GetComponent<IPoolable>().OnUse();
        return go;
    }

    public void Return(GameObject go)
    {
        go.GetComponent<IPoolable>().OnReturn();
        queue.Enqueue(go);
    }
}
