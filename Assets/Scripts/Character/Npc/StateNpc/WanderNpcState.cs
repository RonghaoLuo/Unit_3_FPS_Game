using UnityEngine;

public class WanderNpcState : NpcState
{
    public WanderNpcState(StateNpc owner) : base(owner)
    {

    }

    public override void OnStateEnter()
    {
        //npc.SetAgentDestination(npc.transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
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
