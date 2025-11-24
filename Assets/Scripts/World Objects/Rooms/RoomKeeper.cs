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

    public UnityEvent OnRoomEntry, OnRoomComplete, OnRoomUndoComplete, OnRoomExit;

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

    public void UndoCompletion()
    {
        if (!roomIsCompleted) return;
        roomIsCompleted = false;

        OnRoomUndoComplete?.Invoke();

        Debug.Log("Room Completion Undone!");
    }

    public void StartChallenge()
    {
        GameManager.Instance.StartChallenge(spawnPoints);
    }

    public void ChallengeComplete()
    {
        GameManager.Instance.ChallengeComplete();
        
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

    public void UnpaintTheRoom()
    {
        foreach (RenderersAndColour item in toBePaintedOnComplete)
        {
            foreach (Renderer renderer in item.renderers)
            {
                renderer.material.color = Color.gray5;
            }
        }
    }
}
