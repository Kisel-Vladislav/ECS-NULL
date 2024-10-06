using System;
using UnityEngine;
namespace CodeBase.ECS.PlayerComponent
{
    [Serializable]
    public struct PlayerC
    {
        public Animator playerAnimator;
        public Transform playerTransform;
        public CharacterController CharacterController;
        public float playerSpeed;
    }
}   
