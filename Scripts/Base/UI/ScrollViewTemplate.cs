using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PJLived.GunnerStars.FirstGame.UI.Template
{
    public class ScrollViewTemplate : MonoBehaviour
    {
        #region Private vars
        ScrollView scrollView;
        #endregion

        #region Mono
        private void Awake()
        {
            scrollView = GetComponent<ScrollView>();

        }
        #endregion
    }
}