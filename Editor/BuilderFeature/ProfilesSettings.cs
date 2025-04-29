#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace Exerussus._1Center.Editor.BuilderFeature
{
    public class ProfilesSettings : ScriptableObject
    {
        public List<Profile> profiles;
    }
}
#endif