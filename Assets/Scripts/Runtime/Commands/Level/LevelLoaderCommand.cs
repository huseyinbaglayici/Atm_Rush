using Runtime.Interfaces;
using Runtime.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Commands.Level
{
    public class LevelLoaderCommand : ICommand
    {
        private readonly LevelManager _levelManager;

        public LevelLoaderCommand(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public void Execute(byte param)
        {
            var resourceReq = Resources.LoadAsync<GameObject>($"Prefabs/LevelPrefabs/level {param}");
            resourceReq.completed += operation =>
            {
                var newLevel = Object.Instantiate(resourceReq.asset.GameObject(),
                    Vector3.zero, Quaternion.identity);
                if (newLevel != null) newLevel.transform.SetParent(_levelManager.levelHolder.transform);
                // CameraSignals.Instance.onSetCinemachineTarget?.Invoke(CameraTargetState.Player);
            };
        }
    }
}