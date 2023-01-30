using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    [Serializable]
    public abstract class UIItemAbstract : MonoBehaviour
    {
        #region events
        public delegate void OnSetSelected();
        public delegate void OnSetUnSelected();
        #endregion

        #region public vars        
        /// <summary>
        /// Set select event.
        /// </summary>
        public OnSetSelected onSetSelected;
        /// <summary>
        /// Set un-select event.
        /// </summary>
        public OnSetUnSelected onSetUnSelected;
        /// <summary>
        /// Readying state.
        /// </summary>
        //[HideInInspector]
        public bool initialized = false;
        /// <summary>
        /// State of being selected.
        /// </summary>
        public bool selected
        {
            get { return _selected; }
            set { HandleSetSelected(value); }
        }
        /// <summary>
        /// Serialize for inspecting.
        /// </summary>
        [SerializeField] private bool _selected = false;
        #endregion

        #region private methods
        /// <summary>
        /// Handle how to set selected state.
        /// Value be check first, set after.
        /// </summary>
        /// <param name="value"></param>
        public virtual void HandleSetSelected(bool value)
        {
            //If same value, do nothing.
            if (_selected == value) return;
            if (!_selected && value) onSetSelected?.Invoke();
            else if (_selected && !value) onSetUnSelected?.Invoke();
            _selected = value;
        }
        #endregion

        #region abstract methods
        /// <summary>
        /// I want every inheritances must override Awake()
        /// Example : Button needs to set up its own container (parent).
        /// Don't use this ever. Far too unstable while using with coroutine.
        /// GameObject could be hidden unexpectedly.
        /// Todo : Use Initilize.
        /// </summary>
        /// public abstract void Awake();
        
        /// <summary>
        /// Data (type T) is the class contain infomation.
        /// Entity is the button or the UI item.
        /// </summary>
        public abstract IEnumerator<float> Initialized();

        /// <summary>
        /// Call to activate. Not the whole click.
        /// </summary>
        public abstract IEnumerator<float> Activate();

        /// <summary>
        /// Call to perform task of clicking.
        /// </summary>
        public abstract IEnumerator<float> OnClick();

        /// <summary>
        /// Data (type T) is the class contain infomation.
        /// Entity is the button or the UI item.
        /// </summary>
        public abstract IEnumerator<float> OnPostAdded_SetupUI<T>(T data, GameObject entity);
        #endregion
    }
}
