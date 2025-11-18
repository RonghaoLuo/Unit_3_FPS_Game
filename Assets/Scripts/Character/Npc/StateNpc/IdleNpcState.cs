using UnityEngine;

public class IdleNpcState : NpcState
{
    private float timer = 0;

    public IdleNpcState(StateNpc owner) : base(owner)
    {
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Start Idle");
        timer = 2f;// Random.Range(2f, 4f);
    }

    public override void OnStateExit()
    {
        //Debug.Log("Not Idle anymore");
    }

    public override void OnStateRun()
    {
        //Debug.Log("waiting for command");
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            npc.ChangeState(new WanderNpcState(npc));
        }
    }
}
