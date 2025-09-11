using System;
using System.Collections.Generic;
using Runtime.Commands.Stack;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
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

        public ItemAdderOnStackCommand ItemAdderOnStackCommand { get; private set; }

        public bool LastCheck { get; set; }

        #endregion

        #region private variables

        [ShowInInspector] private StackData _data;
        private List<GameObject> _collectableStack = new List<GameObject>();
        private StackmMoverCommand _stackmMoverCommand;
        private ItemRemoverCommand _itemRemoverCommand;
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
            throw new NotImplementedException();
        }

        private StackData GetStackData()
        {
            return Resources.Load<CD_Stack>(_stackDataPath).Data;
        }
    }
}