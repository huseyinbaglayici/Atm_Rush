using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> OnInteractionAtm = delegate { };
        public UnityAction<GameObject> OnInteractionObstacle = delegate { };
        public UnityAction<GameObject> OnInteractionCollectable = delegate { };
        public UnityAction<Vector2> OnStackFollowPlayer = delegate { };
        public UnityAction OnInteractionConveyor = delegate { };
        public UnityAction OnUpdateType = delegate { };
    }
}