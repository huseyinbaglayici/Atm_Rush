using Runtime.Commands.Level;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [Header("Holder")] [SerializeField] internal GameObject levelHolder;
        [Space] [SerializeField] private int totalLevelCount;

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private LevelDestroyerCommand _levelDestroyer;

        private byte _currentLevel;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _levelLoader = new LevelLoaderCommand(this);
            _levelDestroyer = new LevelDestroyerCommand(this);
        }


        private void OnEnable()
        {
            SubscribeEvents();

            _currentLevel = GetLevelID();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke(_currentLevel);
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnLevelInitialize += _levelLoader.Execute;
            CoreGameSignals.Instance.OnClearActiveLevel += _levelDestroyer.Execute;
            CoreGameSignals.Instance.OnGetLevelID += GetLevelID;
            CoreGameSignals.Instance.OnNextLevel += OnNextLevel;
            CoreGameSignals.Instance.OnRestartLevel += OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }

        private void UnsubscribeEvent()
        {
            CoreGameSignals.Instance.OnLevelInitialize -= _levelLoader.Execute;
            CoreGameSignals.Instance.OnClearActiveLevel -= _levelDestroyer.Execute;
            CoreGameSignals.Instance.OnGetLevelID -= GetLevelID;
            CoreGameSignals.Instance.OnNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.OnRestartLevel -= OnRestartLevel;
        }

        private byte GetLevelID()
        {
            return _currentLevel;
        }

        private void OnNextLevel()
        {
            _currentLevel++;
            SaveSignals.Instance.OnSaveGameData?.Invoke();
            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            byte levelIndex = (byte)(_currentLevel % totalLevelCount);
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke(levelIndex);
        }

        private void OnRestartLevel()
        {
            SaveSignals.Instance.OnSaveGameData?.Invoke();
            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke(GetLevelID());
        }
    }
}