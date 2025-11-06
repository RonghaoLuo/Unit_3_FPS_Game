using UnityEngine;

public class StaticPaintableDetector : PaintableDetector
{
    [SerializeField] private Paintable paintable;
    [SerializeField] private bool detectForAnyColour = false;

    private bool oldOutput = false;

    private void Awake()
    {
        UpdateOutput();

        paintable.OnColourChange += UpdateOutput;
    }

    private void OnDestroy()
    {
        paintable.OnColourChange -= UpdateOutput;
    }

    /// <summary>
    /// <paramref name="paintable"/> is painted with any colour 
    /// </summary>
    /// <param name="paintable"></param>
    /// <returns></returns>
    private bool IsPaintedWithAny(Paintable paintable)
    {
        return !ColourTools.ColorsAreEqual(paintable.PaintColour, Color.gray5);
    }

    /// <summary>
    /// <paramref name="paintable"/> is painted with a specific colour defined by the class
    /// </summary>
    /// <param name="paintable"></param>
    /// <returns></returns>
    private bool IsPaintedWithSpecific(Paintable paintable)
    {
        return ColourTools.ColorsAreEqual(paintable.PaintColour, correctColour);
    }

    protected override void UpdateOutput()
    {
        base.UpdateOutput();

        if (paintable == null) return;

        oldOutput = output;

        if ( (detectForAnyColour && IsPaintedWithAny(paintable)) ||
             (!detectForAnyColour && IsPaintedWithSpecific(paintable)) ) 
        { 
            output = true; 
        }
        else 
        { 
            output = false; 
        }

        if (oldOutput != output)
        {
            OnOutputChange?.Invoke(output);
        }
    }
}
