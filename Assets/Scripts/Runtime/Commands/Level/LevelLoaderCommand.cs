using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Managers;
using Runtime.Signals;
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
                //Level Olusturulmadan once bu command yazildigindan ArgumentException hatasina Null Check
                var path = $"Prefabs/LevelPrefabs/level {param}";
                var prefab = resourceReq.asset as GameObject;
                if (prefab == null)
                {
                    Debug.LogWarning($"Level prefab could not be loaded or found at path: {path}\n--> Returning NULL");
                    return;
                }


                var newLevel = Object.Instantiate(resourceReq.asset.GameObject(),
                    Vector3.zero, Quaternion.identity);
                if (newLevel != null) newLevel.transform.SetParent(_levelManager.levelHolder.transform);
                CameraSignals.Instance.OnSetCinemachineTarget?.Invoke(CameraTargetStates.Player);
            };
        }
    }
}