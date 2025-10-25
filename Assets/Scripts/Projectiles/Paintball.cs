using UnityEngine;

public class Paintball : Projectile
{
    [SerializeField] private Color _color;
    [SerializeField] private float effectRadius;
    [SerializeField] private LayerMask paintableMask;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        Collider[] paintableColliders = Physics.OverlapSphere(gameObject.transform.position, effectRadius, paintableMask);

        OnDespawn();

        foreach (Collider collider in paintableColliders)
        {
            if (!collider.gameObject.TryGetComponent<Paintable>(out Paintable paintable))
                continue;
            paintable.SetColour(_color);
        }
    }
}
