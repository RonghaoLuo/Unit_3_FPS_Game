using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintableDetectorHandler : MonoBehaviour
{
    public UnityEvent OnOutputTrue, OnOutputFalse;

    [SerializeField] private List<PaintableDetector> detectors = new();
    [SerializeField] private List<Door> targets = new();

    [SerializeField] private int totalDetectors = 0;
    [SerializeField] private int totalTrues = 0;
    [SerializeField][Range(0, 1)] private float detectionThreshold = 1f;

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

        if (totalTrues >= totalDetectors * detectionThreshold)
        {
            OnOutputTrue?.Invoke();
            foreach (Door target in targets)
            {
                target.OpenDoor();
            }
        }
        else
        {
            OnOutputFalse?.Invoke();
            foreach (Door target in targets)
            {
                target.CloseDoor();
            }
        }
    }
}
