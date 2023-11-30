using UnityEngine;

public class MainMenu : ControllersDependentState<MainGameState>
{
    private IStateChangeModel<MainGameState> m_stateChangeModel;
    private Transform m_uiRoot;
    public MainMenu(MainGameState key, IStateChangeModel<MainGameState> stateChangeModel, SceneStaticData sceneStaticData) : base(key, stateChangeModel)
    {
        m_stateChangeModel = stateChangeModel;
        m_uiRoot = sceneStaticData.UIRoot;
    }

    public override void EnterState()
    {
        ControllersManager.AddController(new MainMenuUIController(LocalConstants.MAIN_MENU, m_uiRoot, m_stateChangeModel));
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
