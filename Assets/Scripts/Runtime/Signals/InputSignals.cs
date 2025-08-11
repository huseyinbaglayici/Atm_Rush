using DefaultNamespace.Runtime.Keys;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction<bool> onChangeInputState = delegate { };
    }
}