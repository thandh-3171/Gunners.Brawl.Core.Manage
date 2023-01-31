using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class UIEntityTemplate : UIEntityAbstract
    {
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
            return asyncFuncs.Find(func => func.IsRunning) != null;
        }
        #endregion
    }
}
