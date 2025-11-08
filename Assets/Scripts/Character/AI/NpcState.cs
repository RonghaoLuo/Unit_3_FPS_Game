public abstract class NpcState
{
    protected CharacterAIChasePlayer character;

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void OnStateRun();

    public NpcState(CharacterAIChasePlayer owner)
    {
        character = owner;
    }
}
