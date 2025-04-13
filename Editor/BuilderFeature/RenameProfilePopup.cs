#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Plugins.Exerussus._1Center.Editor.BuilderFeature
{
    public class RenameProfilePopup : OdinEditorWindow
    {
        private static Profile targetProfile;
        private static ProfilesSettings profilesSettings;
        private static BuilderEditor parentEditor;

        [Title("Новое имя профиля")]
        [LabelText("Имя")]
        [PropertySpace]
        [Required("Имя не должно быть пустым")]
        public string newName;

        public static void Open(ProfilesSettings profileSettings, Profile profile, BuilderEditor editor)
        {
            profilesSettings = profileSettings;
            targetProfile = profile;
            parentEditor = editor;

            var window = CreateInstance<RenameProfilePopup>();
            window.titleContent = new GUIContent("Переименовать профиль");
            window.newName = profile.name;
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(300, 100);
            window.ShowUtility();
        }

        [HorizontalGroup]
        [Button("ОК", ButtonSizes.Medium), GUIColor(0.3f, 0.8f, 0.3f)]
        private void Confirm()
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                Undo.RecordObject(profilesSettings, "Rename Profile");
                targetProfile.name = newName;
                EditorUtility.SetDirty(profilesSettings);

                parentEditor.RefreshSelectedProfileName();
                Close();
            }
        }

        [Button("Отмена", ButtonSizes.Medium), GUIColor(0.8f, 0.4f, 0.4f)]
        private void Cancel()
        {
            Close();
        }
    }
}
#endif