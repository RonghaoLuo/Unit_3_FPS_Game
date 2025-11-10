using UnityEngine;

public class Defeatable : MonoBehaviour
{
    public void OnHit()
    {
        Debug.Log("Defeatable Hit");
        OnDeath();
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
        Invoke("SetActive", 3f);
        GenerateDeathEffect();
        DropItem();
    }

    private void GenerateDeathEffect()
    {

    }

    private void DropItem()
    {

    }

    private void SetActive()
    {
        gameObject.SetActive(true);
    }
}
