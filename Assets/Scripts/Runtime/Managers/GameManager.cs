using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Self Variables

        #region Public Variables

        public GameStates States;

        #endregion

        #endregion

        protected override void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        }

        private void OnChangeGameState(GameStates newState)
        {
            States = newState;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}