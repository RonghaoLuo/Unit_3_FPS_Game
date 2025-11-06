using System.Collections.Generic;
using UnityEngine;

public class PaintableDetectorHandler : MonoBehaviour
{
    [SerializeField] private List<PaintableDetector> detectors = new();
    [SerializeField] private List<Door> targets = new();

    [SerializeField] private int totalDetectors = 0;
    [SerializeField] private int totalTrues = 0;

    private void Start()
    {
        totalDetectors = detectors.Count;

        foreach (PaintableDetector detector in detectors)
        {
            if (detector.Output)
            {
                totalTrues++;
            }
            detector.OnOutputChange += UpdateOutput;
        }
    }

    private void OnDestroy()
    {
        foreach (PaintableDetector detector in detectors)
        {
            detector.OnOutputChange -= UpdateOutput;
        }
    }

    private void UpdateTotalTrues(bool newInput)
    {
        if (newInput)
        {
            totalTrues++;
        }
        else
        {
            totalTrues--;
        }
    }

    private void UpdateOutput(bool newInput)
    {
        UpdateTotalTrues(newInput);

        if (totalTrues == totalDetectors)
        {
            foreach (Door target in targets)
            {
                target.OpenDoor();
            }
        }
        else
        {
            foreach(Door target in targets)
            {
                target.CloseDoor();
            }
        }
    }
}
