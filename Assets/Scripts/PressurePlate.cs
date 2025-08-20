using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    // could do it so that only one cube can trigger
    //[SerializeField] private Rigidbody correctRigidbody;

    public UnityEvent OnPressureEnter;
    public UnityEvent OnPressureExit;

    private void OnTriggerEnter(Collider other)
    {
        OnPressureEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnPressureExit?.Invoke();
    }
}
