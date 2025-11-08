public abstract class NpcState
{
    protected WanderingNpc npc;

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void OnStateRun();

    public NpcState(WanderingNpc owner)
    {
        npc = owner;
    }
}
