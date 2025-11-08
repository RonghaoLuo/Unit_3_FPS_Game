using System;
using UnityEngine;

public abstract class NpcCommand
{
    public CommandableNpc targetingNpc;
    public Action OnCommandComplete;

    //public abstract bool IsComplete();

    public abstract void Execute();

    public abstract void Cancel();
}
