using System;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color paintColour = Color.gray5;

    private Color oldColour = Color.gray5;

    public Action OnColourChange;

    public Color PaintColour
    {
        get { return paintColour; }
    }

    private void Awake()
    {
        paintColour = meshRenderer.material.color;
        oldColour = paintColour;
    }

    public void SetColour(Color newColour)
    {
        oldColour = paintColour;
        paintColour = newColour;
        meshRenderer.material.color = newColour;

        if (paintColour != oldColour)
        {
            OnColourChange?.Invoke();
        }
    }
}