using System;
using UnityEngine;

public class MainMenuUIController : BaseUIController<MainMenuView>
{
    private IStateChangeModel<MainGameState> m_stateChangeModel;
    public MainMenuUIController(string screenId, Transform uiRoot, IStateChangeModel<MainGameState> stateChangeModel) : base(screenId, uiRoot)
    {
        m_stateChangeModel = stateChangeModel;
    }

    public override void OnEnterExecute()
    {
        base.OnEnterExecute();
        ((MainMenuView)m_uiView).StartGame.clicked += StartGameClickedHandler();
        ((MainMenuView)m_uiView).QuitGame.clicked += QuitGameClickedHandler();
    }

    public override void OnExitExecute()
    {
        ((MainMenuView)m_uiView).StartGame.clicked -= StartGameClickedHandler();
        ((MainMenuView)m_uiView).QuitGame.clicked -= QuitGameClickedHandler();
        base.OnExitExecute();
    }
    private Action StartGameClickedHandler()
    {
        return () => m_stateChangeModel.onStateChangeRequested?.Invoke(MainGameState.Game);
    }

    private Action QuitGameClickedHandler()
    {
        return () => Application.Quit();
    }
}
