using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class BaseItemButton : MonoBehaviour
        , UIItemInterface
    {
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
        public virtual void HandleSetSelected(bool value) { }

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
