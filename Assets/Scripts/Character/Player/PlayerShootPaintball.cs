using UnityEngine;

public class PlayerShootPaintball : MouseClickStrategy
{
    [SerializeField] private Transform weaponTip;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private PoolableType toShoot;

    private float cooldownTimer;

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public override void ExecuteStrategy()
    {
        if (cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }
    }

    public void Shoot()
    {
        PoolManager.Instance.Spawn(toShoot, weaponTip.transform, projectileSpeed);
    }
}
