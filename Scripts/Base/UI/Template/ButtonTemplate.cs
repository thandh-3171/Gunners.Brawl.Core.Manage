using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

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
        public override void Awake()
        {
            Timing.RunCoroutine(_Initialized());
        }
        #endregion

        #region methods
        IEnumerator<float> _Initialized()
        {
            var time = Time.time;
            yield return Timing.WaitUntilTrue(() =>
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
        public override IEnumerator<float> Activate()
        {
            yield return Timing.WaitUntilTrue(() => this.initialized);
            // Perform vfx when select.
            if (this.content != null) content.Activate(this);
        }

        /// <summary>
        /// Call to perform task of clicking.
        /// </summary>
        public override IEnumerator<float> OnClick()
        {
            yield return Timing.WaitUntilTrue(() => this.initialized);
            if (!selected) StartCoroutine(Activate());
        }

        /// <summary>
        /// Set up data and spread infomation into containers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        public override IEnumerator<float> OnPostAdded_SetupUI<T>(T data, GameObject entity)
        {
            yield return Timing.WaitUntilTrue(() => this.initialized);
            this.button.onClick.AddListener(() => StartCoroutine(OnClick()));
        }
        #endregion
    }
}
