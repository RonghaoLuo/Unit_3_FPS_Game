using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _moveSpeed;

    public void MoveCharacter(Vector3 direction)
    {
        Vector3 forwardOrBackwards = transform.forward * _moveSpeed * direction.z;
        Vector3 leftOrRight = transform.right * _moveSpeed * direction.x;

        Vector3 sumOfDirections = forwardOrBackwards + leftOrRight;

        _controller.SimpleMove(sumOfDirections);

        // doesn't work since there's overwriting
        //_controller.SimpleMove(direction.z * _moveSpeed * transform.forward);
        //_controller.SimpleMove(direction.x * _moveSpeed * transform.right);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }
}
