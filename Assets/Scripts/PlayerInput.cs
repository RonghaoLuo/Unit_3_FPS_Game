using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDirection, _lookRotation;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterRotation _rotation;
    [SerializeField] private CharacterSprint _sprint;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.z = Input.GetAxisRaw("Vertical");
        _moveDirection = _moveDirection.normalized;

        _lookRotation.x = -Input.GetAxisRaw("Mouse Y");
        _lookRotation.y = Input.GetAxisRaw("Mouse X");

        _movement.MoveCharacter(_moveDirection);
        _rotation.RotateByAngles(_lookRotation);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _sprint.StartSprinting();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _sprint.StopSprinting();
        }
    }
}
