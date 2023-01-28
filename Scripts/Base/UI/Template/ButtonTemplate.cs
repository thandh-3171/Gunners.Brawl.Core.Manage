using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class ButtonTemplate : UIItemAbstract
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

        private UIContentAbstract content;
        #endregion

        #region mono
        /// <summary>
        /// Must be called first line. And must should be overrided.
        /// </summary>
        public override void Awake()
        {
            Initialized();
        }
        #endregion

        #region methods
        private void Initialized()
        {
            content = GetComponentInParent<UIContentAbstract>();
            if (content == null) Debug.Log("No content?! This is not intended.");
        }

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
