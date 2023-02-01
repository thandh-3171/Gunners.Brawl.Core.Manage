using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UI.Dropdown;

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
        private Dropdown dropDown;
        private ScrollView scrollView;
        #endregion

        #region Mono
        public override void Awake()
        {
            base.Awake();
            HandleInitialized = Timing.RunCoroutine(_Initialized());
        }
        #endregion

        #region Private methods        
        IEnumerator<float> _Initialized()
        {
            this.dropDown = GetComponent<Dropdown>();
            if (dropDown == null)
            {
                Debug.LogWarning("Init error.Null dropdown.");
                yield break;
            }
            this.scrollView = GetComponentInChildren<ScrollView>();
            if (scrollView == null)
            {
                Debug.LogWarning("Init error. Null scroll view.");
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
            OptionData newOptionData = new();
            this.dropDown.AddOptions(new List<OptionData>() { newOptionData });
            this.dropDown.


            GameObject newItem = _InstantiateItem();
            UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
            yield return Timing.WaitUntilTrue(() => iItem.Lived());
            _InitializeQueueItem<T>(data, newItem);
            _items.Add(iItem);
            if (autoActive && _items.Count == 1) iItem.ActivateQueue();
        }

        private GameObject _InstantiateItem()
        {
            if (itemPrefab == null)
            {
                Debug.Log("Item prefab null.");
                return null;
            }
            if (content == null)
            {
                Debug.Log("Content null.");
                return null;
            }
            GameObject newItem = Instantiate(itemPrefab, content.transform);
            return newItem;
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