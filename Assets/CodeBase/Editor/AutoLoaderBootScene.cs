using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    public class AutoLoaderBootScene
    {
        private const string BootstrapScenePath = "Assets/Scenes/Bootstrap.unity";
        private const string TestScenePath = "Assets/Scenes/Test.unity"; // Путь к сцене Test

        [InitializeOnLoadMethod]
        public static void Load()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            if (activeScene.path == TestScenePath)
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(TestScenePath);
            }
            else
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootstrapScenePath);
            }
        }
    }
}