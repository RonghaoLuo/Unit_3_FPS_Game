using UnityEngine;

public class CommandGiver : MouseClickStrategy
{
    [SerializeField] private CompanionNpc companion;

    public override void ExecuteStrategy()
    {
        companion.AddCommandToQueue(new GoToCommand(transform.position));
    }
}
