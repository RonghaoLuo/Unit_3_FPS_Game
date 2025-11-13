using UnityEngine;

public class RoomKeeper : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    public void OnRoomEntryTrigger()
    {
        GameManager.Instance.StartChallenge(spawnPoints);
        // close door behind and other stuff

    }
}
