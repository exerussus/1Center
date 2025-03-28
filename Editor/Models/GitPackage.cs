using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Extensions.ExtensionEditor.Editor.Models
{
    [Serializable]
    public class GitPackage
    {
        public GitPackage(string name, string packageName, string url)
        {
            Name = name;
            PackageName = packageName;
            Url = url;
            UpdateInstalledState();
        }

        private static readonly Color OffColor = new Color(1, 0, 0, 0.5f);
        private static readonly Color OnColor = new Color(0, 1, 0, 0.5f);
        
        public string Name { get; private set; }
        public string PackageName { get; private set; }
        public string Url { get; private set; }
        public bool IsInstalled { get; private set; }

        public Color GetColor() => EditorPrefs.GetBool($"MyPackage.{PackageName}") ? OnColor : OffColor;
        public string GetInstallDescription() => $"Install {Name}"; // $"Uninstall {Name}"
        
        // [Button("GetInstallDescription"), GUIColor("GetColor")]
        public void InstallPackage()
        {
            PackageAutoInstaller.PackageName = PackageName;
            PackageAutoInstaller.PackageUrl = Url;
            PackageAutoInstaller.ManifestPath = PackageConstants.ManifestPath;
            
            PackageAutoInstaller.InstallNuGetForUnity();
        }
        
        public void UpdateInstalledState() => IsInstalled = EditorPrefs.GetBool($"MyPackage.{PackageName}");
    }
}