using Base;
using UnityEngine;

namespace Client
{
    public class GameManager : MonoBehaviour
    {
        public SceneStaticData SceneStaticData;
        public MainConfigurationData MainConfigurationData;
        private StateManager<MainGameState> m_mainGameStateManager;
        private IStateChangeModel<MainGameState> m_stateChangeModel;
        //Главный класс, запускающий игру
        private void Start()
        {
            m_stateChangeModel = new StateChangeModel<MainGameState>(MainGameState.MainMenu);
            InitializeStateManager();
        }

        private void Update()
        {
            m_mainGameStateManager.OnUpdateExecute();
        }

        private void OnDestroy()
        {
            m_mainGameStateManager.Dispose();
        }

        private void InitializeStateManager()
        {
            m_mainGameStateManager = new StateManager<MainGameState>(m_stateChangeModel);
            m_mainGameStateManager.States[MainGameState.Game] = new GameState(MainGameState.Game, m_stateChangeModel, MainConfigurationData, SceneStaticData);
            m_mainGameStateManager.States[MainGameState.MainMenu] = new MainMenu(MainGameState.MainMenu, m_stateChangeModel, SceneStaticData);
            m_mainGameStateManager.CurrentState = m_mainGameStateManager.States[MainGameState.MainMenu];
            m_mainGameStateManager.InitStateManager();

        }
    }
}
