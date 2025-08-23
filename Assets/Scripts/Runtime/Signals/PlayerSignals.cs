using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<PlayerAnimationStates> OnChangePlayerAnimationState = delegate { };
        public UnityAction<bool> OnPlayConditionChanged = delegate { };
        public UnityAction<bool> OnMoveConditionChanged = delegate { };
        public UnityAction<int> OnSetTotalScore = delegate { };
    }
}