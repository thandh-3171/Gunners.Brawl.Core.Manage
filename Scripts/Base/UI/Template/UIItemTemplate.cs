using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class UIItemTemplate : UIItemAbstract
    {
        public override void Activate() { }
        public override void ActivateQueue()
        {
            if (!this.enabled || !gameObject.activeSelf)
            {
                Debug.Log("Not enabled. Can't run.");
                return;
            }
            Timing.RunCoroutine(_ActivateQueue());
        }
        public override void Initialized() { }
        public override void OnClick() { }
        public override void OnPostAdded_SetupUI<T>(T data, GameObject entity) { }
        public override void OnPostQueueAdded_SetupUI<T>(T data, GameObject entity)
        {
            if (!this.enabled || !gameObject.activeSelf)
            {
                Debug.Log("Not enabled. Can't run.");
                return;
            }
            Timing.RunCoroutine(_OnPostQueueAdded_SetupUI<T>(data, entity));
        }
        public override IEnumerator<float> _ActivateQueue()
        {
            yield return Timing.WaitUntilTrue(() => this.initialized);
            Activate();
            yield break;
        }
        public override IEnumerator<float> _OnPostQueueAdded_SetupUI<T>(T data, GameObject entity)
        {
            if (!this.enabled || !gameObject.activeSelf)
            {
                Debug.Log("Not enabled. Can't run.");
                yield break;
            }
            yield return Timing.WaitUntilTrue(() => this.initialized);
            OnPostAdded_SetupUI<T>(data, entity);
        }
    }
}