using Cinemachine;
using UnityEngine;

namespace Runtime.Extensions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LookCinemachineAxis : CinemachineExtension 
    {
        [Tooltip("Lock the camera's X position to this value")]
        public float mXPosition = 0f;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = mXPosition;
                state.RawPosition = pos;
            }
        }
    }
}