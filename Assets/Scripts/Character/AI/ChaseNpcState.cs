using UnityEngine;

public class ChaseNpcState : NpcState
{
    public Transform targetToChase;

    public ChaseNpcState(CharacterAIChasePlayer owner) : base(owner)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Player Detected. Chasing...");
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateRun()
    {
        character.SetAgentDestination(targetToChase.position);
    }
}
