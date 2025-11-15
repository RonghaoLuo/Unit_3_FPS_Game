using UnityEngine;

public class PlayerShootPaintball : MouseClickStrategy
{
    [SerializeField] private Transform weaponTip;
    [SerializeField] private PoolableType toShoot;

    [Header("Base Stats")]
    [SerializeField] private float baseProjectileSpeed = 10f;
    [SerializeField] private float baseShootCooldown = 0.5f;

    [Header("Multipliers")]
    [SerializeField] private float projectileSpeedMultiplier = 1f;
    [SerializeField] private float shootCooldownMultiplier = 1f;

    private float CurrentProjectileSpeed => baseProjectileSpeed * projectileSpeedMultiplier;
    private float CurrentShootCooldown => baseShootCooldown * shootCooldownMultiplier;


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
            cooldownTimer = CurrentShootCooldown;
        }
    }

    public void Shoot()
    {
        PoolManager.Instance.Spawn(toShoot, weaponTip.transform, CurrentProjectileSpeed);
    }
}
