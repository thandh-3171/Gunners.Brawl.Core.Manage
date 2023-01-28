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
        [HideInInspector]
        public bool initialized = false;
        #endregion

        #region private vars
        /// <summary>
        /// The parent.
        /// </summary>
        protected UIContentAbstract content;
        /// <summary>
        /// Button component.
        /// </summary>
        public Button button
        {
            get { return _button; }
            private set { _button = value; }
        }
        private Button _button;
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
        /// Call to perform task of clicking.
        /// </summary>
        public override void Activate()
        {
            // Perform vfx when select.
            if (this.content != null) content.Activate(this);
        }

        /// <summary>
        /// Call to perform task of clicking.
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
            this.button.onClick.AddListener(() => OnClick());
        }
        #endregion
    }
}
