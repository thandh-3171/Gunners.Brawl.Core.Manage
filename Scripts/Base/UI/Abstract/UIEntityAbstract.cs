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

        /// <summary>
        /// Store any data.
        /// </summary>
        protected List<object> library = new();
        #endregion

        #region abstract methods
        /// <summary>
        /// 1. Register all async functions here.
        /// </summary>
        public abstract void Awake();
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
