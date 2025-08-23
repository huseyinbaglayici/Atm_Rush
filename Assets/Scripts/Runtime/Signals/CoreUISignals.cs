using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoSingleton<CoreUISignals>
    {
        public UnityAction<UIPanelTypes, int> OnOpenPanel = delegate { };
        public UnityAction<int>  OnClosePanel = delegate { };
        public UnityAction  OnCloseAllPanel = delegate { };
    }
}