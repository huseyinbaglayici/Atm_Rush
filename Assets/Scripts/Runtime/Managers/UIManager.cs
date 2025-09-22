using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();

            OpenStartPanel();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.OnLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.OnLevelSuccessful += OnLevelSuccesful;
            CoreGameSignals.Instance.OnReset += OnReset;
        }

        private void OpenStartPanel()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Start, 0);
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Level, 1);
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Shop, 2);
        }

        private void OnLevelInitialize(byte levelValue)
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Start, 0);
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Level, 1);
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Shop, 2);
            UISignals.Instance.OnSetNewLevelValue?.Invoke(levelValue);
        }

        public void OnPlay()
        {
            CoreGameSignals.Instance.OnPlay?.Invoke();
            CoreUISignals.Instance.OnClosePanel?.Invoke(0);
            CoreUISignals.Instance.OnClosePanel?.Invoke(2);
            CameraSignals.Instance.OnChangeCameraState?.Invoke(CameraStates.Follow);
        }

        private void OnOpenWinPanel()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Win, 2);
        }

        private void OnOpenFailPanel()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
        }

        public void OnNextLevel()
        {
            CoreGameSignals.Instance.OnNextLevel?.Invoke();
            CoreGameSignals.Instance.OnReset?.Invoke();
        }

        public void OnRestartLevel()
        {
            CoreGameSignals.Instance.OnRestartLevel?.Invoke();
            CameraSignals.Instance.OnChangeCameraState?.Invoke(CameraStates.Initial);
            // CoreGameSignals.Instance.OnReset?.Invoke();
        }

        private void OnLevelFailed()
        {
            OnOpenFailPanel();
        }

        private void OnLevelSuccesful()
        {
            OnOpenWinPanel();
        }

        public void OnIncomeUpdate()
        {
            UISignals.Instance.OnClickIncome?.Invoke();
            UISignals.Instance.OnSetIncomeLvlText?.Invoke();
        }

        public void OnStackUpdate()
        {
            UISignals.Instance.OnClickStack?.Invoke();
            UISignals.Instance.OnSetStackLvlText?.Invoke();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.OnLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.OnLevelSuccessful -= OnLevelSuccesful;
            CoreGameSignals.Instance.OnReset -= OnReset;
        }


        private void OnReset()
        {
            // CoreUISignals.Instance.OnCloseAllPanels?.Invoke();
        }
    }
}