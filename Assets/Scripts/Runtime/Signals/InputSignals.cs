using Runtime.Extensions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction OnFirstTimeTouchTaken = delegate { };
        public UnityAction OnInputTaken = delegate { };
        public UnityAction<HorizontalInputParams> OnInputDragged = delegate { };
        public UnityAction OnInputReleased = delegate { };
        public UnityAction<bool> OnChangeInputState = delegate { };
    }
}