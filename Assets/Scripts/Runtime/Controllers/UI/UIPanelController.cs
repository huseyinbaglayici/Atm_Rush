using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region self variables

        #region serialized variables

        [SerializeField] private List<GameObject> layers = new List<GameObject>();

        #endregion

        #endregion


        #region event subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.OnOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.OnClosePanel += OnClosePanel;
            CoreUISignals.Instance.OnCloseAllPanels += OnCloseAllPanels;
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.OnOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.OnClosePanel -= OnClosePanel;
            CoreUISignals.Instance.OnCloseAllPanels -= OnCloseAllPanels;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        [Button("OpenPanel")]
        private void OnOpenPanel(UIPanelTypes panel, int layerValue)
        {
            CoreUISignals.Instance.OnClosePanel?.Invoke(layerValue);
            Instantiate(Resources.Load<GameObject>($"Screens/{panel}Panel"), layers[layerValue].transform);
        }

        [Button("ClosePanel")]
        private void OnClosePanel(int layerValue)
        {
            if (layers[layerValue].transform.childCount > 0)
                for (int i = 0; i < layers[layerValue].transform.childCount; i++)
                {
                    Destroy(layers[layerValue].transform.GetChild(i).gameObject);
                }
        }

        [Button("CloseAllPanels")]
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                for (int i = 0; i < layer.transform.childCount; i++)
                {
                    Destroy(layer.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}