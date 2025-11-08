using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class FleeNpcState : ChaseNpcState
{
    protected float fleeDistance = 10f;

    public FleeNpcState(WanderingNpc owner) : base(owner)
    {
    }

    public override void OnStateRun()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Vector3 fleeDir = (npc.transform.position - target.position).normalized;
            Vector3 fleePos = npc.transform.position + fleeDir * fleeDistance;
            npc.SetAgentDestination(fleePos);
            timer = pathCalculationTime;
        }
    }
}
