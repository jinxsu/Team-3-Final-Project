public class SmallBossTwoVulnerable : State
{
    protected SmallBossTwoStateController bsc;

    protected override void OnEnter()
    {
        bsc = (SmallBossTwoStateController)sc;
    }
}
