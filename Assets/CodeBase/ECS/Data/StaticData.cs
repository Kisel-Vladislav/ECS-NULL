﻿using UnityEngine;

namespace CodeBase.ECS.Data
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public float PlayerSpeed;

        public float SmoothTime;
        public Vector3 FollowOffset; 
    }
}
