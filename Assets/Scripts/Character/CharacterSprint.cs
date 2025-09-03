using UnityEngine;

public class CharacterSprint : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _walkSpeed;

    private void Awake()
    {
        _movement.SetMoveSpeed(_walkSpeed);
    }

    public void StartSprinting()
    {
        _movement.SetMoveSpeed(_sprintSpeed);
    }

    public void StopSprinting()
    {
        _movement.SetMoveSpeed(_walkSpeed);
    }
}
