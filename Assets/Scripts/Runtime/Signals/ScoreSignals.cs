using System;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> OnSetScore = delegate { };
        public UnityAction<int> OnSetAtmScore = delegate { };
        public UnityAction<int> OnSendFinalScore = delegate { };
        public UnityAction<int> OnSendMoney = delegate { };
        public Func<int> OnGetMoney = delegate { return 0; };

        public Func<float> OnGetMultiplier = delegate { return 0; };
    }
}