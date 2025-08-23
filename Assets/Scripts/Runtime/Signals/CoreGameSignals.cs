using System;
using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> OnChangeGameState;
        public UnityAction<byte> OnLevelInitialize = delegate { };
        public UnityAction OnClearActiveLevel = delegate { };
        public UnityAction OnLevelSuccessful = delegate { };
        public UnityAction OnLevelFailed = delegate { };
        public UnityAction OnNextLevel = delegate { };
        public UnityAction OnRestartLevel = delegate { };
        public UnityAction OnPlay = delegate { };
        public UnityAction OnReset = delegate { };
        public Func<byte> OnGetLevelID = () => 0;

        public UnityAction OnMiniGameEntered = delegate { };
        public UnityAction<GameObject> OnAtmTouched = delegate { };
        public UnityAction OnMiniGameStart = delegate { };

        public Func<byte> OnGetIncomeLevel = () => 0;
        public Func<byte> OnGetStackLevel = () => 0;
    }
}