using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public abstract class UIEntityAbstract : MonoBehaviour
    {
        #region abstract methods
        /// <summary>
        /// Return if lived.
        /// </summary>        
        public abstract bool Lived();
        #endregion
    }
}
