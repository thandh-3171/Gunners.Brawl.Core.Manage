using System;
using System.Collections.Generic;
using UnityEngine;
using WeAreProStars.Core.Manage.UI.Template;

namespace WeAreProStars.Core.Manage
{
    [Serializable]
    public abstract class UIContentAbstract : MonoBehaviour
    {
        #region events
        // Event right at the moment select an item.
        //public delegate void OnSelectItem(List<UIItemAbstract> items);
        //public OnSelectItem onSelectItem;

        // Event after selecting an item.
        //public delegate void OnClickItem(List<UIItemAbstract> items);
        //public OnClickItem onClickItem;

        // Event of clearing current selection.
        //public delegate void OnClearSelection(List<UIItemAbstract> lastItems);
        //public OnClearSelection onClearSelection;
        #endregion

        #region public vars
        /// <summary>
        /// State of being initialized.
        /// </summary>
        public bool initialized
        {
            get { return _initialized; }
            set { HandleSetInitialized(value); }
        }
        /// <summary>
        /// Serialize for inspecting.
        /// </summary>
        [SerializeField] private bool _initialized = false;
        #endregion

        #region private methods
        /// <summary>
        /// Handle how to set initialized state.
        /// Value be check first, set after.
        /// </summary>
        /// <param name="value"></param>
        public virtual void HandleSetInitialized(bool value)
        {
            //If same value, do nothing.
            if (_initialized == value) return;
            //if (!_initialized && value) onSetSelected?.Invoke();
            //else if (_initialized && !value) onSetUnSelected?.Invoke();
            _initialized = value;
        }
        #endregion

        #region methods
        /// <summary>
        /// Frequently, I set up prefabs for scroll rect, view port and content gameobject here.
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// To add an item.
        /// </summary>
        public abstract GameObject AddItem<T>(T data, int index = -1, bool autoActive = true);

        /// <summary>
        /// To activate an item.
        /// </summary>
        public abstract void Activate(UIItemAbstract item);

        /// <summary>
        /// To click an item.
        /// </summary>
        public abstract void ClickItem(UIItemAbstract item);

        /// <summary>
        /// To clear all selection.
        /// </summary>
        public abstract void ClearSeletion();

        /// <summary>
        /// To reset the content.
        /// </summary>
        [ContextMenu("ResetScroll")]
        public abstract void ResetContent();
        #endregion
    }
}
