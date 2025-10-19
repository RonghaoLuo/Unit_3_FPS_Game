using UnityEngine;


public interface IGrabbable : IInteractable
{
    bool IsGrabbed { get; }
    void Grab(Transform parent);
    void Release();
}
