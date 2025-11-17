public abstract class NpcState
{
    protected StateNpc npc;

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void OnStateRun();

    public NpcState(StateNpc owner)
    {
        npc = owner;
    }
}
