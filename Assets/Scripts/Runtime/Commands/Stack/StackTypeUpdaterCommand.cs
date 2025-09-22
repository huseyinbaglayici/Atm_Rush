using System.Collections.Generic;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackTypeUpdaterCommand
    {
        private List<GameObject> _collectableStack;
        private int _totalListScore;

        public StackTypeUpdaterCommand(ref List<GameObject> collectableStack)
        {
            _collectableStack = collectableStack;
        }

        public void Execute()
        {
            _totalListScore = 0;
            foreach (var item in _collectableStack)
            {
                if (item == null) continue;
                var cm = item.GetComponent<CollectableManager>();
                if (cm != null)
                {
                    _totalListScore += cm.GetCurrentValue() + 1;
                }
            }

            ScoreSignals.Instance.OnSetScore?.Invoke(_totalListScore);
        }
    }
}