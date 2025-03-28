#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using Exerussus._1Extensions.ExtensionEditor.Editor.Models;
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Extensions.ExtensionEditor.Editor
{
    public class ExerussusCenterEditor : EditorWindow
    {
        private static List<GitPackage> _packages = new List<GitPackage>();
        private Dictionary<string, bool> _foldoutStates = new Dictionary<string, bool>();
        public static int CenterUpdateVersion { get; private set; }
        
        [MenuItem("Tools/Exerussus/ExerussusCenter")]
        private static void OpenWindow()
        {
            InitPackages();
            GetWindow<ExerussusCenterEditor>("Exerussus Center");
        }

        private static void InitPackages()
        {
            Debug.Log("Updated packages");
            _packages = new()
            {
                new GitPackage("NuGet", "com.github-glitchenzo.nugetforunity", "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity"),
                new GitPackage("1Extensions", "com.exerussus.1extensions", "https://github.com/exerussus/1Extensions.git"),
                new GitPackage("1OrganizerUI", "com.exerussus.1organizer-ui", "https://github.com/exerussus/1OrganizerUI.git"),
                new GitPackage("EcsLite", "com.leopotam.ecslite", "https://github.com/Leopotam/ecslite.git"),
                new GitPackage("EcsLiteEditor", "com.leopotam.ecslite.unityeditor", "https://github.com/Leopotam/ecslite-unityeditor.git"),
                new GitPackage("1EasyEcs", "com.exerussus.1easyecs", "https://github.com/exerussus/1EasyEcs.git", "Easy Ecs"),
                new GitPackage("NetworkTools", "com.exerussus.easyecs.networktools", "https://github.com/exerussus/EasyEcsNetworkTools.git", "Easy Ecs"),
                new GitPackage("Movement", "com.exerussus.ecsmodule.movement", "https://github.com/exerussus/ecsmodule-movement.git", "Easy Ecs"),
                new GitPackage("SpaceHash", "com.exerussus.ecsmodule.spacehash", "https://github.com/exerussus/ecsmodule-spacehash.git", "Easy Ecs"),
                new GitPackage("Health", "com.exerussus.ecsmodule.health", "https://github.com/exerussus/ecsmodule-health.git", "Easy Ecs"),
            };
        }

        protected void OnGUI()
        {
            if (_packages.Count == 0) InitPackages();
        
            var groupedPackages = _packages.GroupBy(p => p.Group);

            foreach (var group in groupedPackages)
            {
                if (!_foldoutStates.ContainsKey(group.Key))
                {
                    _foldoutStates[group.Key] = true;
                }

                GUI.backgroundColor = Color.white;
                _foldoutStates[group.Key] = EditorGUILayout.Foldout(_foldoutStates[group.Key], group.Key, true, EditorStyles.foldoutHeader);
            
                if (_foldoutStates[group.Key])
                {
                    EditorGUI.indentLevel++;
                    foreach (var gitPackage in group)
                    {
                        GUI.backgroundColor = gitPackage.GetColor();
                        if (GUILayout.Button(gitPackage.GetInstallDescription()))
                        {
                            gitPackage.InstallPackage();
                        }
                    }
                    EditorGUI.indentLevel--;
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