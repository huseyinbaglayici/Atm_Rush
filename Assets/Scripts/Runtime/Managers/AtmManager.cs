using System;
using DG.Tweening;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region self variables

        #region serialized variables

        [SerializeField] private DOTweenAnimation _doTweenAnimation;
        [SerializeField] private TextMeshPro atmText;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _doTweenAnimation = GetComponentInChildren<DOTweenAnimation>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnAtmTouched += OnAtmTouched;
            AtmSignals.Instance.OnSetSAtmScoreText += OnSetAtmScoreText;
        }


        private void OnAtmTouched(GameObject touchedAtmObject)
        {
            if (touchedAtmObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                _doTweenAnimation.DOPlay();
            }
        }

        private void OnSetAtmScoreText(int value)
        {
            atmText.text = value.ToString();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnAtmTouched -= OnAtmTouched;
            AtmSignals.Instance.OnSetSAtmScoreText -= OnSetAtmScoreText;
        }
    }
}