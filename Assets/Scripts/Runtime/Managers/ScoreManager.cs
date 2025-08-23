using System;
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
            Init();
        }

        private void Init()
        {
            _money = GetMoneyValue();
        }

        private int GetMoneyValue()
        {
            if (!ES3.FileExists()) return 0;
            return (int)(ES3.KeyExists("Money") ? ES3.Load<int>("Money") : 0);
        }
    }
}