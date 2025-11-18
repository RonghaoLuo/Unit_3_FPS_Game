using UnityEngine;
using UnityEngine.AI;

public class WanderNpcState : NpcState
{
    private float wanderingRadius = 20f;

    public WanderNpcState(StateNpc owner) : base(owner)
    {

    }

    public override void OnStateEnter()
    {
        npc.SetAgentDestination(GetRandomNavmeshLocation(wanderingRadius));
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

    Vector3 GetRandomNavmeshLocation(float radius)
    {
        // Pick a random direction & distance
        Vector2 randomDir = Random.insideUnitCircle * radius;
        Vector3 randomPos = new Vector3(randomDir.x, 0f, randomDir.y) + npc.transform.position;

        // Snap onto NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return npc.transform.position; // fallback
    }
}
