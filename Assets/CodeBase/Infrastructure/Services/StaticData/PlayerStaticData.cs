using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        public GameObject Prefab;

        [Range(1, 100)]
        public int Hp;

        [Range(0.5f, 5)]
        public float MoveSpeed;
    }
}
