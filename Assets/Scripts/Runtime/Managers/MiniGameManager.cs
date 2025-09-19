using System.Collections;
using DG.Tweening;
using Runtime.Controllers.MiniGame;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region self variables

        #region serialized variables

        [SerializeField] private GameObject wallObject;
        [SerializeField] private GameObject fakeMoneyObject;
        [SerializeField] private Transform fakePlayer;
        [SerializeField] private Transform minigameTransform;
        [SerializeField] private Material mat;

        [SerializeField] private short wallCount, fakeMoneyCount;

        [SerializeField] private WallCheckController wallChecker;

        #endregion

        #region private variables

        private int _score;
        private float _multiplier;
        private Vector3 _initialPos;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.OnSendFinalScore += OnSendScore;
            ScoreSignals.Instance.OnGetMultiplier += OnGetMultiplier;
            CoreGameSignals.Instance.OnMiniGameStart += OnMiniGameStart;
            CoreGameSignals.Instance.OnReset += OnReset;
        }

        private void OnMiniGameStart()
        {
            fakePlayer.gameObject.SetActive(true);
            StartCoroutine(GoUp());
        }

        private IEnumerator GoUp()
        {
            yield return new WaitForSeconds(1f);
            if (_score == 0)
            {
                CoreGameSignals.Instance.OnLevelFailed?.Invoke();
            }
            else
            {
                fakePlayer.DOLocalMoveY(Mathf.Clamp(_score, 0, 900), 2.7f).SetEase(Ease.Flash).SetDelay(1f);
                yield return new WaitForSeconds(4.5f);
                CoreGameSignals.Instance.OnLevelSuccessful?.Invoke();
            }
        }


        internal void SetMultiplier(float multiplierValue)
        {
            _multiplier = multiplierValue;
        }

        private float OnGetMultiplier()
        {
            return _multiplier;
        }

        private void OnSendScore(int scoreValue)
        {
            _score = scoreValue;
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.OnSendFinalScore -= OnSendScore;
            ScoreSignals.Instance.OnGetMultiplier -= OnGetMultiplier;
            CoreGameSignals.Instance.OnMiniGameStart -= OnMiniGameStart;
            CoreGameSignals.Instance.OnReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            SpawnWallObjects();
            SpawnFakeMoneyObjects();
            Init();
        }

        private void Init()
        {
            _initialPos = minigameTransform.localPosition;
            Debug.LogWarning("initial pos -->" + _initialPos);
        }

        private void SpawnWallObjects()
        {
            for (int i = 0; i < wallCount; i++)
            {
                var ob = Instantiate(wallObject, transform);
                ob.transform.localPosition = new Vector3(0, i * 10, 0);
                ob.transform.GetChild(0).GetComponent<TextMeshPro>().text = "x" + ((i / 10f) + 1);
            }
        }

        private void SpawnFakeMoneyObjects()
        {
            for (int i = 0; i < fakeMoneyCount; i++)
            {
                var ob = Instantiate(fakeMoneyObject, fakePlayer);
                ob.transform.localPosition = new Vector3(0, -i * 1.58f, -7);
            }
        }

        //TO-DO FAKE PLAYER 560 KORDINATINA GIDIYOR! FIXLE >
        private void ResetWalls()
        {
            for (int i = 1; i < wallCount; i++)
            {
                var wall = transform.GetChild(i);
                transform.GetChild(i).GetComponent<Renderer>().material = mat;
                wall.localPosition = new Vector3(0, i * 10, 0);
            }
        }

        private void OnReset()
        {
            StopAllCoroutines();
            DOTween.KillAll();
            ResetWalls();
            ResetFakePlayer();
        }

        private void ResetFakePlayer()
        {
            fakePlayer.gameObject.SetActive(false);
            fakePlayer.localPosition = _initialPos;
            wallChecker.OnReset();
        }
    }
}