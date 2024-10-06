using System;
using UnityEngine;
namespace CodeBase.ECS.PlayerComponent
{
    [Serializable]
    public struct PlayerC
    {
        public CharacterController CharacterController;
        public float playerSpeed;
    }
}   
