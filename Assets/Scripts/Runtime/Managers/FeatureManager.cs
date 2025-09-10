using Runtime.Commands.Feature;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class FeatureManager : MonoBehaviour
    {
        #region self variables

        #region public variables

        public FeatureManager()
        {
            _onClickIncomeCommand = new OnClickIncomeCommand(this, ref _newPriceTag, ref _incomeLevel);
            _onClickStackCommand = new OnClickStackCommand(this, ref _newPriceTag, ref _stackLevel);
        }

        #endregion

        #region private variables

        [ShowInInspector] private byte _incomeLevel = 1;
        [ShowInInspector] private byte _stackLevel = 1;
        [ShowInInspector] private int _newPriceTag;

        private readonly OnClickIncomeCommand _onClickIncomeCommand;
        private readonly OnClickStackCommand _onClickStackCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _incomeLevel = LoadIncomeData();
            _stackLevel = LoadStackData();
        }

        private void OnEnable()
        {
            Subscription();
        }

        private void Subscription()
        {
            UISignals.Instance.OnClickIncome += _onClickIncomeCommand.Execute;
            UISignals.Instance.OnClickStack += _onClickStackCommand.Execute;
            CoreGameSignals.Instance.OnGetIncomeLevel += OnGetIncomeLevel;
            CoreGameSignals.Instance.OnGetStackLevel += OnGetStackLevel;
        }

        private byte OnGetIncomeLevel() => _incomeLevel;
        private byte OnGetStackLevel() => _stackLevel;

        private void UnSubscription()
        {
            UISignals.Instance.OnClickIncome -= _onClickIncomeCommand.Execute;
            UISignals.Instance.OnClickStack -= _onClickStackCommand.Execute;
            CoreGameSignals.Instance.OnGetIncomeLevel -= OnGetIncomeLevel;
            CoreGameSignals.Instance.OnGetStackLevel -= OnGetStackLevel;
        }

        private void OnDisable()
        {
            UnSubscription();
        }

        private byte LoadIncomeData()
        {
            if (!ES3.FileExists()) return 1;
            return (byte)(ES3.KeyExists("IncomeLevel") ? ES3.Load<int>("IncomeLevel") : 1);
        }

        private byte LoadStackData()
        {
            if (!ES3.FileExists()) return 1;
            return (byte)(ES3.KeyExists("StackLevel") ? ES3.Load<int>("StackLevel") : 1);
        }

        internal void SaveFeatureData()
        {
            SaveSignals.Instance.OnSaveGameData?.Invoke();
        }
    }
}