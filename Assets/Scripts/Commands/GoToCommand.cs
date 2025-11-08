using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GoToCommand : NpcCommand
{
    private Vector3 destination;

    public GoToCommand(Vector3 destination)
    {
        this.destination = destination;
    }

    public override void Cancel()
    {
        targetingNpc.StopAllCoroutines(); // or a smarter coroutine manager
        targetingNpc.GetAgent().ResetPath();
    }

    public override void Execute()
    {
        var agent = targetingNpc.GetAgent();
        agent.SetDestination(destination);
        targetingNpc.StartCoroutine(WaitUntilArrived(agent));
    }

    private IEnumerator WaitUntilArrived(NavMeshAgent agent)
    {
        // Wait until path is ready
        while (agent.pathPending)
            yield return null;

        // Wait until agent is close enough
        while (agent.remainingDistance > agent.stoppingDistance)
            yield return null;

        OnCommandComplete?.Invoke();
    }

    //public override bool IsComplete()
    //{
    //    return targetingNpc.GetAgent().remainingDistance <= targetingNpc.GetAgent().stoppingDistance;
    //}
}
