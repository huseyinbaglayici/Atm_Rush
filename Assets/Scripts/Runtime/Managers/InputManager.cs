using System.Collections.Generic;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region self variables

        #region private variables

        private float _positionValuesX;
        private bool _isTouching;

        // ref types
        private float _currentVelocity;
        private Vector2? _mousePosition;
        private Vector3 _moveVector;

        [Header("Data")] private InputData _data;
        [ShowInInspector] private bool _isFirstTimeTouchTaken, _isAvaibleForTouch;

        private const string PlayerDataPath = "Data/CD_Input";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>(PlayerDataPath).Data;


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnReset += OnReset;
            CoreGameSignals.Instance.OnPlay += OnPlay;
            InputSignals.Instance.OnChangeInputState += OnChangeInputState;
        }

        private void OnPlay()
        {
            _isAvaibleForTouch = true;
        }

        private void OnChangeInputState(bool state)
        {
            _isAvaibleForTouch = state;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnReset -= OnReset;
            CoreGameSignals.Instance.OnPlay -= OnPlay;
            InputSignals.Instance.OnChangeInputState -= OnChangeInputState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (!_isAvaibleForTouch) return;

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.OnInputReleased?.Invoke();
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())

                if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
                {
                    _isTouching = true;
                    InputSignals.Instance.OnInputTaken?.Invoke();
                    if (!_isFirstTimeTouchTaken)
                    {
                        _isFirstTimeTouchTaken = true;
                        InputSignals.Instance.OnFirstTimeTouchTaken?.Invoke();
                    }

                    _mousePosition = Input.mousePosition;
                }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

                        if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                            _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                            _moveVector.x = _data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;

                        else
                            _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0, ref _currentVelocity,
                                _data.HorizontalInputClampStopValue);

                        _mousePosition = Input.mousePosition;
                        
                        InputSignals.Instance.OnInputDragged?.Invoke(new HorizontalInputParams()
                        {
                          HorizontalInputValue  =  _moveVector.x,
                          HorizontalInputClampSides = _data.HorizontalInputClampNegativeSides,
                        });
                    }
                }
            }
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }


        private void OnReset()
        {
            _isTouching = false;
            _isAvaibleForTouch = false;
        }
    }
}