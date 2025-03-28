#if UNITY_EDITOR

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Exerussus._1Extensions.ExtensionEditor.Editor
{
    public class ExerussusCenterEditor : OdinEditorWindow
    {
        [MenuItem("Tools/Exerussus/ExerussusCenter")]
        private static void OpenWindow()
        {
            GetWindow<ExerussusCenterEditor>("Exerussus Center");
        }
        
        [FoldoutGroup("Base"), Button("Install NuGet")]
        public void InstallNuGet()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.NuGet;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.NuGet;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
        
        [FoldoutGroup("Base"), Button("Install 1Extensions")]
        public void Install1Extensions()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.OneExtensions;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.OneExtensions;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
        
        [FoldoutGroup("Base"), Button("Install EcsLite")]
        public void InstallEcsLite()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.EcsLite;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.EcsLite;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
        
        [FoldoutGroup("Base"), Button("Install 1Organizer-Ui")]
        public void InstallOneOrganizerUi()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.OneOrganizerUi;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.OneOrganizerUi;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
        
        [FoldoutGroup("EasyEcs"), Button("Install Core")]
        public void InstallEasyEcs()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.OneEasyEcs;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.OneEasyEcs;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
    }
}

#endif