using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackAnimatorCommand
    {
        private StackData _stackData;
        private List<GameObject> _collectableStack;

        public StackAnimatorCommand(StackData data, ref List<GameObject> collectableStack)
        {
            _stackData = data;
            _collectableStack = collectableStack;
        }

        public IEnumerator Execute()
        {
            for (int i = 0; i <= _collectableStack.Count - 1; i++)
            {
                int index = (_collectableStack.Count - 1) - i;
                _collectableStack[index].transform
                    .DOScale(
                        new Vector3(_stackData.StackScaleValue, _stackData.StackScaleValue, _stackData.StackScaleValue),
                        _stackData.StackAnimDuration).SetEase(Ease.Flash);
                _collectableStack[index].transform.DOScale(Vector3.one, _stackData.StackAnimDuration)
                    .SetDelay(_stackData.StackAnimDuration).SetEase(Ease.Flash);
                yield return new WaitForSeconds(_stackData.StackAnimDuration / 3);
            }
        }
    }
}