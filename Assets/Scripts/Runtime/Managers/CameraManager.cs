using System;
using Cinemachine;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables

        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        [ShowInInspector] private float3 _initalPosition;

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _initalPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CameraSignals.Instance.OnSetCinemachineTarget += OnSetCinemachineTarget;
            CameraSignals.Instance.OnChangeCameraState += OnChangeCameraState;
        }

        private void OnSetCinemachineTarget(CameraTargetStates state)
        {
            switch (state)
            {
                case CameraTargetStates.Player:
                {
                    // var playerManager = FindObjectOfType<PlayerManager>.transform;
                    // stateDrivenCamera.follow = playerManager;
                }
                    break;
                case CameraTargetStates.FakePlayer:
                {
                    stateDrivenCamera.Follow = null;
                    // var fakePlayer = FindObjectOfType<WallCheckController>().transform.parent.transform;
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnChangeCameraState(CameraStates state)
        {
            animator.SetTrigger(state.ToString());
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CameraSignals.Instance.OnSetCinemachineTarget -= OnSetCinemachineTarget;
            CameraSignals.Instance.OnChangeCameraState -= OnChangeCameraState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnReset()
        {
            CameraSignals.Instance.OnChangeCameraState?.Invoke(CameraStates.Initial);
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
            transform.position = _initalPosition;
        }

        #endregion
    }
}