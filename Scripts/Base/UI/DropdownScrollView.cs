//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace WeAreProStars.Core.Manage.UI.Template
//{
//    // <summary>
//    // Hierychy : 
//    // ScrollView
//    //   -> ViewPort
//    //      -> Content              // Sibling Index : Must 01
//    //         -> Item
//    //      -> Horizon  Bar         // Don't care. May delete.
//    //      -> Vertical Bar         // Don't care. May delete
//    ///     -> ContentPrefab        // Must UN-enable
//    // </summary>

//    public class DropdownScrollView : MonoBehaviour
//    {
//        #region Serialized
//        [SerializeField] GameObject itemPrefab;
//        #endregion

//        #region Private vars
//        private ScrollViewTemplate scrollViewScript;
//        private GameObject scrollView;
//        private GameObject viewPort;
//        private GameObject content;
//        private GameObject contentPrefab;
//        private bool initialized = false;
//        #endregion

//        #region Mono
//        private void Awake()
//        {
//            Initialized();
//        }
//        #endregion

//        #region Private methods
//        [ContextMenu("Initialized")]
//        private void Initialized()
//        {
//            StartCoroutine(_Initialized());
//        }
//        protected override IEnumerator _Initialized()
//        {
//            scrollViewScript = GetComponent<ScrollRect>();
//            if (scrollViewScript == null)
//            {
//                Debug.LogWarning("Init error.");
//                yield break;
//            }
//            scrollView = scrollViewScript.gameObject;
//            viewPort = scrollViewScript.viewport.gameObject;
//            content = scrollViewScript.content.gameObject;
//            List<GameObject> deleteChild = new List<GameObject>();
//            for (int child = 0; child < content.transform.childCount; child++)
//                deleteChild.Add(content.transform.GetChild(child).gameObject);
//            for (int child = 0; child < deleteChild.Count; child++)
//                if (Application.isPlaying) Destroy(deleteChild[child].gameObject);
//                else DestroyImmediate(deleteChild[child].gameObject);
//            yield return new WaitUntil(() => content.transform.childCount == 0);
//            if (contentPrefab == null)
//            {
//                contentPrefab = Instantiate(content, scrollView.transform);
//                contentPrefab.SetActive(false);
//                contentPrefab.name = "ContentPrefab";
//            }
//            initialized = true;
//        }
//        #endregion

//        #region public methods
//        /// <summary>
//        /// Add a new Item.
//        /// Item should have interface.
//        /// If index = -1, add new at the end.
//        /// If autoActive == true, set Index value at 01.
//        /// </summary>
//        public override GameObject AddItem<T>(T data, int index = -1, bool autoActive = true)
//        {
//            return base.AddItem<T>(data);
//        }

//        /// <summary>
//        /// Handle when select an item.
//        /// </summary>
//        /// <param name="item"></param>
//        public override void SelectItem(UIItemAbstract item)
//        {
//            base.SelectItem(item);
//        }

//        /// <summary>
//        /// Clear the selection.
//        /// Reset the state of the last picked item.
//        /// </summary>
//        public override void ClearSeletion()
//        {
//            if (onClearSelection != null) 
//                onClearSelection.Invoke(this._selectingItems);
//        }

//        /// <summary>
//        /// Delete and re-create is way faster than delete one by one.
//        /// </summary>
//        [ContextMenu("ResetScroll")]
//        public override void ResetScroll()
//        {
//            StartCoroutine(_ResetScroll());
//        }
//        IEnumerator _ResetScroll()
//        {
//            if (!this.initialized) yield return new WaitUntil(() => this.initialized);
//            if (Application.isPlaying) Destroy(content.gameObject);
//            else DestroyImmediate(content.gameObject);
//            content = Instantiate(contentPrefab, viewPort.transform);
//            content.SetActive(true);
//            content.transform.SetAsFirstSibling();
//            content.name = "Content";
//            scrollViewScript.content = content.GetComponent<RectTransform>();
//            onClickItem = null;
//            onClearSelection = null;
//            _items = new List<UIItemAbstract>();
//        }

//        /// <summary>
//        /// Force item at index to click.
//        /// </summary>
//        /// <param name="index"></param>
//        public override void ClickItemAt(int index)
//        {
//            if (0 < index && index < this._items.Count)
//                this._items[index].OnClick();
//        }

//        /// <summary>
//        /// Click the next item.
//        /// </summary>
//        public override void SelectNextItem()
//        {
//            if (this._items.Count <= 0) return;
//            if (_selectingItem == null) ClickItemAt(0);
//            else
//            {
//                var nextIndex = this._items.IndexOf(_selectingItem);
//                if (nextIndex >= this._items.Count - 1) nextIndex = 0;
//                else nextIndex++;
//                this._items[nextIndex].OnClick();
//            }
//        }

//        /// <summary>
//        /// Click the next item.
//        /// </summary>
//        public override void SelectPreviousItem()
//        {
//            if (this._items.Count <= 0) return;
//            if (_selectingItem == null) ClickItemAt(this._items.Count - 1);
//            else
//            {
//                var previousIndex = this._items.IndexOf(_selectingItem);
//                if (previousIndex <= 0) previousIndex = this._items.Count - 1;
//                else previousIndex--;
//                this._items[previousIndex].OnClick();
//            }
//        }
//        #endregion
//    }
//}