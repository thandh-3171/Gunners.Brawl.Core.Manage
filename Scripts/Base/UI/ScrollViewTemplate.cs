using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WeAreProStars.Core.Manage.UI.Template
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
        #region events
        // Event right at the moment select an item.
        public delegate void OnSelectItem(List<UIItemAbstract> items);
        public OnSelectItem onSelectItem;

        // Event after selecting an item.
        public delegate void OnClickItem(List<UIItemAbstract> items);
        public OnClickItem onClickItem;

        // Event of clearing current selection.
        public delegate void OnClearSelection(List<UIItemAbstract> lastItems);
        public OnClearSelection onClearSelection;
        #endregion

        #region Serialized
        [SerializeField] GameObject itemPrefab;
        public bool Multiple = false;
        #endregion

        #region Private vars
        private ScrollRect scrollViewScript;
        private GameObject scrollView;
        private GameObject viewPort;
        private GameObject content;
        private GameObject contentPrefab;
        private bool initialized = false;
        #endregion

        #region Public vars
        public List<UIItemAbstract> _items = new();
        public List<UIItemAbstract> _selectingItems = new();
        public UIItemAbstract _indexedItem;
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

        protected virtual IEnumerator _Initialized()
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
        /// If autoActive == true, set Index value at 01.
        /// </summary>
        public virtual GameObject AddItem<T>(T data, int index = -1, bool autoActive = true)
        {
            if (itemPrefab == null)
            {
                Debug.Log("Item prefab null.");
                return null;
            }
            if (content == null)
            {
                Debug.Log("Content null.");
                return null;
            }
            GameObject newItem = Instantiate(itemPrefab, content.transform);
            UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
            if (iItem != null)
            {
                iItem.OnPostAdded_SetupUI(data, newItem);
                _items.Add(iItem);
                if (autoActive && _items.Count == 1) SelectItem(iItem);
            }
            return newItem;
        }

        /// <summary>
        /// Handle when select an item.
        /// </summary>
        /// <param name="item"></param>
        public virtual void SelectItem(UIItemAbstract item)
        {
            // Set current selected item to the next value.
            if (this._selectingItems.Count == 0) this._selectingItems.Add(item);
            else
            {
                if (Multiple) this._selectingItems.Add(item);
                else this._selectingItems[0] = item;
            }
            this._indexedItem = item;
            onClearSelection?.Invoke(this._selectingItems);
            onSelectItem?.Invoke(this._selectingItems);
        }

        /// <summary>
        /// Handle after select an item and move on to the next stage.
        /// If you want to select, must call SelectItem.
        /// </summary>
        /// <param name="item"></param>
        public virtual void ClickItem(UIItemAbstract item)
        {
            // I may just handle the post click event here.
            onClickItem?.Invoke(this._selectingItems);
        }

        /// <summary>
        /// Clear the selection.
        /// Reset the state of the last picked item.
        /// </summary>
        public virtual void ClearSeletion()
        {
            onClearSelection?.Invoke(this._selectingItems);
        }

        /// <summary>
        /// Delete and re-create is way faster than delete one by one.
        /// </summary>
        [ContextMenu("ResetScroll")]
        public virtual void ResetScroll()
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
            onClickItem = null;
            onClearSelection = null;
            _items = new List<UIItemAbstract>();
        }

        /// <summary>
        /// Force item at index to click.
        /// </summary>
        /// <param name="index"></param>
        public virtual void ClickItemAt(int index)
        {
            if (0 < index && index < this._items.Count)
                this._items[index].OnClick();
        }

        /// <summary>
        /// Click the next item.
        /// </summary>
        public virtual void SelectNextItem()
        {
            if (this._items.Count <= 0) return;
            if (this._indexedItem == null) ClickItemAt(0);
            else
            {
                var nextIndex = this._items.IndexOf(this._indexedItem);
                if (nextIndex >= this._items.Count - 1) nextIndex = 0;
                else nextIndex++;
                if (!this._selectingItems.Contains(this._items[nextIndex]))
                {
                    SelectItem(this._items[nextIndex]);
                    ClickItem(this._items[nextIndex]);
                }
            }
        }

        /// <summary>
        /// Click the next item.
        /// </summary>
        public virtual void SelectPreviousItem()
        {
            if (this._items.Count <= 0) return;
            if (this._indexedItem == null) ClickItemAt(this._items.Count - 1);
            else
            {
                var previousIndex = this._items.IndexOf(this._indexedItem);
                if (previousIndex <= 0) previousIndex = this._items.Count - 1;
                else previousIndex--;
                if (!this._selectingItems.Contains(this._items[previousIndex]))
                {
                    SelectItem(this._items[previousIndex]);
                    ClickItem(this._items[previousIndex]);
                }
            }
        }
        #endregion
    }
}