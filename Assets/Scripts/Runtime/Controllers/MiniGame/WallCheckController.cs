using DG.Tweening;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers.MiniGame
{
    public class WallCheckController : MonoBehaviour
    {
        #region self variables

        #region serialized variables

        [SerializeField] private MiniGameManager manager;

        #endregion

        #region private variables

        private float _changesColor;
        private float _multiplier;

        private readonly string _wall = "Wall";

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_wall)) return;
            _multiplier += 0.1f;
            manager.SetMultiplier(_multiplier);
            Changecolor(other);
        }

        private void Changecolor(Collider other)
        {
            _changesColor = (0.036f + _changesColor) % 1;
            var otherGameObject = other.gameObject;
            otherGameObject.GetComponent<Renderer>().material.DOColor(Color.HSVToRGB(_changesColor, 1, 1), 0.1f);
            otherGameObject.transform.DOLocalMoveZ(-3, 0.1f);
        }

        public void OnReset()
        {
            _changesColor = 0;
            _multiplier = 0.90f;
        }
    }
}