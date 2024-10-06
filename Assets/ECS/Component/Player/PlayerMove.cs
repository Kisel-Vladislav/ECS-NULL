using System;
using UnityEngine;
namespace CodeBase.ECS.PlayerComponent
{
    [Serializable]
    public struct PlayerMove
    {
        public CharacterController CharacterController;
        public float playerSpeed;
    }
}   
