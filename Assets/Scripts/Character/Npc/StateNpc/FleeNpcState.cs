using UnityEngine;
using UnityEngine.AI;

public class FleeNpcState : ChaseNpcState
{
    protected float fleeDistance = 20f;

    public FleeNpcState(WanderingNpc owner) : base(owner)
    {
    }

    public override void OnStateRun()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            npc.SetAgentDestination(GetBestFleePointFromPlayer(target.transform, fleeDistance));

            timer = pathCalculationTime;
        }
    }

    Vector3 GetBestFleePointFromPlayer(Transform player, float fleeDistance, float angleStep = 30f)
    {
        Vector3 bestPos = npc.transform.position;
        float bestPlayerDist = 0f;

        for (float angle = 0; angle < 360f; angle += angleStep)
        {
            // Base flee direction (away from player)
            Vector3 baseDir = (npc.transform.position - player.position).normalized;

            // Rotate base direction around NPC
            Vector3 dir = Quaternion.Euler(0, angle, 0) * baseDir;

            Vector3 rawTargetPos = npc.transform.position + dir * fleeDistance;

            if (NavMesh.SamplePosition(rawTargetPos, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas))
            {
                float distToPlayer = Vector3.Distance(hit.position, player.position);

                if (distToPlayer > bestPlayerDist)
                {
                    bestPlayerDist = distToPlayer;
                    bestPos = hit.position;
                }
            }
        }

        return bestPos;
    }

}
