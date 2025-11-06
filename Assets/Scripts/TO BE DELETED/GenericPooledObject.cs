using UnityEngine;

public abstract class GenericPooledObject : MonoBehaviour
{
    public BulletPooling poolOwner;

    public abstract void InitialPooledObject();
    public abstract void ResetPooledObject();
}
