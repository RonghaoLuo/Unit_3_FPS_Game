using UnityEngine;

public class Prey : WanderingNpc, IDefeatable
{
    [SerializeField][Range(0, 1)] private float itemDropChance = 0.5f;

    public void OnHit()
    {
        //Debug.Log("Defeatable Hit");
        OnDeath();
    }

    private void OnDeath()
    {
        //Debug.Log("Defeatable Died");
        NpcManager.Instance.DespawnNpc(this);
        GenerateDeathEffect();
        DropItem();
    }

    private void GenerateDeathEffect()
    {

    }

    private void DropItem()
    {
        float random = Random.value;
        if (random < itemDropChance || random == 1f)
        {
            CollectionManager.Instance.TrySpawnPowerUp(transform.position);
        }
    }
}
