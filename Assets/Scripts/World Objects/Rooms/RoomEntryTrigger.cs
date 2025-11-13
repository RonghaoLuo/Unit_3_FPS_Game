using UnityEngine;

public class RoomEntryTrigger : MonoBehaviour
{
    [SerializeField] private RoomKeeper myRoom;

    private void OnTriggerEnter(Collider other)
    {
        myRoom.OnRoomEntryTrigger();
        gameObject.SetActive(false);
    }
}
