using Base;
using UnityEngine;

public class MainMenu : ControllersDependentState<MainGameState>
{
    public MainMenu(MainGameState key, StateManager<MainGameState> stateManager, Transform uiRoot) : base(key, stateManager)
    {
        ControllersManager.AddController(new MainMenuUIController(uiRoot, stateManager));
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
