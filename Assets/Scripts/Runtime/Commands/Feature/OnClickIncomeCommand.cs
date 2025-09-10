using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Feature
{
    public class OnClickIncomeCommand
    {
        private readonly FeatureManager _featureManager;
        private int _newPriceTag;
        private byte _incomeLevel;

        public OnClickIncomeCommand(FeatureManager featureManager, ref int newPriceTag, ref byte incomeLevel)
        {
            _featureManager = featureManager;
            _newPriceTag = newPriceTag;
            _incomeLevel = incomeLevel;
        }

        internal void Execute()
        {
            _newPriceTag = (int)(CoreGameSignals.Instance.OnGetIncomeLevel() -
                                 ((Mathf.Pow(2, Mathf.Clamp(_incomeLevel, 0, 10)) * 100)));
            _incomeLevel += 1;
            ScoreSignals.Instance.OnSendMoney?.Invoke((int)_newPriceTag);
            UISignals.Instance.OnSetMoneyValue?.Invoke((int)_newPriceTag);
            _featureManager.SaveFeatureData();
        }
    }
}