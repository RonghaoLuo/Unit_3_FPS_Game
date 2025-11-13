using UnityEngine;
using UnityEngine.AI;

public class WanderingNpc : StateNpc
{

    protected virtual void Start()
    {
        //InvokeRepeating("FollowTarget", 1, 5);
        ChangeState(new WanderNpcState(this));
    }
    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateRun();
        }
    }
}
