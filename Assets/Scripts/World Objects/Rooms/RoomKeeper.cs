using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomKeeper : MonoBehaviour
{
    [System.Serializable]
    private struct RenderersAndColour
    {
        public Renderer[] renderers;
        public Color colour;
    }

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool roomIsCompleted = false;
    [SerializeField] private List<RenderersAndColour> toBePaintedOnComplete;

    public UnityEvent OnRoomEntry, OnRoomComplete, OnRoomExit;

    public void EnterRoom()
    {
        OnRoomEntry?.Invoke();
    }

    public void ExitRoom()
    {
        OnRoomExit?.Invoke();
    }

    public void CompleteRoom()
    {
        if (roomIsCompleted) return;
        roomIsCompleted = true;

        OnRoomComplete?.Invoke();

        Debug.Log("Room Completed!");
    }

    public void StartPreySpawning()
    {
        GameManager.Instance.StartChallenge(spawnPoints);
    }

    public void PaintTheRoom()
    {
        foreach (RenderersAndColour item in toBePaintedOnComplete)
        {
            foreach(Renderer renderer in item.renderers)
            {
                renderer.material.color = item.colour;
            }
        }
    }
}
