#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Extensions.ExtensionEditor.Editor
{
    public static class PackageAutoInstaller
    {
        public static string PackageName { get; set; }
        public static string PackageUrl { get; set; }
        public static string ManifestPath  { get; set; }
        
        public static void InstallNuGetForUnity()
        {
            if (string.IsNullOrEmpty(PackageName)) return;
            if (string.IsNullOrEmpty(PackageUrl)) return;
            if (string.IsNullOrEmpty(ManifestPath)) return;
            
            
            string manifestFullPath = Path.Combine(Application.dataPath, "../", ManifestPath);

            if (!File.Exists(manifestFullPath))
            {
                Debug.LogError($"Файл manifest.json не найден по пути: {manifestFullPath}");
                return;
            }

            string jsonText = File.ReadAllText(manifestFullPath);
            var packageSymbol = $"MyPackage.{PackageName}";
            
            // Проверяем, есть ли уже этот пакет
            if (jsonText.Contains($"\"{PackageName}\""))
            {
                Debug.Log($"Пакет {PackageName} уже установлен.");
                EditorPrefs.SetBool(packageSymbol, true);
                return;
            }
            else
            {
                EditorPrefs.SetBool(packageSymbol, false);
            }

            // Ищем место, где добавляются зависимости
            int dependenciesIndex = jsonText.IndexOf("\"dependencies\": {");
            if (dependenciesIndex == -1)
            {
                Debug.LogError("Не найдена секция \"dependencies\" в manifest.json.");
                return;
            }

            // Ищем место для добавления новой зависимости
            int insertIndex = jsonText.IndexOf('}', dependenciesIndex); // Найти конец блока
            string newDependency = $"\"{PackageName}\": \"{PackageUrl}\"";

            // Добавляем новую зависимость
            jsonText = jsonText.Insert(insertIndex, (insertIndex > dependenciesIndex + 15 ? ",\n    " : "\n    ") + newDependency);

            // Сохраняем изменения
            File.WriteAllText(manifestFullPath, jsonText);

            Debug.Log($"Добавлен {PackageName} в manifest.json. Перезагружаем UPM...");

            // Перезагружаем зависимости UPM
            EditorPrefs.SetBool(packageSymbol, true);
            ExerussusCenterEditor.UpVersion();
            AssetDatabase.Refresh();
            EditorApplication.ExecuteMenuItem("Assets/Refresh");
        }
    }
}

#endif