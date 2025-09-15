using Runtime.Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.Collectables
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region self variables

        #region serialized variables

        [SerializeField] private MeshFilter meshFilter;

        #endregion

        #region private variables

        [ShowInInspector] private CollectableMeshData _data;

        #endregion

        #endregion


        private void OnEnable()
        {
            ActivateMeshVisuals();
        }

        internal void SetMeshData(CollectableMeshData dataMeshData)
        {
            _data = dataMeshData;
        }

        private void ActivateMeshVisuals()
        {
            meshFilter.mesh = _data.MeshList[0];
        }

        internal void UpgradeCollectableVisual(int value)
        {
            meshFilter.mesh = _data.MeshList[value];
        }
    }
}