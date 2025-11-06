using System.Collections.Generic;
using UnityEngine;

public class DetectionForStaticPaintables : MonoBehaviour
{
    //[SerializeField] private List<Paintable> paintables = new();
    //[SerializeField] private List<Door> targets = new();
    //[SerializeField] private int numOfPaintables = 0;
    //[SerializeField] private int numOfPaintablesCorrect = 0;
    //[SerializeField] private bool detectForAnyColour = false;
    //[SerializeField] private Color specificColour = Color.gray5;

    //private void Start()
    //{
    //    InitializeNumbers();
    //    foreach (Paintable paintable in paintables)
    //    {
    //        paintable.OnColourChange += UpdateOutput;
    //    }
    //}

    //private void InitializeNumbers()
    //{
    //    numOfPaintables = paintables.Count;

    //    foreach (Paintable paintable in paintables)
    //    {
    //        if (detectForAnyColour && IsPaintedWithAny(paintable) ||
    //            !detectForAnyColour && IsPaintedWithSpecific(paintable))
    //        {
    //            numOfPaintablesCorrect++;
    //        }
    //    }
    //}

    //private void OnDestroy()
    //{
    //    foreach (Paintable paintable in paintables)
    //    {
    //        paintable.OnColourChange -= UpdateOutput;
    //    }
    //}

    //private void UpdateNumOfCorrects(bool newInput)
    //{
    //    if (newInput)
    //    {
    //        numOfPaintablesCorrect++;
    //    }
    //    else
    //    {
    //        numOfPaintablesCorrect--;
    //    }
    //}

    //private void UpdateOutput(bool newInput)
    //{
    //    UpdateNumOfCorrects(newInput);

    //    if (numOfPaintablesCorrect == numOfPaintables)
    //    {
    //        foreach (Door target in targets)
    //        {
    //            target.OpenDoor();
    //        }
    //    }
    //    else
    //    {
    //        foreach (Door target in targets)
    //        {
    //            target.CloseDoor();
    //        }
    //    }
    //}

    ///// <summary>
    ///// <paramref name="paintable"/> is painted with any colour 
    ///// </summary>
    ///// <param name="paintable"></param>
    ///// <returns></returns>
    //private bool IsPaintedWithAny(Paintable paintable)
    //{
    //    return paintable.PaintColour != Color.gray5;
    //}

    ///// <summary>
    ///// <paramref name="paintable"/> is painted with a specific colour defined by the class
    ///// </summary>
    ///// <param name="paintable"></param>
    ///// <returns></returns>
    //private bool IsPaintedWithSpecific(Paintable paintable)
    //{
    //    return paintable.PaintColour == specificColour;
    //}
}
