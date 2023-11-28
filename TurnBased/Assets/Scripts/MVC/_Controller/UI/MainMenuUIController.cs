using System;
using Base;
using UnityEngine;

public class MainMenuUIController : IBaseController, IEnterController, IExitController
{
    private GameObject m_uiPrefab;
    private MainMenuView m_mainMenuView;
    private Transform m_uiRoot;
    private StateManager<MainGameState> m_stateManager;
    public MainMenuUIController(Transform uiRoot, StateManager<MainGameState> stateManager)
    {
        m_uiRoot = uiRoot;
        m_uiPrefab = Resources.Load<GameObject>(LocalConstants.MAIN_MENU);
        m_stateManager = stateManager;
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
        return () => m_stateManager.SwitchState(MainGameState.Game);
    }

    private Action QuitGameClickedHandler()
    {
        return () => Application.Quit();
    }
}
