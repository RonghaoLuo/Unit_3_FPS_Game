using UnityEngine;

public class Paintball : Projectile
{
    [SerializeField] private Color paintColor = Color.gray5;
    [SerializeField] private float effectRadius;
    [SerializeField] private LayerMask paintableMask;
    [SerializeField] private MeshRenderer meshRenderer;

    protected override void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Paintball Hit");
        base.OnCollisionEnter(collision);

        Vector3 overlapSpherePosition = transform.position;

        PoolManager.Instance.ReturnToPool(this);

        Collider[] paintableColliders = Physics.OverlapSphere(overlapSpherePosition, effectRadius, paintableMask);

        foreach (Collider collider in paintableColliders)
        {
            if (!collider.gameObject.TryGetComponent<Paintable>(out Paintable paintable))
            {
                //Debug.Log("Didn't get a Paintable");
            }
            if (paintable != null)
            {
                paintable.SetColour(paintColor);
            }


            if (!collider.gameObject.TryGetComponent<IDefeatable>(out IDefeatable defeatable))
            {
                Debug.Log("Didn't get a Defeatable");
            }
            if (defeatable != null)
            {
                defeatable.OnHit();
            }

        }
    }

    public override void OnSpawn()
    {
        paintColor = PaintInventory.selectedPaint;
        meshRenderer.material.color = paintColor;
        base.OnSpawn();
    }
}
