using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public class DropdownTemplate : MonoBehaviour
    {
        #region Serialized

        #endregion

        #region Private vars
        private Dropdown dropdownScript;
        [HideInInspector] public ScrollViewTemplate scrollView;
        private bool initialized = false;
        #endregion

        #region Mono
        private void Awake()
        {
            Initialized();
        }
        #endregion

        #region Private methods
        [ContextMenu("Initialized")]
        private void Initialized()
        {
            StartCoroutine(_Initialized());
        }
        IEnumerator _Initialized()
        {
            dropdownScript = GetComponent<Dropdown>();
            if (dropdownScript == null)
            {
                Debug.LogWarning("Init error.Null dropdown.");
                yield break;
            }
            scrollView = GetComponentInChildren<ScrollViewTemplate>();
            if (scrollView == null)
            {
                Debug.LogWarning("Init error. Null scroll view template.");
                yield break;
            }
            initialized = true;
        }
        #endregion

        #region public methods
        
        #endregion
    }
}