using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.UI
{
    public class LevelEditorHud : MonoBehaviour
    {
        public BuildPanelContentController BuildPanelContentController;

        private void Start()
        {
            BuildPanelContentController.Fill(BuildGroupType.Build);
        }
    }
}
