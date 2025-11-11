using UnityEngine;

public class PlayerShootPaintball : MouseClickStrategy
{
    [SerializeField] private Transform weaponTip;
    [SerializeField] private float shootSpeed;
    [SerializeField] private PoolableType toShoot;


    public override void ExecuteStrategy()
    {
        Shoot();
    }

    public void Shoot()
    {
        PoolManager.Instance.Spawn(toShoot, weaponTip.transform, shootSpeed);
    }
}
