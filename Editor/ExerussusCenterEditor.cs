#if UNITY_EDITOR

using System.Collections.Generic;
using Exerussus._1Extensions.ExtensionEditor.Editor.Models;

using UnityEditor;
using UnityEngine;

namespace Exerussus._1Extensions.ExtensionEditor.Editor
{
    public class ExerussusCenterEditor : EditorWindow
    {
        private static List<GitPackage> _packages = new List<GitPackage>();
        public static int CenterUpdateVersion { get; private set; }
        
        [MenuItem("Tools/Exerussus/ExerussusCenter")]
        private static void OpenWindow()
        {
            InitPackages();
            GetWindow<ExerussusCenterEditor>("Exerussus Center");
        }

        private static void InitPackages()
        {
            _packages = new()
            {
                new GitPackage("NuGet", "com.github-glitchenzo.nugetforunity", "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity"),
                new GitPackage("1Extensions", "com.exerussus.1extensions", "https://github.com/exerussus/1Extensions.git"),
                new GitPackage("EcsLite", "com.leopotam.ecslite", "https://github.com/Leopotam/ecslite.git"),
                new GitPackage("1EasyEcs", "com.exerussus.1easyecs", "https://github.com/exerussus/1EasyEcs.git"),
                new GitPackage("1Attributes", "com.exerussus.1attributes", "https://github.com/exerussus/1Attributes.git"),
                new GitPackage("1OrganizerUI", "com.exerussus.1organizer-ui", "https://github.com/exerussus/1OrganizerUI.git")
            };
        }
        
        protected void OnGUI()
        {
            if (_packages.Count == 0) InitPackages();
            
            foreach (var gitPackage in _packages)
            {
                GUI.backgroundColor = gitPackage.GetColor();
                if (GUILayout.Button(gitPackage.GetInstallDescription()))
                {
                    gitPackage.InstallPackage();
                }
            }
            
            GUI.backgroundColor = Color.white;
        }

        public static void UpVersion()
        {
            CenterUpdateVersion++;
            
            foreach (var gitPackage in _packages)
            {
                gitPackage.UpdateInstalledState();
            }
        }
        
    }
}

#endif