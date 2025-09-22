using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Feature
{
    public class OnClickIncomeCommand
    {
        private readonly FeatureManager _featureManager;

        public OnClickIncomeCommand(FeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        internal void Execute(ref int newPriceTag, ref byte incomeLevel)
        {
            int currentMoney = ScoreSignals.Instance.OnGetMoney?.Invoke()
                               ?? (ES3.KeyExists("Money") ? ES3.Load<int>("Money") : 0);

            int cost = (int)(Mathf.Pow(2, Mathf.Clamp(incomeLevel, 0, 10)) * 100);
            newPriceTag = currentMoney - cost;

            incomeLevel += 1;

            ScoreSignals.Instance.OnSendMoney?.Invoke(newPriceTag);
            UISignals.Instance.OnSetMoneyValue?.Invoke(newPriceTag);
            _featureManager.SaveFeatureData();

            UISignals.Instance.OnRefreshShopUI?.Invoke();
        }
    }
}