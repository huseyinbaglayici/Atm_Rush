using System;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class SaveManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.OnSaveGameData += SaveData;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.OnSaveGameData -= SaveData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SaveData()
        {
            Debug.LogWarning(ScoreSignals.Instance.OnGetMoney());
            OnSaveGame(
                new SaveGameDataParams()
                {
                    Money = ScoreSignals.Instance.OnGetMoney(),
                    Level = CoreGameSignals.Instance.OnGetLevelID(),
                    IncomeLevel = CoreGameSignals.Instance.OnGetIncomeLevel(),
                    StackLevel = CoreGameSignals.Instance.OnGetStackLevel()
                }
            );
        }

        private void OnSaveGame(SaveGameDataParams saveDataParams)
        {
            ES3.Save("Level", saveDataParams.Level);
            ES3.Save("Money", saveDataParams.Money);
            ES3.Save("IncomeLevel", saveDataParams.IncomeLevel);
            ES3.Save("StackLevel", saveDataParams.StackLevel);
        }
    }
}