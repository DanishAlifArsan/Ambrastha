using UnityEngine.UI;

public class PocongChangeState : IState
{
    private Text stageName;
    private int currentPhase;

    public PocongChangeState(Text stageName, int currentPhase)
    {
        this.stageName = stageName;
        this.currentPhase = currentPhase;
    }

    public void EnterState()
    {
        switch(currentPhase) {
            case 1:
                stageName.text = "[Manuk Culi]";
                break;
            case 2:
                stageName.text = "[Gelu]";
                break;
        }
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
