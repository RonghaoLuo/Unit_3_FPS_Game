using UnityEngine;

public interface IPoolable
{
    PoolableType Type { get; }
    void OnUse();
    void OnReturn();
}
