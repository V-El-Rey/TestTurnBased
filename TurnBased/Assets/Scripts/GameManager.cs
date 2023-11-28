using Base;
using UnityEngine;

namespace Client
{
    public class GameManager : MonoBehaviour
    {
        public Transform UIRoot;
        public MainConfigurationData MainConfigurationData;
        private StateManager<MainGameState> m_mainGameStateManager;
        //Главный класс, запускающий игру
        public void Awake()
        {
            InitializeStateManager();
        }

        private void Start()
        {
        }

        private void Update()
        {
            m_mainGameStateManager.OnUpdateExecute();
        }

        private void OnDestroy()
        {
        }

        private void InitializeStateManager()
        {
            m_mainGameStateManager = new StateManager<MainGameState>();
            // States MainGameState.Loading] = new 
            // States MainGameState.MainMenu] = 
            m_mainGameStateManager.States[MainGameState.Game] = new GameState(MainGameState.Game, m_mainGameStateManager, MainConfigurationData);
            m_mainGameStateManager.States[MainGameState.MainMenu] = new MainMenu(MainGameState.MainMenu, m_mainGameStateManager, UIRoot);
            m_mainGameStateManager.CurrentState = m_mainGameStateManager.States[MainGameState.MainMenu];
            m_mainGameStateManager.InitStateManager();

        }
    }
}
