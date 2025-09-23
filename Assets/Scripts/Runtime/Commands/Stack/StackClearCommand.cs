using System.Collections.Generic;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackClearCommand
    {
        private List<GameObject> _collectableStack;

        public StackClearCommand(ref List<GameObject> collectableStack)
        {
            _collectableStack = collectableStack;
        }

        internal void Execute()
        {
            if (_collectableStack == null || _collectableStack.Count == 0) return;
            foreach (var item in _collectableStack)
            {
                item.transform.SetParent(null);
                item.SetActive(false);
            }
            _collectableStack.Clear();
            _collectableStack.TrimExcess();

            Debug.LogWarning("Stack cleared");
        }
    }
}