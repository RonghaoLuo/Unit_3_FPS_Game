using System.Collections.Generic;
using UnityEngine;

public class PaintInventory : MonoBehaviour
{
    public static Color selectedPaint = Color.gray;

    [SerializeField] private List<KeyCodeIndexPair> keyCodeToIndexMap = new();
    [SerializeField] private List<Color> existPaints = new();
    [SerializeField] private List<bool> paintAvailability = new();

    private Dictionary<KeyCode, int> actualKeyCodeToIndexMap;

    private void Awake()
    {
        actualKeyCodeToIndexMap = new();
        foreach (KeyCodeIndexPair pair in keyCodeToIndexMap)
        {
            actualKeyCodeToIndexMap.Add(pair.keyCode, pair.index);
        }
    }

    private void TrySelectPaint(int index)
    {
        if (existPaints[index] == null || !paintAvailability[index]) return;

        selectedPaint = existPaints[index];
    }

    public void TrySelectPaintWithKeyCode(KeyCode code)
    {
        TrySelectPaint(actualKeyCodeToIndexMap[code]);
    }


    [System.Serializable]
    private struct KeyCodeIndexPair
    {
        public KeyCode keyCode;
        public int index;
    }
}
