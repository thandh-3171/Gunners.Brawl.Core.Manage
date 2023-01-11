using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJLived.GunnerStars.FirstGame.UI.Template
{
    public interface UIItemInterface
    {
        void OnPostAdded_SetupUI<T>(T data);
    }
}
