using System;
using UnityEngine;

public class PaintableDetector : MonoBehaviour
{
    public bool Output
    {
        get { return output; }
    }

    public Action<bool> OnOutputChange;

    [SerializeField] protected Color correctColour = Color.gray5;
    [SerializeField] protected bool output = false;

    protected virtual void UpdateOutput()
    {

    }
}
