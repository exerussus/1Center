#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Plugins.Exerussus._1Center.Editor.BuilderFeature
{
    [Serializable]
    public class Profile
    {
        public string name;
        public List<SceneAsset> scenes;
        public List<string> symbols;
    }
}
#endif