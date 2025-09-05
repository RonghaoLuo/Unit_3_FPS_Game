using UnityEngine;

public class CharacterShooting : MouseClickStrategy
{
    [SerializeField] private BulletPooling poolOfBullets;
    [SerializeField] private Transform weaponTip;

    private void Awake()
    {
        if (poolOfBullets == null)
        {
            poolOfBullets = FindAnyObjectByType<BulletPooling>();
        }
    }

    public override void ExecuteStrategy()
    {
        Shoot();
    }

    public void Shoot()
    {
        //Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
        Bullet newBullet = poolOfBullets.GetAvailableBullet();

        if (newBullet == null)
        {
            return;
        }

        newBullet.transform.position = weaponTip.position; ;
        newBullet.transform.rotation = weaponTip.rotation;
        
        newBullet.gameObject.SetActive(true);
        newBullet.UseBullet();
    }
}
