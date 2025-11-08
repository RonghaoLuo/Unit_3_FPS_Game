using UnityEngine;

public class WanderNpcState : NpcState
{
    public WanderNpcState(CharacterAIChasePlayer owner) : base(owner)
    {

    }

    public override void OnStateEnter()
    {
        character.SetAgentDestination(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateRun()
    {
        if (!character.IsMoving())
        {
            character.ChangeState(new IdleNpcState(character));
        }
    }
}
