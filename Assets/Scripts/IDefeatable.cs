using Unity.VisualScripting;
using UnityEngine;

public interface IDefeatable
{
    public IPoolable Poolable { get; }

    public void OnHit();
}

//public class Test: MonoBehaviour
//{
//    private IPoolable poolable;

//    private void Awake()
//    {
//        poolable = gameObject.GetComponent<IPoolable>();
//    }

//    public void OnHit()
//    {
//        Debug.Log("Defeatable Hit");
//        OnDeath();
//    }

//    private void OnDeath()
//    {
//        PoolManager.Instance.ReturnToPool(poolable);
//        GenerateDeathEffect();
//        DropItem();
//    }

//    private void GenerateDeathEffect()
//    {

//    }

//    private void DropItem()
//    {

//    }

//    private void SetActive()
//    {
//        gameObject.SetActive(true);
//    }
//}