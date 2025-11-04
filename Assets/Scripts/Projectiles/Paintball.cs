using UnityEngine;

public class Paintball : Projectile
{
    [SerializeField] private Color paintColor = Color.gray5;
    [SerializeField] private float effectRadius;
    [SerializeField] private LayerMask paintableMask;
    [SerializeField] private MeshRenderer meshRenderer;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        Collider[] paintableColliders = Physics.OverlapSphere(gameObject.transform.position, effectRadius, paintableMask);

        OnDespawn();

        foreach (Collider collider in paintableColliders)
        {
            if (!collider.gameObject.TryGetComponent<Paintable>(out Paintable paintable))
                continue;
            paintable.SetColour(paintColor);
        }
    }

    public override void OnSpawn()
    {
        paintColor = PaintInventory.selectedPaint;
        meshRenderer.material.color = paintColor;
        base.OnSpawn();
    }
}
