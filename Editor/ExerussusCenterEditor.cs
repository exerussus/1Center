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
        
        [Button("Install NuGet")]
        public void InstallNuGet()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.NuGet;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.NuGet;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
        
        [Button("Install 1Extensions")]
        public void Install1Extensions()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.OneExtensions;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.OneExtensions;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
        
        [Button("Install EcsLite")]
        public void InstallEcsLite()
        {
            PackageAutoInstaller.PackageName = PackageConstants.Name.EcsLite;
            PackageAutoInstaller.PackageUrl = PackageConstants.Url.EcsLite;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
    }
}

#endif