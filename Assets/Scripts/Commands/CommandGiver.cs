using UnityEngine;

public class CommandGiver : MouseClickStrategy
{
    [SerializeField] private CharacterCompanion companion;

    public override void ExecuteStrategy()
    {
        companion.AddCommandToQueue(new GoToCommand(transform.position));
    }
}
