using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public float CollectableOffsetInStack = 1;
        [Range(0.1f, 0.8f)] public float LerpSpeed = 0.25f;
        [Range(0, 0.2f)] public float StackAnimDuration = 0.12f;
        [Range(1f, 3f)] public float StackScaleValue = 1f;
        [Range(1, 10f)] public float JumpForce = 7f;
        public float JumpItemsClampX = 5f;

    }
}