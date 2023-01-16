using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class BaseItemButton : MonoBehaviour
        , UIItemInterface
    {
        #region vars
        public bool selected
        {
            get { return _selected; }
            set { HandleSetSelected(value); }
        }
        private bool _selected = false;
        #endregion

        #region methods
        public virtual void HandleSetSelected(bool value) { }
        public virtual void OnPostAdded_SetupUI<T>(T data, GameObject entity) { }
        #endregion
    }
}
