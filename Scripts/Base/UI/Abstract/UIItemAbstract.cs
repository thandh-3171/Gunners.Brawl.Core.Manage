using System;
using System.Collections;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    [Serializable]
    public abstract class UIItemAbstract : MonoBehaviour
    {
        #region events
        public delegate void OnSetSelected();
        public OnSetSelected onSetSelected;

        public delegate void OnSetUnSelected();
        public OnSetUnSelected onSetUnSelected;
        #endregion

        #region methods
        /// <summary>
        /// I want every inheritances must override Awake()
        /// Example : Button needs to set up its own container (parent).
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// Call to click.
        /// </summary>
        public abstract void OnClick();

        /// <summary>
        /// Data (type T) is the class contain infomation.
        /// Entity is the button or the UI item.
        /// </summary>
        public abstract IEnumerator OnPostAdded_SetupUI<T>(T data, GameObject entity);
        #endregion
    }
}
