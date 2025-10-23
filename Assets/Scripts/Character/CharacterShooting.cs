using UnityEngine;

public class CharacterShooting : MouseClickStrategy
{
    [SerializeField] private Transform weaponTip;
    [SerializeField] private float shootSpeed;

    public override void ExecuteStrategy()
    {
        Shoot();
    }

    public void Shoot()
    {
        GameObject spawnedProjectile = PoolManager.Instance.Spawn(PoolableType.Ball, weaponTip.transform, shootSpeed);
    }
}
