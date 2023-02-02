using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class UIEntityTemplate : UIEntityAbstract
    {
        #region async functions vars.
        /// <summary>
        /// Entity has this.
        /// </summary>
        protected CoroutineHandle HandleInitialized;
        #endregion

        #region mono
        /// <summary>
        /// 1. No async function to register here.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            // Register async functions.
            this.asyncFuncs.Add(HandleInitialized);
        }
        #endregion

        #region abstract methods
        /// <summary>
        /// Return if lived.
        /// </summary>        
        public override bool Lived()
        {
            return (this.enabled && this.gameObject.activeSelf);
        }

        /// <summary>
        /// Search info in list.
        /// </summary>        
        public override bool IsBusy()
        {
            return asyncFuncs.Exists(func => func != null && func.IsRunning);            
        }

        #endregion
    }
}
