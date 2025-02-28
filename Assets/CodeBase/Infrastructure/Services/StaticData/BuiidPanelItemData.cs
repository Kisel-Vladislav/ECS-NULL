using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "BuiidPanelItemData", menuName = "StaticData/BuildPanel/ItemData")]
    public class BuildPanelItemData : ScriptableObject
    {
        public BuildGroupType BuildGroupType;
        public BuildTypeId BuildTypeId;
        public Sprite Sprite;
    }
}
