public class BigBossTwoVulnerable : State
{

    protected BigBossTwoStateController bsc;

    protected override void OnEnter()
    {
        bsc = (BigBossTwoStateController)sc;
    }
}
