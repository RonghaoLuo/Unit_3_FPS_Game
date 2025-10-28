using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSignal : MonoBehaviour
{
    [SerializeField] private float checkTime = 1f;
    [SerializeField] private List<PaintableDetector> detectors = new();
    [SerializeField] private List<Door> targets = new();

    private void Start()
    {
        StartPeriodicUpdate();
    }

    private bool IsAllInputTrue()
    {
        HashSet<PaintableDetector> copyOfDetectors = new(detectors);

        if (detectors.Count < 1) 
            return false;
        foreach (var detector in copyOfDetectors)
        {
            if (!detector.IsTrue) return false;
            continue;
        }

        return true;
    }

    private void OutputToAllTargets()
    {
        HashSet<Door> copyOfTargets = new(targets);

        if (IsAllInputTrue())
        {
            foreach (var target in copyOfTargets)
            {
                target.OpenDoor();
            }
        }
        else
        {
            foreach (var target in copyOfTargets)
            {
                target.CloseDoor();
            }
        }
    }

    private IEnumerator PeriodicUpdate()
    {
        while (true)
        {
            OutputToAllTargets();
            yield return new WaitForSeconds(checkTime);
        }
    }

    public void StartPeriodicUpdate()
    {
        StartCoroutine(PeriodicUpdate());
    }

    public void StopPeriodicUpdate()
    {
        StopCoroutine(PeriodicUpdate());
    }
}
