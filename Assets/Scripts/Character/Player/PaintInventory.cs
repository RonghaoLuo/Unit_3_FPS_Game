using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Semi Manager since it does its own logics?
/// </summary>
public class PaintInventory : MonoBehaviour
{
    public Color SelectedPaint { get; private set; }

    public Color RandomPaint
    {
        get { return existPaints[Random.Range(0, existPaints.Count - 1)]; }
    }

    public int NumOfExistPaints
    {
        get { return numOfExistPaints; }
    }

    [SerializeField] private List<KeyCodeIndexPair> keyCodeToIndexMap = new();
    [SerializeField] private List<Color> existPaints = new();
    [SerializeField] private List<bool> paintAvailability = new();

    private Dictionary<KeyCode, int> actualKeyCodeToIndexMap;
    private Dictionary<Color, int> paintToIndexMap;
    private int numOfExistPaints;

    private void Awake()
    {
        numOfExistPaints = existPaints.Count;
        SelectedPaint = existPaints[0];

        actualKeyCodeToIndexMap = new();
        foreach (KeyCodeIndexPair pair in keyCodeToIndexMap)
        {
            actualKeyCodeToIndexMap.Add(pair.keyCode, pair.index);
        }
        paintToIndexMap = new();
        for (int i = 0; i < existPaints.Count; i++)
        {
            paintToIndexMap.Add(existPaints[i], i);
        }
    }

    private void Start()
    {
        CollectionManager.Instance.RegisterInventory(this);

        for (int i = 0; i < paintAvailability.Count; i++)
        {
            if (paintAvailability[i])
            {
                UIManager.Instance.OnUpdatePaintIcon?.Invoke(i, existPaints[i]);
            }
        }
    }

    private void TrySelectPaint(int index)
    {
        if (existPaints[index] == null || !paintAvailability[index]) return;

        SelectedPaint = existPaints[index];
        UIManager.Instance.OnUpdateSelectionOutline.Invoke(index);
    }

    private void SetPaintAvailability(Color colour, bool isAvailable)
    {
        paintAvailability[ paintToIndexMap[colour] ] = isAvailable;
    }

    public void TrySelectPaintWithKeyCode(KeyCode code)
    {
        TrySelectPaint( actualKeyCodeToIndexMap[code] );
    }

    public void CollectPaint(Color colour)
    {
        if (!existPaints.Contains(colour)) return;
        SetPaintAvailability(colour, true);
        UIManager.Instance.OnUpdatePaintIcon?.Invoke(paintToIndexMap[colour], colour);
    }

    [System.Serializable]
    private struct KeyCodeIndexPair
    {
        public KeyCode keyCode;
        public int index;
    }
}
