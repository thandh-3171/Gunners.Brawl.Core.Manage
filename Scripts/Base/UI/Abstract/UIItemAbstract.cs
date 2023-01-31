using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    [Serializable]
    public abstract class UIItemAbstract : UIEntityTemplate
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
        /// I don't want ui item to be un-enable, hidden of not-active-self ever.
        /// I will warn of this. Every time.
        /// </summary>
        /// public abstract void OnDisable();

        /// <summary>
        /// Data (type T) is the class contain infomation.
        /// Entity is the button or the UI item.
        /// </summary>
        /// public abstract IEnumerator<float> Initialized();
        public abstract void Initialized();

        /// <summary>
        /// Call to activate. 
        /// Not the whole click.
        /// Require initialized.
        /// </summary>        
        public abstract void Activate();

        /// <summary>
        /// Wait for initialized.
        /// </summary>        
        public abstract void ActivateQueue();

        /// <summary>
        /// Wait for initialized.
        /// </summary>        
        public abstract IEnumerator<float> _ActivateQueue();

        /// <summary>
        /// Call to perform task of clicking.
        /// </summary>
        //public abstract IEnumerator<float> OnClick();
        public abstract void OnClick();

        /// <summary>
        /// Data (type T) is the class contain infomation.
        /// Entity is the button or the UI item.
        /// </summary>        
        public abstract void OnPostAdded_SetupUI<T>(T data, GameObject entity);

        /// <summary>
        /// Wait for initialized.
        /// </summary>        
        public abstract void OnPostQueueAdded_SetupUI<T>(T data, GameObject entity);

        /// <summary>
        /// Wait for initialized.
        /// </summary>        
        public abstract IEnumerator<float> _OnPostQueueAdded_SetupUI<T>(T data, GameObject entity);        
        #endregion
    }
}
