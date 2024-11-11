using CodeBase.ECS.Component.Agent;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "AgentData", menuName = "StaticData/Agent")]
    public class AgentStaticData : ScriptableObject
    {
        public TeamType Team;

        public GameObject Prefab;

        [Range(1, 100)]
        public int Hp;
    }
}
