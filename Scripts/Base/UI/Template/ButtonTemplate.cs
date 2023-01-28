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
        public override IEnumerator Start()
        {
            yield return StartCoroutine(Initialized());
        }
        #endregion

        #region methods
        IEnumerator Initialized()
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
        public override IEnumerator Activate()
        {
            yield return new WaitUntil(() => this.initialized);
            // Perform vfx when select.
            if (this.content != null) content.Activate(this);
        }

        /// <summary>
        /// Call to perform task of clicking.
        /// </summary>
        public override IEnumerator OnClick()
        {
            yield return new WaitUntil(() => this.initialized);
            if (!selected) StartCoroutine(Activate());
        }

        /// <summary>
        /// Set up data and spread infomation into containers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        public override IEnumerator OnPostAdded_SetupUI<T>(T data, GameObject entity)
        {
            yield return new WaitUntil(() => this.initialized);
            this.button.onClick.AddListener(() => StartCoroutine(OnClick()));
        }
        #endregion
    }
}
