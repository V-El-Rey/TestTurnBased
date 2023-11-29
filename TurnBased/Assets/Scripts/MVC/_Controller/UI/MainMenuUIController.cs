using System;
using UnityEngine;

public class MainMenuUIController : IBaseController, IEnterController, IExitController
{
    private GameObject m_uiPrefab;
    private MainMenuView m_mainMenuView;
    private Transform m_uiRoot;
    private IStateChangeModel<MainGameState> m_stateChangeModel;
    public MainMenuUIController(Transform uiRoot, IStateChangeModel<MainGameState> stateChangeModel)
    {
        m_uiRoot = uiRoot;
        m_uiPrefab = Resources.Load<GameObject>(LocalConstants.MAIN_MENU);
        m_stateChangeModel = stateChangeModel;
    }

        public void OnEnterExecute()
    {
        var obj = GameObject.Instantiate(m_uiPrefab, m_uiRoot);
        m_mainMenuView = obj.GetComponent<MainMenuView>();
        m_mainMenuView.startGame.clicked += StartGameClickedHandler();
        m_mainMenuView.quitGame.clicked += QuitGameClickedHandler();
    }

    public void OnExitExecute()
    {
        m_mainMenuView.startGame.clicked -= StartGameClickedHandler();
        m_mainMenuView.quitGame.clicked -= QuitGameClickedHandler();
        GameObject.Destroy(m_mainMenuView.gameObject);
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
