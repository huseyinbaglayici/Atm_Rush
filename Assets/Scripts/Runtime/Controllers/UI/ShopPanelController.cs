using System;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class ShopPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI incomeLvlText;
        [SerializeField] private Button incomeLvlButton;
        [SerializeField] private TextMeshProUGUI incomeValue;
        [SerializeField] private Button stackLvlButton;
        [SerializeField] private TextMeshProUGUI stackLvlText;
        [SerializeField] private TextMeshProUGUI stackValue;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.OnSetIncomeLvlText += OnSetIncomeLvLText;
            UISignals.Instance.OnSetStackLvlText += OnSetStackLvLText;
        }

        private void OnSetStackLvLText()
        {
            stackLvlText.text = "Stack lvl\n" + CoreGameSignals.Instance.OnGetStackLevel();
            stackValue.text = (Mathf.Pow(2, Mathf.Clamp(CoreGameSignals.Instance.OnGetStackLevel(), 0, 10)) * 100)
                .ToString();
        }

        private void OnSetIncomeLvLText()
        {
            incomeLvlText.text = "Income lvl\n" + CoreGameSignals.Instance.OnGetIncomeLevel();
            incomeValue.text = (Mathf.Pow(2, Mathf.Clamp(CoreGameSignals.Instance.OnGetIncomeLevel(), 0, 10)) * 100)
                .ToString();
        }

        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnSetIncomeLvlText -= OnSetIncomeLvLText;
            UISignals.Instance.OnSetStackLvlText -= OnSetStackLvLText;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            SyncShopUi();
        }

        private void SyncShopUi()
        {
            OnSetIncomeLvLText();
            OnSetStackLvLText();
            ChangesIncomeInteractable();
            ChangesStackInteractable();
        }

        private void ChangesIncomeInteractable()
        {
            if (int.Parse(UISignals.Instance.OnGetMoneyValue?.Invoke().ToString()!) < int.Parse(incomeValue.text) ||
                CoreGameSignals.Instance.OnGetIncomeLevel() >= 30)
            {
                incomeLvlButton.interactable = false;
            }

            else
            {
                incomeLvlButton.interactable = true;
            }
        }

        private void ChangesStackInteractable()
        {
            if (int.Parse(UISignals.Instance.OnGetMoneyValue?.Invoke().ToString()!) < int.Parse(stackValue.text) ||
                CoreGameSignals.Instance.OnGetStackLevel() >= 15)
            {
                stackLvlButton.interactable = false;
            }
            else
            {
                stackLvlButton.interactable = true;
            }
        }
    }
}