﻿using UnityEngine;

namespace CodeBase.ECS.Data
{
    public class SceneData : MonoBehaviour
    {
        public Transform PlayerSpawnPoint;
        public Camera MainCamera;
        public AgentView[] Enemy;
        public AgentView[] Ally;
    }
}
