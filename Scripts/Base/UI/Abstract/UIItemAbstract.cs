using System;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    [Serializable]
    public abstract class UIItemAbstract : MonoBehaviour
    {
        #region vars
        private UIContentAbstract content;
        #endregion

        #region methods
        /// <summary>
        /// I want every inheritances must override Awake()
        /// To set up their own container (parent).
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// Call to force active.
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Call to click.
        /// </summary>
        public abstract void OnClick();

        /// <summary>
        /// Data (type T) is the class contain infomation.
        /// Entity is the button or the UI item.
        /// </summary>
        public abstract void OnPostAdded_SetupUI<T>(T data, GameObject entity);
        #endregion
    }
}
