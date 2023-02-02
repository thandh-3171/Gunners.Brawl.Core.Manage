using MEC;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace WeAreProStars.Core.Manage.UI.Template
{
    // <summary>
    // Hierychy : 
    // Dropdown
    //   -> ViewPort
    //      -> Content              // Sibling Index : Must 01
    //         -> Item
    //      -> Horizon  Bar         // Don't care. May delete.
    //      -> Vertical Bar         // Don't care. May delete
    ///     -> ContentPrefab        // Must UN-enable
    // </summary>
    public class DropdownTemplate : UIContentAbstract
    {
        #region Serialized.        
        #endregion

        #region Public vars.
        public List<UIItemAbstract> _items = new();
        #endregion

        #region Private vars.
        Dropdown dropdown;
        #endregion

        #region Mono
        protected override void OnEnable()
        {
            Debug.Log("abc");
        }

        protected override void Awake()
        {
            base.Awake();
            HandleInitialized = Timing.RunCoroutine(_Initialized());
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (GetComponent<DropdownPro>() == null) GenerateDropdownPro();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// I want to replace dropdown by dropdown-pro.
        /// </summary>
        [ContextMenu("GenerateDropdownPro")]
        private void GenerateDropdownPro()
        {
            Dropdown _dropdown = GetComponent<Dropdown>();
            if (_dropdown == null)
            {
                Debug.LogError("Valid error. Null dropdown.");
                return;
            }
            var stored = new DropdownProData(_dropdown);
            //try
            //{
            //    PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
            //}
            //catch { }
            EditorApplication.delayCall += () =>
            {
                DestroyImmediate(_dropdown);
                Dropdown tempDropdown = gameObject.AddComponent<DropdownPro>();
                stored.ApplyTo(tempDropdown);
            };
            //try
            //{
            //    if (PrefabUtility.IsPartOfAnyPrefab(this)) return;
            //    GameObject newRoot = PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject,
            //        AssetDatabase.GetAssetPath(gameObject),
            //        InteractionMode.UserAction, out bool success);
            //}
            //catch { }


        }

        /// <summary>
        /// Initializing process.
        /// </summary>
        /// <returns></returns>
        IEnumerator<float> _Initialized()
        {
            this.dropdown = GetComponent<DropdownPro>();
            if (dropdown == null)
            {
                Debug.LogWarning("Init error.Null dropdown-pro.");
                yield break;
            }
            initialized = true;
        }

        public override void AddQueueItem<T>(T data, int index = -1, bool autoActive = true, bool forceLived = false)
        {
            if (!Lived())
            {
                if (!forceLived)
                {
                    Debug.Log("Not enabled. Can't run.");
                    return;
                }
                else { }
                //TODO : Code to force game object become active here.
            }
            Timing.RunCoroutine(_AddQueueItem<T>(data, index, autoActive));
        }

        IEnumerator<float> _AddQueueItem<T>(T data, int index = -1, bool autoActive = true)
        {
            Dropdown.OptionData newOptionData = new();
            this.dropdown.AddOptions(new List<Dropdown.OptionData>() { newOptionData });
            //this.dropDown.

            GameObject newItem = _InstantiateItem();
            UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
            yield return Timing.WaitUntilTrue(() => iItem.Lived());
            _InitializeQueueItem<T>(data, newItem);
            _items.Add(iItem);
            if (autoActive && _items.Count == 1) iItem.ActivateQueue();
        }

        private GameObject _InstantiateItem()
        {
            //if (itemPrefab == null)
            //{
            //    Debug.Log("Item prefab null.");
            //    return null;
            //}
            //if (content == null)
            //{
            //    Debug.Log("Content null.");
            //    return null;
            //}
            //GameObject newItem = Instantiate(itemPrefab, content.transform);
            return null;
        }

        /// <summary>
        /// Call item's own asynchronous functions.
        /// </summary>        
        private void _InitializeQueueItem<T>(T data, GameObject newItem)
        {
            UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
            if (iItem != null)
            {
                iItem.Initialized();
                iItem.OnPostQueueAdded_SetupUI(data, newItem);
            }
        }

        public override GameObject AddReturnItem<T>(T data, int index = -1, bool autoActive = true)
        {
            throw new NotImplementedException();
        }

        public override void Activate(UIItemAbstract item)
        {
            throw new NotImplementedException();
        }

        public override void ClickItem(UIItemAbstract item)
        {
            throw new NotImplementedException();
        }

        public override void ClearSeletion()
        {
            throw new NotImplementedException();
        }

        public override void ResetContent()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region public methods

        #endregion
    }
}