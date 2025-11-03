using System.Collections.Generic;
using UnityEngine;

public class PaintInventory : MonoBehaviour
{
    public static Color selectedPaint = Color.gray;

    [SerializeField] private List<KeyCodeIndexPair> keyCodeToIndexMap = new();
    [SerializeField] private List<Color> existPaints = new();
    [SerializeField] private List<bool> paintAvailability = new();

    private Dictionary<KeyCode, int> actualKeyCodeToIndexMap;
    private Dictionary<Color, int> paintToIndexMap;

    private void Awake()
    {
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
        SetPaintAvailability(colour, true);
    }


    [System.Serializable]
    private struct KeyCodeIndexPair
    {
        public KeyCode keyCode;
        public int index;
    }
}
