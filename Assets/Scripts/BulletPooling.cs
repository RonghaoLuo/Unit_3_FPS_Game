using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private List<Bullet> availableBullets = new List<Bullet>();
    [SerializeField] private List<Bullet> unavailableBullets = new List<Bullet>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            Bullet bulletClone = Instantiate(bulletPrefab, transform);
            bulletClone.gameObject.name = i.ToString();
            availableBullets.Add(bulletClone);
            bulletClone.gameObject.SetActive(false);
        }
    }

    public Bullet GetAvailableBullet()
    {
        Bullet firstAvailableBullet = availableBullets[0];
        availableBullets.RemoveAt(0);
        unavailableBullets.Add(firstAvailableBullet);

        return firstAvailableBullet;
    }

    public void ReturnBullet(Bullet usedBullet)
    {
        unavailableBullets.Remove(usedBullet);
        availableBullets.Add(usedBullet);
    }
}
