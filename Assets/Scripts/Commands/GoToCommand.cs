using UnityEngine;

public class GoToCommand : NpcCommand
{
    private Vector3 goToDestination;

    public GoToCommand(Vector3 destination)
    {
        goToDestination = destination;
    }

    public override void Cancel()
    {
        
    }

    public override void Execute()
    {
        targetingNpc.GetAgent().SetDestination(goToDestination);
    }

    public override bool IsComplete()
    {
        return targetingNpc.GetAgent().remainingDistance <= targetingNpc.GetAgent().stoppingDistance;
    }
}
