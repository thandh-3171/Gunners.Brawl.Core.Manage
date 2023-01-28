using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeAreProStars.Core.Manage.UI.Template;

namespace WeAreProStars.Core.Manage
{
    [Serializable]
    public abstract class UIContentAbstract : MonoBehaviour
    {
        #region methods
        /// <summary>
        /// Frequently, I set up prefabs for scroll rect, view port and content gameobject here.
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// Getting the ready state of this content.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsInitialized();

        /// <summary>
        /// To add an item.
        /// </summary>
        public abstract GameObject AddItem<T>(T data, int index = -1, bool autoActive = true);

        /// <summary>
        /// To select an item.
        /// </summary>
        public abstract void SelectItem(UIItemAbstract item);

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