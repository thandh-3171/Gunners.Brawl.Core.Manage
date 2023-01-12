using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace PJLived.GunnerStars.FirstGame.UI.Template
{
    // <summary>
    // Hierychy : 
    // ScrollView
    //   -> ViewPort
    //      -> Content              // Sibling Index : Must 01
    //         -> Item
    //      -> Horizon  Bar         // Don't care. May delete.
    //      -> Vertical Bar         // Don't care. May delete
    ///     -> ContentPrefab        // Must UN-enable
    // </summary>
    public class ScrollViewTemplate : MonoBehaviour
    {
        #region Serialized
        [SerializeField] GameObject itemPrefab;
        #endregion

        #region Private vars
        private ScrollRect scrollViewScript;
        private GameObject scrollView;
        private GameObject viewPort;
        private GameObject content;
        private GameObject contentPrefab;
        private bool initialized = false;
        #endregion

        #region Mono
        private void Awake()
        {
            Initialized();
        }
        #endregion

        #region Private methods
        [ContextMenu("Initialized")]
        private void Initialized()
        {
            StartCoroutine(_Initialized());
        }
        IEnumerator _Initialized()
        {
            scrollViewScript = GetComponent<ScrollRect>();
            if (scrollViewScript == null)
            {
                Debug.LogWarning("Init error.");
                yield break;
            }
            scrollView = scrollViewScript.gameObject;
            viewPort = scrollViewScript.viewport.gameObject;
            content = scrollViewScript.content.gameObject;
            List<GameObject> deleteChild = new List<GameObject>();
            for (int child = 0; child < content.transform.childCount; child++)
                deleteChild.Add(content.transform.GetChild(child).gameObject);
            for (int child = 0; child < deleteChild.Count; child++)
                if (Application.isPlaying) Destroy(deleteChild[child].gameObject);
                else DestroyImmediate(deleteChild[child].gameObject);
            yield return new WaitUntil(() => content.transform.childCount == 0);
            if (contentPrefab == null)
            {
                contentPrefab = Instantiate(content, scrollView.transform);
                contentPrefab.SetActive(false);
                contentPrefab.name = "ContentPrefab";
            }
            initialized = true;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Add a new Item.
        /// Item should have interface.
        /// If index = -1, add new at the end.
        /// </summary>
        public virtual void AddItem<T>(T data, int index = -1)
        {
            if (itemPrefab == null)
            {
                Debug.Log("Item prefab null.");
                return;
            }
            if (content == null)
            {
                Debug.Log("Content null.");
                return;
            }
            GameObject newItem = Instantiate(itemPrefab, content.transform);
            UIItemInterface iItem = newItem.GetComponent<UIItemInterface>();
            if (iItem != null) iItem.OnPostAdded_SetupUI(data);
        }
        /// <summary>
        /// Delete and re-create is way faster than delete one by one.
        /// </summary>
        [ContextMenu("ResetScroll")]
        public void ResetScroll()
        {
            StartCoroutine(_ResetScroll());
        }
        IEnumerator _ResetScroll()
        {
            if (!this.initialized) yield return new WaitUntil(() => this.initialized);
            if (Application.isPlaying) Destroy(content.gameObject);
            else DestroyImmediate(content.gameObject);
            content = Instantiate(contentPrefab, viewPort.transform);
            content.SetActive(true);
            content.transform.SetAsFirstSibling();
            content.name = "Content";
            scrollViewScript.content = content.GetComponent<RectTransform>();
        }
        #endregion
    }
}