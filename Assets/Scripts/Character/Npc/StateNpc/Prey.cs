using UnityEngine;

public class Prey : WanderingNpc, IDefeatable
{
    public void OnHit()
    {
        Debug.Log("Defeatable Hit");
        OnDeath();
    }

    private void OnDeath()
    {
        Debug.Log("Defeatable Died");
        NpcManager.Instance.DespawnNpc(this);
        GenerateDeathEffect();
        DropItem();
    }

    private void GenerateDeathEffect()
    {

    }

    private void DropItem()
    {
        CollectionManager.Instance.TrySpawnPowerUp(transform.position);
    }
}
