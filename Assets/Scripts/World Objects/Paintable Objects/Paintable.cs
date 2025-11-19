using System;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private Color paintColour = Color.gray5;
    [SerializeField] private bool enableSetColour = true;

    private Color oldColour = Color.gray5;

    public Action OnColourChange;

    public Color PaintColour
    {
        get { return paintColour; }
    }

    private void Awake()
    {
        paintColour = myRenderer.material.color;
        oldColour = paintColour;
    }

    public void SetColour(Color newColour)
    {
        if (!enableSetColour) return;

        oldColour = paintColour;
        paintColour = newColour;
        myRenderer.material.color = newColour;

        if (paintColour != oldColour)
        {
            OnColourChange?.Invoke();
        }
    }

    public void EnableSetColour()
    {
        enableSetColour = true;
    }

    public void DisableSetColour()
    {
        enableSetColour = false;
    }
}