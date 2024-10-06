using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.Component
{
    public struct AnimatorRef
    {
        public Animator animator;
    }
    public struct Health
    {
        public int value;
    }
    public struct Idle : IEcsIgnoreInFilter
    {
    }
}   
