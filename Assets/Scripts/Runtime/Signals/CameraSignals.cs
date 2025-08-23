using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction<CameraStates> OnChangeCameraState = delegate { };
        public UnityAction<CameraTargetStates> OnSetCinemachineTarget = delegate { };
    }
}