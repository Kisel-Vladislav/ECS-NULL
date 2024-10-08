using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.Component
{
    public struct AnimatorRef
    {
        public Animator animator;
    }
    public struct TransformRef
    {
        public Transform transform;
    }
    public struct Health
    {
        public int value;
    }
    public struct Idle : IEcsIgnoreInFilter
    {
    }
    public struct MoveInput
    {
        public Vector3 vector;
    }
    public struct TryAim : IEcsIgnoreInFilter { }
    public struct Aiming : IEcsIgnoreInFilter { }
    public struct AimFinished : IEcsIgnoreInFilter { }
    public struct LookAt
    {
        public Transform transform;
    }
}
