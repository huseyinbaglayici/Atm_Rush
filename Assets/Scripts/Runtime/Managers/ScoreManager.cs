using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector] private int _money;
        [ShowInInspector] private int _stackValueMultiplier;
        [ShowInInspector] private int _scoreCache = 0;
        [ShowInInspector] private int _atmScoreValue = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _money = GetMoneyValue();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.OnSendMoney += OnSendMoney;
            ScoreSignals.Instance.OnGetMoney += () => _money;
            ScoreSignals.Instance.OnSetScore += OnSetScore;
            ScoreSignals.Instance.OnSetAtmScore += OnSetAtmScore;
            CoreGameSignals.Instance.OnMiniGameStart +=
                () => ScoreSignals.Instance.OnSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.OnReset += OnReset;
            CoreGameSignals.Instance.OnLevelSuccessful += RefreshMoney;
            CoreGameSignals.Instance.OnLevelFailed += RefreshMoney;
            UISignals.Instance.OnClickIncome += OnSetValueMultiplier;
        }


        private void OnSendMoney(int value)
        {
            _money = value;
        }


        private void OnSetScore(int setScore)
        {
            _scoreCache = (setScore * _stackValueMultiplier) + _atmScoreValue;
            PlayerSignals.Instance.OnSetTotalScore?.Invoke(_scoreCache);
        }

        private void OnSetAtmScore(int atmValues)
        {
            _atmScoreValue += atmValues * _stackValueMultiplier;
            AtmSignals.Instance.OnSetSAtmScoreText?.Invoke(_atmScoreValue);
        }

        private void OnSetValueMultiplier()
        {
            _stackValueMultiplier = CoreGameSignals.Instance.OnGetIncomeLevel();
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.OnSendMoney -= OnSendMoney;
            ScoreSignals.Instance.OnGetMoney -= () => _money;
            ScoreSignals.Instance.OnSetScore -= OnSetScore;
            ScoreSignals.Instance.OnSetAtmScore -= OnSetAtmScore;
            CoreGameSignals.Instance.OnMiniGameStart -=
                () => ScoreSignals.Instance.OnSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.OnReset -= OnReset;
            CoreGameSignals.Instance.OnLevelSuccessful -= RefreshMoney;
            CoreGameSignals.Instance.OnLevelFailed -= RefreshMoney;
            UISignals.Instance.OnClickIncome -= OnSetValueMultiplier;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }


        private void Start()
        {
            OnSetValueMultiplier();
            RefreshMoney();
        }


        private int GetMoneyValue()
        {
            if (!ES3.FileExists()) return 0;
            return (int)(ES3.KeyExists("Money") ? ES3.Load<int>("Money") : 0);
        }

        private void RefreshMoney()
        {
            _money += (int)(_scoreCache * ScoreSignals.Instance.OnGetMultiplier());
            UISignals.Instance.OnSetMoneyValue?.Invoke(_money);
        }

        private void OnReset()
        {
            _scoreCache = 0;
            _atmScoreValue = 0;
        }
    }
}