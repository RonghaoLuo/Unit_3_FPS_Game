using UnityEngine;
using UnityEngine.AI;

public class WanderingNpc : StateNpc
{

    void Start()
    {
        //InvokeRepeating("FollowTarget", 1, 5);
        ChangeState(new WanderNpcState(this));
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateRun();
        }
    }
}
