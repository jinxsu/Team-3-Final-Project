public class BoarVulnerable : State
{
    //States that have this as a parent are able to have their actions interrupted by being shot so that the boss will charge
    
    protected BoarBossStateController bsc;


    protected override void OnEnter()
    {
        //Establishing a variable that is frequently used here so that all the children automatically have it
        bsc = (BoarBossStateController)sc;
    }



}
