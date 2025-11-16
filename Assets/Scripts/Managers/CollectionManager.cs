using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;

    public PaintInventory PlayerPaintInventory
    {
        get {  return playerPaintInventory; }
    }

    [SerializeField] private int NumOfPowerUpsPresent;
    [SerializeField] private int maxNumOfPowerUps = 3;

    [SerializeField] private PaintInventory playerPaintInventory;
    [SerializeField] private PowerUp powerUpPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        GameManager.Instance.OnResetManagers += ResetManager;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetManagers -= ResetManager;
    }

    private void ResetManager()
    {
        playerPaintInventory = null;
    }

    private void SpawnPowerUp(Vector3 position)
    {
        Instantiate(powerUpPrefab, position, Quaternion.identity);

        NumOfPowerUpsPresent++;
    }

    public void TrySpawnPowerUp(Vector3 position)
    {
        if (NumOfPowerUpsPresent >= maxNumOfPowerUps)
        {
            Debug.LogWarning("Too Many Power Ups Present!");
            return;
        }

        SpawnPowerUp(position);
    }

    public void DespawnPowerUp(PowerUp powerUp)
    {
        Debug.Log("Despawning Power Up");
        Destroy(powerUp.gameObject);
        NumOfPowerUpsPresent--;
    }

    public void RegisterInventory(PaintInventory inventory)
    {
        playerPaintInventory = inventory;
    }
}
