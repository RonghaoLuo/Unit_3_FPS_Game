using UnityEngine;

public class WanderNpcState : NpcState
{
    public WanderNpcState(WanderingNpc owner) : base(owner)
    {

    }

    public override void OnStateEnter()
    {
        //.SetAgentDestination(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateRun()
    {
        if (!npc.IsMoving())
        {
            npc.ChangeState(new IdleNpcState(npc));
        }
    }
}
