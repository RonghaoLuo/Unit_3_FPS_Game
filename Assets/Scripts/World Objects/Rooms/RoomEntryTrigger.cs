using UnityEngine;

public class RoomEntryTrigger : MonoBehaviour
{
    [SerializeField] private RoomKeeper myRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Entered Trigger");
            myRoom.OnRoomEntryTrigger();
            gameObject.SetActive(false);
        }
    }
}
