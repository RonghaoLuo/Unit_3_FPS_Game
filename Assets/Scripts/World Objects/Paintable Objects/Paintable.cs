using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private Color paintColour;

    public Color PaintColour
    {
        get { return paintColour; }
    }

    public void SetColour(Color newColour)
    {
        paintColour = newColour;
        meshRenderer.material.color = newColour;
    }
}