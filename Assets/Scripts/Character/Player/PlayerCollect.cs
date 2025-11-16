using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private PlayerShootPaintball shoot;

    private void OnTriggerEnter(Collider other)
    {
        ICollectible collectible = other.GetComponentInParent<ICollectible>();

        if (collectible == null)
        {
            return;
        }

        Debug.Log("Hit a collectible");

        collectible.CollectTo(this);
    }

    public void StartPowerUP(float projectileSpeed, float size, float effectRafius, 
        float shootCooldown, float duration, bool shootRandomColour)
    {
        shoot.StartPowerUp(projectileSpeed, size, effectRafius, shootCooldown, duration, true);
    }
}
