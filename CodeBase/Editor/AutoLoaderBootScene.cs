using UnityEditor;
using UnityEditor.SceneManagement;

namespace CodeBase.Editor
{
    public class AutoLoaderBootScene
    {
        private const string BootstrapScenePath = "Assets/Scenes/Bootstrap.unity";

        [InitializeOnLoadMethod]
        public static void Load()
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootstrapScenePath);
        }
    }
}