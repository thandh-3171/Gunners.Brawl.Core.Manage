using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class BaseItemButton : MonoBehaviour
        , UIItemInterface
    {
        #region events
        public delegate void OnSetSelected();
        public OnSetSelected onSetSelected;

        public delegate void OnSetUnSelected();
        public OnSetUnSelected onSetUnSelected;
        #endregion

        #region vars
        /// <summary>
        /// State of being selected.
        /// </summary>
        public bool selected
        {
            get { return _selected; }
            set { HandleSetSelected(value); }
        }
        private bool _selected = false;

        /// <summary>
        /// Item-Interface of this class. Should be one.
        /// </summary>
        public UIItemInterface itemInterface;
        #endregion

        #region mono
        /// <summary>
        /// Must be called first line. And must should be overrided.
        /// </summary>
        public virtual void Awake()
        {
            itemInterface = GetComponent<UIItemInterface>();
        }
        #endregion

        #region methods
        /// <summary>
        /// Handle how to set selected state.
        /// Value be check first, set after.
        /// </summary>
        /// <param name="value"></param>
        public virtual void HandleSetSelected(bool value)
        {
            //If same value, do nothing.
            if (_selected == value) return;
            if (!_selected && value && onSetSelected != null) onSetSelected.Invoke();
            else if (_selected && !value && onSetUnSelected != null) onSetUnSelected.Invoke();
            _selected = value;
        }

        /// <summary>
        /// Call to activate.
        /// </summary>
        public virtual void SelfActive()
        {
            if (!_selected && onSetSelected != null)
            {
                onSetSelected.Invoke();
                _selected = true;
            }
        }

        /// <summary>
        /// Call to click.
        /// </summary>
        public virtual void SelfClick() { }

        /// <summary>
        /// Set up data and spread infomation into containers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        public virtual void OnPostAdded_SetupUI<T>(T data, GameObject entity) { }
        #endregion
    }
}
