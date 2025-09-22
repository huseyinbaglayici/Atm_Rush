using System;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction OnSetIncomeLvlText = delegate { };
        public UnityAction OnSetStackLvlText = delegate { };
        public UnityAction<byte> OnSetNewLevelValue = delegate { };
        public UnityAction<int> OnSetMoneyValue = delegate { };
        public Func<int> OnGetMoneyValue = () => 0;
        public UnityAction OnRefreshShopUI = delegate { };

        public UnityAction OnClickIncome = delegate { };
        public UnityAction OnClickStack = delegate { };
    }
}