using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Semi Manager since it does its own logics?
/// </summary>
public class PaintInventory : MonoBehaviour
{
    public static Color selectedPaint;

    [SerializeField] private List<KeyCodeIndexPair> keyCodeToIndexMap = new();
    [SerializeField] private List<Color> existPaints = new();
    [SerializeField] private List<bool> paintAvailability = new();

    private Dictionary<KeyCode, int> actualKeyCodeToIndexMap;
    private Dictionary<Color, int> paintToIndexMap;

    private void Awake()
    {
        selectedPaint = existPaints[0];

        CollectionManager.Instance.RegisterInventory(this);

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

        selectedPaint = existPaints[index];
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
