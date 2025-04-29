#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Center.Editor.BuilderFeature
{
    [InitializeOnLoad]
    public static class ConfigCreator
    {
        public static readonly string FolderPath = "Assets/Configs/ExerussusCenter/Editor";
        public static readonly string AssetPath = $"{FolderPath}/ProfilesSettings.asset";
        
        static ConfigCreator()
        {
            EditorApplication.delayCall += CreateProfilesSettingsIfMissing;
        }

        private static void CreateProfilesSettingsIfMissing()
        {
            var existingAsset = AssetDatabase.LoadAssetAtPath<ProfilesSettings>(AssetPath);
            if (existingAsset != null) return;
            
            CreateFolderIfNotExists("Assets", "Configs");
            CreateFolderIfNotExists("Assets/Configs", "ExerussusCenter");
            CreateFolderIfNotExists("Assets/Configs/ExerussusCenter", "Editor");

            var asset = ScriptableObject.CreateInstance<ProfilesSettings>();
            AssetDatabase.CreateAsset(asset, AssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("ProfilesSettings автоматически создан по пути: " + AssetPath);
        }

        private static void CreateFolderIfNotExists(string parent, string folderName)
        {
            string fullPath = $"{parent}/{folderName}";
            if (!AssetDatabase.IsValidFolder(fullPath))
            {
                AssetDatabase.CreateFolder(parent, folderName);
            }
        }
    }
}
#endif