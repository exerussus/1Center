
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Plugins.Exerussus._1Center.Editor.BuilderFeature
{
    public static class BuildProcess
    {
        public static void Run(Profile profile, ProfilesSettings settings)
        {
            var log = $"{profile.name}\n\n{string.Join("\n", profile.symbols)}\n\n";
            var isFirst = true;
            
            foreach (var sceneAsset in profile.scenes)
            {
                var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                if (isFirst)
                {
                    isFirst = false;
                    EditorSceneManager.OpenScene(scenePath);
                }
                log += sceneAsset.name + "\n";
                log += scenePath + "\n\n";
            }
            
            Debug.Log(log);
        }
    }
}