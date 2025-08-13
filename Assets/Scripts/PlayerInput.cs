using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDirection, _lookRotation;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterRotation _rotation;
    [SerializeField] private CharacterSprint _sprint;
    [SerializeField] private CharacterJump jump;
    [SerializeField] private CharacterShooting shooting;

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

        if (_movement != null)
        {
            _movement.MoveCharacter(_moveDirection);
        }
        if (_rotation != null)
        {
            _rotation.RotateByAngles(_lookRotation);
        }
        if (_sprint != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _sprint.StartSprinting();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _sprint.StopSprinting();
            }
        }
        if (jump != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump.Jump();
            }
        }
        if (shooting != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shooting.Shoot();
            }
        }
    }
}
