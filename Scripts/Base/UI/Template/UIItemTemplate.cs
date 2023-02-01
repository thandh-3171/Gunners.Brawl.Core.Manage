using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeAreProStars.Core.Manage.UI.Template
{
    public class UIItemTemplate : UIItemAbstract
    {
        public override void Activate() { }
        public override void ActivateQueue() { }
        public override void Initialized() { }
        public override void OnClick() { }
        public override void OnPostAdded_SetupUI<T>(T data, GameObject entity) { }
        public override void OnPostQueueAdded_SetupUI<T>(T data, GameObject entity) { }
        public override IEnumerator<float> _ActivateQueue() { yield break; }
        public override IEnumerator<float> _OnPostQueueAdded_SetupUI<T>(T data, GameObject entity) { yield break; }
    }
}