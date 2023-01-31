using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class UIEntityTemplate : UIEntityAbstract
    {
        /// <summary>
        /// Return if lived.
        /// </summary>        
        public override bool Lived()
        {
            return (this.enabled && this.gameObject.activeSelf);
        }
    }
}
