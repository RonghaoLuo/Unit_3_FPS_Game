using UnityEngine;

public class RoomExitTrigger : MonoBehaviour
{
    [SerializeField] private RoomKeeper myRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Entered Trigger");
            myRoom.ExitRoom();
            gameObject.SetActive(false);
        }
    }
}
