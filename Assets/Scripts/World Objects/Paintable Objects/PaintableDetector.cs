using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintableDetector : MonoBehaviour
{
    public UnityEvent OnActivation, OnDeactivation;
    
    public bool IsActivated
    {
        get { return isActivated; }
    }
 
    [SerializeField] private Color correctColour;
    [SerializeField] private List<Paintable> paintables;
    [SerializeField] private float checkTime = 1f;

    private bool isActivated;

    private void Awake()
    {
        paintables = new List<Paintable>();
    }

    private void Start()
    {
        StartPeriodicCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Paintable>(out Paintable paintable)){
            return;
        }
        paintables.Add(paintable);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Paintable>(out Paintable paintable))
        {
            return;
        }
        paintables.Remove(paintable);
    }

    public void StartPeriodicCheck()
    {
        StartCoroutine(PeriodicCheck());
    }

    public void StopPeriodicCheck()
    {
        StopCoroutine(PeriodicCheck());
    }

    private IEnumerator PeriodicCheck()
    {
        while (true)
        {
            Check();
            yield return new WaitForSeconds(checkTime);
        }        
    }

    private void Check()
    {
        List<Paintable> copyOfPaintables = new(paintables);
        foreach (var paintable in copyOfPaintables)
        {
            if (paintable.PaintColour != correctColour) continue;

            // if at least one correct paintable is found
            OnActivation?.Invoke();
            isActivated = true;
            return;
        }
        // if no correct paintable is found
        OnDeactivation?.Invoke();
        isActivated = false;
    }
}
