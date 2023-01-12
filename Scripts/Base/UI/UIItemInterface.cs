using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJLived.GunnerStars.FirstGame.UI.Template
{
    public interface UIItemInterface
    {
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
