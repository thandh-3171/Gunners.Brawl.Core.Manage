using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public abstract class UIEntityAbstract : MonoBehaviour
    {
        #region public vars
        /// <summary>
        /// Contain async funcs.
        /// </summary>
        public List<CoroutineHandle> asyncFuncs = new();
        #endregion
        
        #region abstract methods
        /// <summary>
        /// Return if lived.
        /// </summary>        
        public abstract bool Lived();

        /// <summary>
        /// Return state of processing.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsBusy();
        #endregion
    }
}
