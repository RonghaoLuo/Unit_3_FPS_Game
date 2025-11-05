using System;
using System.Collections.Generic;
using UnityEngine;

public class PaintableDetector : MonoBehaviour
{
    public Action<bool> OnOutputChange;
    
    public bool Output
    {
        get { return output; }
    }
 
    [SerializeField] private Color correctColour;
    [SerializeField] private List<Paintable> paintablesDetected;
    //[SerializeField] private float checkTime = 1f;

    [SerializeField] private bool output;

    private void Awake()
    {
        paintablesDetected = new List<Paintable>();
    }

    private void OnDestroy()
    {
        foreach (var paintable in paintablesDetected)
        {
            if (paintable == null) continue;
            paintable.OnColourChange -= UpdateOutput;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Filter for only paintables
        if (!other.TryGetComponent<Paintable>(out var paintable))
        {
            Debug.Log(other.gameObject.name + " is not paintable");
            return;
        }
        Debug.Log(other.gameObject.name + " is paintable");
        // If this paintable has correct colour, output true regardless of other paintables
        if (paintable.PaintColour == correctColour)
        {
            bool oldOutput = output;

            output = true;

            if (oldOutput != output)
                OnOutputChange?.Invoke(output);
        }
        paintablesDetected.Add(paintable);
        paintable.OnColourChange += UpdateOutput;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Paintable>(out var paintable))
        {
            return;
        }
        // If this paintable has correct colour, check if other paintables also have
        paintablesDetected.Remove(paintable);
        paintable.OnColourChange -= UpdateOutput;
        if (paintable.PaintColour == correctColour)
        {
            UpdateOutput();
        }
    }

    private bool DetectedPaintablesHasCorrectColour()
    {
        //paintablesDetected.RemoveAll(null);
        List<Paintable> copyOfPaintables = new(paintablesDetected);
        foreach (var paintable in copyOfPaintables)
        {
            if (paintable == null) continue;
            if (paintable.PaintColour == correctColour) return true;
            else continue;
        }

        return false;
    }

    private void UpdateOutput()
    {
        bool oldOutput = output;

        output = DetectedPaintablesHasCorrectColour();

        if (oldOutput != output)
            OnOutputChange?.Invoke(output);
    }


    //private void Check()
    //{
    //    List<Paintable> copyOfPaintables = new(paintables);
    //    foreach (var paintable in copyOfPaintables)
    //    {
    //        if (paintable.PaintColour != correctColour) continue;

    //        // if at least one correct paintable is found
    //        OnActivation?.Invoke();
    //        isActivated = true;
    //        return;
    //    }
    //    // if no correct paintable is found
    //    OnDeactivation?.Invoke();
    //    isActivated = false;
    //}
}
