using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private Transform _head;

    [SerializeField] private Vector3 _currentRotation;

    public void RotateByAngles(Vector3 angles)
    {
        angles *= GameManager.Instance.MouseSensitivity;

        _currentRotation.x += angles.x;
        _currentRotation.y += angles.y;
        _currentRotation.x = Mathf.Clamp(_currentRotation.x, -70f, 70f);

        //transform.Rotate();
        transform.rotation = Quaternion.Euler(0, _currentRotation.y, 0);
        _head.localRotation = Quaternion.Euler(_currentRotation.x, 0, 0);
    }
}
