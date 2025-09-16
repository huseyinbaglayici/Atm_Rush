using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.Stack;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region public variables

        public StackJumperCommand StackJumperCommand { get; private set; }

        public StackTypeUpdaterCommand StackTypeUpdaterCommand { get; private set; }

        public ItemAdderOnStackCommand AdderOnStackCommand { get; private set; }

        public bool LastCheck { get; set; }

        #endregion

        #region serialized variables

        [SerializeField] private GameObject money;

        #endregion

        #region private variables

        private StackData _data;
        private List<GameObject> _collectableStack = new List<GameObject>();

        private StackmMoverCommand _stackMoverCommand;
        private ItemRemoverOnStackCommand _itemRemoverOnStackOnStackCommand;
        private StackAnimatorCommand _stackAnimatorCommand;
        private StackInteractionWithConveyorCommand _stackInteractionWithConveyorCommand;
        private StackInitializerCommand _stackInitializerCommand;

        private readonly string _stackDataPath = "Data/CD_Stack";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetStackData();
            Init();
        }

        private void Init()
        {
            _stackMoverCommand = new StackmMoverCommand(ref _data);
            AdderOnStackCommand = new ItemAdderOnStackCommand(this, ref _collectableStack, ref _data);
            _itemRemoverOnStackOnStackCommand = new ItemRemoverOnStackCommand(this, ref _collectableStack);
            _stackAnimatorCommand = new StackAnimatorCommand(_data, ref _collectableStack);
            StackJumperCommand = new StackJumperCommand(_data, ref _collectableStack);
            _stackInteractionWithConveyorCommand = new StackInteractionWithConveyorCommand(this, ref _collectableStack);
            StackTypeUpdaterCommand = new StackTypeUpdaterCommand(ref _collectableStack);
            _stackInitializerCommand = new StackInitializerCommand(this, ref money);
        }

        private StackData GetStackData()
        {
            return Resources.Load<CD_Stack>(_stackDataPath).Data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.OnInteractionCollectable += OnInteractionWithCollectable;
            StackSignals.Instance.OnInteractionObstacle += _itemRemoverOnStackOnStackCommand.Execute;
            StackSignals.Instance.OnInteractionAtm += OnInteractionWithAtm;
            StackSignals.Instance.OnInteractionConveyor += _stackInteractionWithConveyorCommand.Execute;
            StackSignals.Instance.OnStackFollowPlayer += OnStackMove;
            StackSignals.Instance.OnUpdateType += StackTypeUpdaterCommand.Execute;
            CoreGameSignals.Instance.OnPlay += OnPlay;
            CoreGameSignals.Instance.OnReset += OnReset;
        }

        private void OnStackMove(Vector2 direction)
        {
            transform.position = new Vector3(0, gameObject.transform.position.y, direction.y + 2f);
            if (gameObject.transform.childCount > 0)
            {
                _stackMoverCommand.Execute(direction.x, _collectableStack);
            }
        }

        private void OnInteractionWithAtm(GameObject collectableGameObject)
        {
            ScoreSignals.Instance.OnSetAtmScore?.Invoke((int)collectableGameObject.GetComponent<CollectableManager>()
                .GetCurrentValue() + 1);
            if (!LastCheck)
            {
                _itemRemoverOnStackOnStackCommand.Execute(collectableGameObject);
            }
            else
            {
                collectableGameObject.SetActive(false);
            }
        }

        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
            DOTween.Complete(StackJumperCommand);
            AdderOnStackCommand.Execute(collectableGameObject);
            StartCoroutine(_stackAnimatorCommand.Execute());
            StackTypeUpdaterCommand.Execute();
        }

        private void OnPlay()
        {
            _stackInitializerCommand.Execute();
        }


        private void UnSubscribeEvents()
        {
            StackSignals.Instance.OnInteractionCollectable -= OnInteractionWithCollectable;
            StackSignals.Instance.OnInteractionObstacle -= _itemRemoverOnStackOnStackCommand.Execute;
            StackSignals.Instance.OnInteractionAtm -= OnInteractionWithAtm;
            StackSignals.Instance.OnInteractionConveyor -= _stackInteractionWithConveyorCommand.Execute;
            StackSignals.Instance.OnStackFollowPlayer -= OnStackMove;
            StackSignals.Instance.OnUpdateType -= StackTypeUpdaterCommand.Execute;
            CoreGameSignals.Instance.OnPlay -= OnPlay;
            CoreGameSignals.Instance.OnReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }


        private void OnReset()
        {
            LastCheck = false;
            _collectableStack.Clear();
            _collectableStack.TrimExcess();
        }
    }
}