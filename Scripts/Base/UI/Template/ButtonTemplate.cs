using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class ButtonTemplate : UIItemAbstract
    {
        #region public vars
        /// <summary>
        /// Readying state.
        /// </summary>
        //[HideInInspector]
        public bool initialized = false;
        /// <summary>
        /// State of being selected.
        /// </summary>
        public bool selected
        {
            get { return _selected; }
            set { HandleSetSelected(value); }
        }
        [SerializeField] private bool _selected = false;
        #endregion

        #region private vars
        protected UIContentAbstract content;
        protected Button button;
        #endregion

        #region mono
        /// <summary>
        /// Must be called first line. And must should be overrided.
        /// </summary>
        public override void Awake()
        {
            Initialized();
        }
        #endregion

        #region methods
        private void Initialized()
        {
            StartCoroutine(_Initialized());
        }

        IEnumerator _Initialized()
        {
            var time = Time.time;
            yield return new WaitUntil(() =>
            (GetComponentInParent<UIContentAbstract>() != null && GetComponent<Button>() != null)
            || Time.time - time > 5f);
            if (Time.time - time > 5f)
            {
                Debug.LogWarning("Init error.");
                yield break;
            }
            this.content = GetComponentInParent<UIContentAbstract>();
            this.button = GetComponent<Button>();
            this.initialized = true;
        }

        /// <summary>
        /// Handle how to set selected state.
        /// Value be check first, set after.
        /// </summary>
        /// <param name="value"></param>
        public virtual void HandleSetSelected(bool value)
        {
            //If same value, do nothing.
            if (_selected == value) return;
            if (!_selected && value) onSetSelected?.Invoke();
            else if (_selected && !value) onSetUnSelected?.Invoke();
            _selected = value;
        }

        /// <summary>
        /// Call to activate.
        /// </summary>
        public override void Activate() { selected = true; }

        /// <summary>
        /// Call to click.
        /// </summary>
        public override void OnClick() { }

        /// <summary>
        /// Set up data and spread infomation into containers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        public override IEnumerator OnPostAdded_SetupUI<T>(T data, GameObject entity)
        {
            yield return new WaitUntil(() => this.initialized);
        }
        #endregion
    }
}
