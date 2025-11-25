using UnityEngine;
using UnityEngine.AI;

public class WanderingNpc : StateNpc
{
    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateRun();
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        ChangeState(new WanderNpcState(this));
    }
}
