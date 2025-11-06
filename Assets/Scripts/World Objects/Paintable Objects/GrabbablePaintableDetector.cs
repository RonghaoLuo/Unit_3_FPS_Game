using System;
using System.Collections.Generic;
using UnityEngine;

public class GrabbablePaintableDetector : PaintableDetector
{
    [SerializeField] private List<Paintable> paintablesDetected;

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
        List<Paintable> copyOfPaintables = new(paintablesDetected);
        foreach (var paintable in copyOfPaintables)
        {
            if (paintable == null) continue;
            if (paintable.PaintColour == correctColour) return true;
            else continue;
        }

        return false;
    }

    protected override void UpdateOutput()
    {
        base.UpdateOutput();

        bool oldOutput = output;

        output = DetectedPaintablesHasCorrectColour();

        if (oldOutput != output)
            OnOutputChange?.Invoke(output);
    }
}
