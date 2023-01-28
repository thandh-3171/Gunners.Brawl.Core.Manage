using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class BaseItemButton : UIItemAbstract
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
        [SerializeField] private bool _selected = false;

        /// <summary>
        /// Item-Interface of this class. Should be one.
        /// </summary>
        public UIItemAbstract itemInterface;
        #endregion

        #region mono
        /// <summary>
        /// Must be called first line. And must should be overrided.
        /// </summary>
        public virtual void Awake()
        {
            itemInterface = GetComponent<UIItemAbstract>();
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
            if (!_selected && value) onSetSelected?.Invoke();
            else if (_selected && !value) onSetUnSelected?.Invoke();
            _selected = value;
        }

        /// <summary>
        /// Call to activate.
        /// </summary>
        public override void Activate() { selected = true; }

        /// <summary>
        /// Call to click.
        /// </summary>
        public override void OnClick() { }

        /// <summary>
        /// Set up data and spread infomation into containers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        public override void OnPostAdded_SetupUI<T>(T data, GameObject entity) { }
        #endregion
    }
}
