public abstract class NpcState
{
    protected CharacterAI character;

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void OnStateRun();

    public NpcState(CharacterAI owner)
    {
        character = owner;
    }
}
