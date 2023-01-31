using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

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

    public class ScrollViewTemplate : UIContentAbstract
    {
        #region Serialized
        [SerializeField] GameObject itemPrefab;
        public bool Multiple = false;
        #endregion

        #region Public vars
        //[HideInInspector]
        public List<UIItemAbstract> _items = new();
        //[HideInInspector]
        public List<UIItemAbstract> _selectingItems = new();
        #endregion

        #region Private vars
        private ScrollRect scrollViewScript;
        private GameObject scrollView;
        private GameObject viewPort;
        private GameObject content;
        private GameObject contentPrefab;
        #endregion        

        #region Mono
        /// <summary>
        /// Must be called first line. And must should be overrided.
        /// </summary>
        public override void Awake()
        {
            Timing.RunCoroutine(_Initialized());
        }
        #endregion

        #region Private methods        
        IEnumerator<float> _Initialized()
        {
            var time = Time.time;
            yield return Timing.WaitUntilTrue(() => GetComponent<ScrollRect>() != null || Time.time - time > 5f);
            if (Time.time - time > 5f)
            {
                Debug.LogWarning("Init error. No Scroll Rect.");
                yield break;
            }
            scrollViewScript = GetComponent<ScrollRect>();
            scrollView = scrollViewScript.gameObject;
            viewPort = scrollViewScript.viewport.gameObject;
            content = scrollViewScript.content.gameObject;
            List<GameObject> deleteChild = new List<GameObject>();
            for (int child = 0; child < content.transform.childCount; child++)
                deleteChild.Add(content.transform.GetChild(child).gameObject);
            for (int child = 0; child < deleteChild.Count; child++)
                if (Application.isPlaying) Destroy(deleteChild[child].gameObject);
                else DestroyImmediate(deleteChild[child].gameObject);
            yield return Timing.WaitUntilTrue(() => content.transform.childCount == 0);
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
        /// Add a new item asyncly.
        /// </summary>
        public override void AddQueueItem<T>(T data, int index = -1, bool autoActive = true, bool forceLived = false)
        {
            if (!Lived())
            {
                if (!forceLived)
                {
                    Debug.Log("Not enabled. Can't run.");
                    return;
                }
                else { }
                //TODO : Code to force game object become active here.
            }
            Timing.RunCoroutine(_AddQueueItem<T>(data, index, autoActive));
        }

        IEnumerator<float> _AddQueueItem<T>(T data, int index = -1, bool autoActive = true)
        {
            GameObject newItem = _InstantiateItem();
            UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
            yield return Timing.WaitUntilTrue(() => iItem.Lived());
            _InitializeQueueItem<T>(data, newItem);
            _items.Add(iItem);
            if (autoActive && _items.Count == 1) iItem.ActivateQueue();
        }

        /// <summary>
        /// Add a new Item and return it.
        /// Item should have interface.
        /// If index = -1, add new at the end.
        /// If autoActive == true, set Index value at 01.
        /// </summary>
        public override GameObject AddReturnItem<T>(T data, int index = -1, bool autoActive = true)
        {
            GameObject newItem = _InstantiateItem();
            UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
            _InitializeQueueItem<T>(data, newItem);
            _items.Add(iItem);
            if (autoActive && _items.Count == 1) iItem.ActivateQueue();
            return newItem;
        }

        private GameObject _InstantiateItem()
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
            return newItem;
        }

        /// <summary>
        /// Call item's own asynchronous functions.
        /// </summary>        
        private void _InitializeQueueItem<T>(T data, GameObject newItem)
        {
            UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
            if (iItem != null)
            {
                iItem.Initialized();
                iItem.OnPostQueueAdded_SetupUI(data, newItem);
            }
        }

        //private IEnumerator<float> _InitializingNewItem<T>(GameObject newItem, T data, int index = -1, bool autoActive = true)
        //{
        //    UIItemAbstract iItem = newItem.GetComponent<UIItemAbstract>();
        //    if (iItem != null)
        //    {
        //        yield return Timing.WaitUntilDone(Timing.RunCoroutine(iItem.Initialized()
        //            //Use this to bypass error.
        //            //.CancelWith(newItem)
        //            ));
        //        yield return Timing.WaitUntilDone(Timing.RunCoroutine(iItem.OnPostAdded_SetupUI(data, newItem)
        //            //.CancelWith(newItem)
        //            ));
        //        _items.Add(iItem);
        //        if (autoActive && _items.Count == 1)
        //        {
        //            yield return Timing.WaitUntilDone(Timing.RunCoroutine(iItem.Activate()));
        //            Debug.Log("First Activate.");
        //        }
        //    }
        //    yield break;
        //}

        /// <summary>
        /// Handle vfx when select an item.
        /// </summary>
        /// <param name="item"></param>
        public override void Activate(UIItemAbstract item)
        {
            if (!Multiple) SingleChoiceActivate(item);
            else MultipleChoiceActivate(item);
        }

        /// <summary>
        /// Activate with multiple style. (Like : Button)
        /// </summary>
        /// <param name="item"></param>
        private void SingleChoiceActivate(UIItemAbstract item)
        {
            // False to True.
            if (!item.selected)
            {
                // None selected.
                if (this._selectingItems.Count == 0)
                {
                    this._selectingItems.Add(item);
                }
                // Being selected.
                else
                {
                    this._selectingItems[0].selected = false;
                    this._selectingItems[0] = item;
                }
            }
            // True to False.
            else
            {
                this._selectingItems.Remove(item);
            }
            item.selected = !item.selected;
        }

        /// <summary>
        /// Activate with multiple style. (Like : Toggle)
        /// </summary>        
        private void MultipleChoiceActivate(UIItemAbstract item)
        {
            //ToDo :
        }

        /// <summary>
        /// Handle vfx when select an item.
        /// </summary>
        /// <param name="item"></param>
        public override void ClickItem(UIItemAbstract item) { }

        /// <summary>
        /// Clear the selection.
        /// Reset the state of the last picked item.
        /// </summary>
        public override void ClearSeletion()
        {
            //onClearSelection?.Invoke(this._selectingItems);
        }

        /// <summary>
        /// Delete and re-create is way faster than delete one by one.
        /// </summary>
        public override void ResetContent()
        {            
            Timing.RunCoroutine(_ResetScroll());
        }

        IEnumerator<float> _ResetScroll()
        {
            if (!this.initialized) yield return Timing.WaitUntilTrue(() => this.initialized);
            this.initialized = false;
            if (Application.isPlaying) Destroy(content.gameObject);
            else DestroyImmediate(content.gameObject);
            content = Instantiate(contentPrefab, viewPort.transform);
            content.SetActive(true);
            content.transform.SetAsFirstSibling();
            content.name = "Content";
            scrollViewScript.content = content.GetComponent<RectTransform>();
            
            _items = new();
            _selectingItems = new();
            this.initialized = true;
        }

        /// <summary>
        /// Force item at index to click.
        /// </summary>
        /// <param name="index"></param>
        public virtual void ClickItemAt(int index)
        {
            //if (0 < index && index < this._items.Count)
            //    this._items[index].OnClick();
        }

        /// <summary>
        /// Click the next item.
        /// </summary>
        public virtual void SelectNextItem()
        {
            //if (this._items.Count <= 0) return;
            //if (this._indexedItem == null) ClickItemAt(0);
            //else
            //{
            //    var nextIndex = this._items.IndexOf(this._indexedItem);
            //    if (nextIndex >= this._items.Count - 1) nextIndex = 0;
            //    else nextIndex++;
            //    if (!this._selectingItems.Contains(this._items[nextIndex]))
            //    {
            //        this._items[nextIndex].OnClick();
            //    }
            //}
        }

        /// <summary>
        /// Click the next item.
        /// </summary>
        public virtual void SelectPreviousItem()
        {
            //if (this._items.Count <= 0) return;
            //if (this._indexedItem == null) ClickItemAt(this._items.Count - 1);
            //else
            //{
            //    var previousIndex = this._items.IndexOf(this._indexedItem);
            //    if (previousIndex <= 0) previousIndex = this._items.Count - 1;
            //    else previousIndex--;
            //    if (!this._selectingItems.Contains(this._items[previousIndex]))
            //    {
            //        this._items[previousIndex].OnClick();
            //    }
            //}
        }

        [ContextMenu("ActiveFirstItem")]
        public void ActiveFirstItem()
        {
            _items[0].Activate();
        }
        #endregion
    }
}