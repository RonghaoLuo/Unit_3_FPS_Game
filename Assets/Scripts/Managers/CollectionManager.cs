using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;

    public PaintInventory PlayerPaintInventory
    {
        get {  return playerPaintInventory; }
    }

    [SerializeField] private PaintInventory playerPaintInventory;

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

    public void RegisterInventory(PaintInventory inventory)
    {
        playerPaintInventory = inventory;
    }
}
