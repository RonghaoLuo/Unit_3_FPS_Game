using UnityEngine;

public class ChaseNpcState : NpcState
{
    public Transform target;

    protected float timer = 0f;
    protected float pathCalculationTime = 1f;

    public ChaseNpcState(WanderingNpc owner) : base(owner)
    {

    }

    public override void OnStateEnter()
    {
        //Debug.Log("Player Detected. Reacting...");
        timer = pathCalculationTime;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateRun()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            npc.SetAgentDestination(target.position);
            timer = pathCalculationTime;
        }
    }
}
