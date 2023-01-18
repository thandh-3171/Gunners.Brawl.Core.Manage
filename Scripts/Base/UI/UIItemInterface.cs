using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public interface UIItemInterface
    {
        /// <summary>
        /// Call to force active.
        /// </summary>
        void Active();

        /// <summary>
        /// Get the transform of this item.
        /// </summary>
        /// <returns></returns>
        /// Transform transform();

        /// <summary>
        /// T data is the class contain infomation.
        /// GameObject entity is the button or the UI item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        void OnPostAdded_SetupUI<T>(T data, GameObject entity);
    }
}
