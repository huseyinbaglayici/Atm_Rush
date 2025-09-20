using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region self variables

        #region private variables

        private readonly string _level = "Level";
        private readonly string _money = "Money";
        private readonly string _incomeLevel = "IncomeLevel";
        private readonly string _stackLevel = "StackLevel";

        #endregion

        #endregion

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
            Debug.LogWarning("Saving game data ---> " + saveDataParams.Level);
            ES3.Save(_level, saveDataParams.Level);
            ES3.Save(_money, saveDataParams.Money);
            ES3.Save(_incomeLevel, saveDataParams.IncomeLevel);
            ES3.Save(_stackLevel, saveDataParams.StackLevel);
        }
    }
}