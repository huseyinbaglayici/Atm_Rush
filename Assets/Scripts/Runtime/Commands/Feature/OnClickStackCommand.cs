using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Feature
{
    public class OnClickStackCommand
    {
        private readonly FeatureManager _featureManager;

        public OnClickStackCommand(FeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        internal void Execute(ref int newPriceTag, ref byte stackLevel)
        {
            int currentMoney = ScoreSignals.Instance.OnGetMoney?.Invoke() ??
                               (ES3.KeyExists("Money") ? ES3.Load<int>("Money") : 0);

            int cost = (int)(Mathf.Pow(2, Mathf.Clamp(stackLevel, 0, 10)) * 100);
            newPriceTag = currentMoney - cost;
            stackLevel += 1;
            ScoreSignals.Instance.OnSendMoney?.Invoke((int)newPriceTag);
            UISignals.Instance.OnSetMoneyValue?.Invoke((int)newPriceTag);
            _featureManager.SaveFeatureData();

            UISignals.Instance.OnRefreshShopUI?.Invoke();
        }
    }
}