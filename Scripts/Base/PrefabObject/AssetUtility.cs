#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace WeAreProStars.Core.Manage
{
    public static class AssetUtility
    {
        #region editor methods
#if UNITY_EDITOR
        public static GameObject FindAssetExact(string assetName, string filter = "")
        {
            var assets = AssetDatabase.FindAssets(assetName + " " + filter);
            if (assets.Length == 0) return null;
            else if (assets.Length > 1)
            {
                Debug.LogWarning("More than one " + assetName);
                return AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assets[0]), typeof(GameObject)) as GameObject;
            }
            else return AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assets[0]), typeof(GameObject)) as GameObject;
        }
#endif
        #endregion
    }
}
