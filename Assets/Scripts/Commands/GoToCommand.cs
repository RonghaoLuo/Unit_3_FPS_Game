using UnityEngine;

public class GoToCommand : Command
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
        characterTarget.GetAgent().SetDestination(goToDestination);
    }

    public override bool IsComplete()
    {
        return characterTarget.GetAgent().remainingDistance <= characterTarget.GetAgent().stoppingDistance;
    }
}
