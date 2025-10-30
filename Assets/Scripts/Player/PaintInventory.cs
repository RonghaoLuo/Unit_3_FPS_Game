using System.Collections.Generic;
using UnityEngine;

public class PaintInventory : MonoBehaviour
{
    public static Color selectedColour = Color.gray;

    [SerializeField] private List<KeyCodeColorPair> keyCodeToColorMap = new();

    private Dictionary<KeyCode, Color> actualKeyCodeToColorMap;
    private Dictionary<Color, bool> colorToAvailabilityMap = new();

    private void Awake()
    {
        actualKeyCodeToColorMap = new();
        foreach (KeyCodeColorPair pair in keyCodeToColorMap)
        {
            actualKeyCodeToColorMap.Add(pair.keyCode, pair.color);
            colorToAvailabilityMap.Add(pair.color, true);
        }
    }

    public void TrySelectColour(Color color)
    {
        if (color == null || !colorToAvailabilityMap.ContainsKey(color)) return;
        if (colorToAvailabilityMap[color])
        {
            selectedColour = color;
        }
    }

    public void TrySelectColourWithKeyCode(KeyCode code)
    {
        TrySelectColour(actualKeyCodeToColorMap[code]);
    }

    [System.Serializable]
    private struct KeyCodeColorPair
    {
        public KeyCode keyCode;
        public Color color;
    }
}
