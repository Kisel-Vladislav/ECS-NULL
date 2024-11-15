using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "NewBuild", menuName = "Build System/Build")]
    public class Build : ScriptableObject
    {
        public GameObject prefab;
        public string buildName;
    }

}
