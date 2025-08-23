using DG.Tweening;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region serialized variables

        [SerializeField] private Rigidbody managerRigidBody;

        #endregion

        #region private variables

        private readonly string _obstacle = "Obstacle";
        private readonly string _atm = "ATM";
        private readonly string _collectable = "Collectable";
        private readonly string _conveyor = "Conveyor";

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_obstacle))
            {
                managerRigidBody.transform.DOMoveZ(managerRigidBody.transform.position.z - 10f, 1f)
                    .SetEase(Ease.OutBack);
                return;
            }

            if (other.CompareTag(_atm))
            {
                CoreGameSignals.Instance.OnAtmTouched?.Invoke(other.gameObject);
                return;
            }

            if (other.CompareTag(_collectable))
            {
                other.tag = "Collected";
                // StackSignals.Instance.OnInteractionCollectable?.Invoke(other.transform.parent.gameObject);
                return;
            }

            if (other.CompareTag(_conveyor))
            {
                CoreGameSignals.Instance.OnMiniGameEntered?.Invoke();
                DOVirtual.DelayedCall(1.5f,
                    () => CameraSignals.Instance.OnChangeCameraState?.Invoke(CameraStates.MiniGame));
                DOVirtual.DelayedCall(2.5f,
                    () => CameraSignals.Instance.OnSetCinemachineTarget?.Invoke(CameraTargetStates.FakePlayer));
                return;
            }
        }
    }
}