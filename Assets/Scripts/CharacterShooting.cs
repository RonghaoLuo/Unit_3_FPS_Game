using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    [SerializeField] private BulletPooling poolOfBullets;
    [SerializeField] private Transform weaponTip;

    public void Shoot()
    {
        //Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
        Bullet newBullet = poolOfBullets.GetAvailableBullet();
        newBullet.transform.position = weaponTip.position; ;
        newBullet.transform.rotation = weaponTip.rotation;
        
        newBullet.gameObject.SetActive(true);

        // call the return bullet method in some way
        //poolOfBullets.ReturnBullet(newBullet);
    }
}
