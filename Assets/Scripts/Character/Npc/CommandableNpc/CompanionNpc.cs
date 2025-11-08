using UnityEngine;

public class CompanionNpc : CommandableNpc
{
    [SerializeField] protected PlayerShootPaintball shooting;

    public PlayerShootPaintball GetShooting()
    {
        return shooting;
    }
}
