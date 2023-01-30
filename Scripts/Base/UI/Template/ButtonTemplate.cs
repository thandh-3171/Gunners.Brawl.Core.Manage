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
        // public override void Awake()
        // {            
        // Don't ever call init in Awake() again.
        // So much unstable while using with MEC Coroutine or any coroutine.
        // Must/should call as followed : 
        // => Add -> Init -> OnPostInit.
        // Code :
        // Timing.RunCoroutine(_Initialized());
        // }
        #endregion

        #region methods
        //public override IEnumerator<float> Initialized()
        //{
        //    var time = Time.time;
        //    yield return Timing.WaitUntilTrue(() =>
        //    (GetComponentInParent<UIContentAbstract>() != null && GetComponent<Button>() != null)
        //    || Time.time - time > 5f);
        //    if (Time.time - time > 5f)
        //    {
        //        Debug.LogWarning("Init error.");
        //        yield break;
        //    }
        //    this.content = GetComponentInParent<UIContentAbstract>();
        //    this.button = GetComponent<Button>();
        //    this.initialized = true;
        //}
        public override void Initialized()
        {
            this.content = GetComponentInParent<UIContentAbstract>();
            if (this.content == null) Debug.Log("Null content.");
            this.button = GetComponent<Button>();
            if (this.button == null) Debug.Log("Null button.");
            this.initialized = true;
        }

        /// <summary>
        /// Call to perform task of clicking.
        /// </summary>        
        public override void Activate()
        {
            if (!initialized) return;
            // Perform vfx when select.
            if (this.content != null) content.Activate(this);
        }

        /// <summary>
        /// Call to wait init and execute activate.
        /// </summary>        
        public override void ActivateQueue()
        {
            Timing.RunCoroutine(_ActivateQueue());
        }

        /// <summary>
        /// Call to wait init and execute activate.
        /// </summary>        
        public override IEnumerator<float> _ActivateQueue()
        {
            yield return Timing.WaitUntilTrue(() => this.initialized);
            Activate();
        }

        /// <summary>
        /// Call to perform task of clicking.
        /// </summary>
        //public override IEnumerator<float> OnClick()
        //{
        //    yield return Timing.WaitUntilTrue(() => this.initialized);
        //    if (!selected) StartCoroutine(Activate());
        //}
        public override void OnClick()
        {
            if (!initialized) return;
            if (!selected) Activate();
        }

        /// <summary>
        /// Set up data and spread infomation into containers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="entity"></param>        
        public override void OnPostAdded_SetupUI<T>(T data, GameObject entity)
        {
            if (!initialized) return;
            this.button.onClick.AddListener(() => OnClick());
        }

        /// <summary>
        /// Call to wait init and execute onpostadd set-up.
        /// </summary>        
        public override void OnPostQueueAdded_SetupUI<T>(T data, GameObject entity)
        {
            Timing.RunCoroutine(_OnPostQueueAdded_SetupUI<T>(data, entity));
        }

        /// <summary>
        /// Call to wait init and execute onpostadd set-up.
        /// </summary>        
        public override IEnumerator<float> _OnPostQueueAdded_SetupUI<T>(T data, GameObject entity)
        {
            yield return Timing.WaitUntilTrue(() => this.initialized);
            OnPostAdded_SetupUI<T>(data, entity);
        }
        #endregion
    }
}
