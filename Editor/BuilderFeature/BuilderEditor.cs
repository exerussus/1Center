#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Center.Editor.BuilderFeature
{
    public class BuilderEditor : OdinEditorWindow
    {
        [MenuItem("Tools/Exerussus/Builder")]
        private static void OpenWindow()
        {
            GetWindow<BuilderEditor>().titleContent = new GUIContent("Builder");
        }

        private ProfilesSettings profilesSettings;

        [ValueDropdown("GetProfileNames")]
        [OnValueChanged("OnProfileSelected", includeChildren: true)]
        public string selectedProfileName;

        public BuildTarget buildTarget;
        
        [ShowInInspector, LabelText("Сцены профиля")]
        [OnValueChanged(nameof(SyncToProfile))]
        private List<SceneAsset> profileScenes;

        [ShowInInspector, LabelText("Символы компиляции")]
        [OnValueChanged(nameof(SyncToProfile))]
        private List<string> profileSymbols;

        private Profile selectedProfile;

        protected override void OnEnable()
        {
            base.OnEnable();
            LoadSettings();
        }

        private void LoadSettings()
        {
            profilesSettings = AssetDatabase.LoadAssetAtPath<ProfilesSettings>(ConfigCreator.AssetPath);

            if (profilesSettings != null && profilesSettings.profiles != null && profilesSettings.profiles.Count > 0)
            {
                selectedProfile = profilesSettings.profiles[0];
                selectedProfileName = selectedProfile.name;
                SyncFromProfile();
            }
        }

        public void RefreshSelectedProfileName()
        {
            selectedProfileName = selectedProfile?.name;
        }
        
        private IEnumerable<string> GetProfileNames()
        {
            if (profilesSettings == null || profilesSettings.profiles == null)
                return new List<string>();
            return profilesSettings.profiles.Select(p => p.name);
        }

        private void OnProfileSelected()
        {
            selectedProfile = profilesSettings.profiles.FirstOrDefault(p => p.name == selectedProfileName);
            SyncFromProfile();
        }

        private void SyncFromProfile()
        {
            if (selectedProfile == null) return;

            profileScenes = new List<SceneAsset>(selectedProfile.scenes ?? new List<SceneAsset>());
            profileSymbols = new List<string>(selectedProfile.symbols ?? new List<string>());
        }

        private void SyncToProfile()
        {
            if (selectedProfile == null) return;

            selectedProfile.scenes = profileScenes;
            selectedProfile.symbols = profileSymbols;

            EditorUtility.SetDirty(profilesSettings);
            AssetDatabase.SaveAssets();
        }

        [Button(ButtonSizes.Large), GUIColor(0.3f, 0.8f, 0.3f)]
        private void Build()
        {
            if (selectedProfile == null) profilesSettings = AssetDatabase.LoadAssetAtPath<ProfilesSettings>(ConfigCreator.AssetPath);
            if (selectedProfile != null) BuildProcess.Run(selectedProfile, profilesSettings);
        }

        [Button("Добавить новый профиль"), GUIColor(0.4f, 0.6f, 1f)]
        private void AddNewProfile()
        {
            if (profilesSettings == null) return;

            var newProfile = new Profile
            {
                name = "NewProfile",
                scenes = new List<SceneAsset>(),
                symbols = new List<string>()
            };

            profilesSettings.profiles.Add(newProfile);
            selectedProfile = newProfile;
            selectedProfileName = newProfile.name;
            SyncFromProfile();

            EditorUtility.SetDirty(profilesSettings);
            AssetDatabase.SaveAssets();
        }
        
        [Button("Переименовать профиль"), GUIColor(1f, 0.8f, 0.4f)]
        private void RenameProfile()
        {
            if (selectedProfile == null) return;
            RenameProfilePopup.Open(profilesSettings, selectedProfile, this);
        }
        
        [Button("Скопировать все текущие символы"), GUIColor(0.7f, 0.5f, 0.7f)]
        private void GetAllCurrentSymbols()
        {
            if (selectedProfile != null) return;
            
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
            string currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        }

        [Button("Удалить текущий профиль"), GUIColor(1f, 0.4f, 0.4f)]
        private void DeleteCurrentProfile()
        {
            if (profilesSettings == null || selectedProfile == null) return;

            if (EditorUtility.DisplayDialog("Удаление профиля", $"Удалить профиль '{selectedProfile.name}'?", "Да", "Отмена"))
            {
                profilesSettings.profiles.Remove(selectedProfile);
                selectedProfile = null;
                selectedProfileName = null;

                EditorUtility.SetDirty(profilesSettings);
                AssetDatabase.SaveAssets();
            }
        }
    }
}

#endif