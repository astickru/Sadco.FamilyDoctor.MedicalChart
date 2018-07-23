// ////////////////////////////////////////////////////////////////////////////
//
//  $RCSfile: ListBoxList.cs,v $
//
//  $Revision: 1.2 $
//
//  Last change:
//    $Author: Robert $
//    $Date: 2004/07/28 11:03:24 $
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
//
//  Original Code from Christian Tratz (via www.codeproject.com).
//  Changed by R. Lelieveld, SimVA GmbH.
//
// ////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;

namespace Sadco.FamilyDoctor.Core.Controls.ResizableListBox
{
    /// <summary>
    /// EventHanlder for adding items.
    /// </summary>
    public delegate void AddedEventHandler();

    /// <summary>
    /// EventHandler for inserting items.
    /// </summary>
    public delegate void InsertEventHandler(int index);

    /// <summary>
    /// EventHandler for removing items.
    /// </summary>
    public delegate void RemoveEventHandler(int index);

    /// <summary>
    /// ArrayList with OnItemInserted event.
    /// </summary>
    public class ListBoxList : IList
    {
        /// <summary>
        /// Internal messages list.
        /// </summary>
        private ArrayList _alObjects;
        ///// <summary>
        ///// Internal messages list.
        ///// </summary>
        //public ArrayList Objects {
        //    get { return _alObjects; }
        //}

        /// <summary>
		/// Internal information about the messages.
		/// </summary>
		private ArrayList _alObjectsInfo;

        /// <summary>
        /// Number of items in the list.
        /// </summary>
        /// <returns></returns>
        public int Count {
            get { return _alObjects.Count; }
        }

        public bool IsReadOnly => ((IList)_alObjects).IsReadOnly;

        public bool IsFixedSize => ((IList)_alObjects).IsFixedSize;

        public object SyncRoot => ((IList)_alObjects).SyncRoot;

        public bool IsSynchronized => ((IList)_alObjects).IsSynchronized;

        object IList.this[int index] { get => ((IList)_alObjects)[index]; set => ((IList)_alObjects)[index] = value; }


        /// <summary>
        /// Item at index.
        /// </summary>
        public object this[int index] {
            get { return _alObjects[index]; }
        }


        /// <summary>
        /// Item has been added.
        /// </summary>
        public event AddedEventHandler OnItemAdded;

        /// <summary>
        /// Item has been inserted.
        /// </summary>
        public event InsertEventHandler OnItemInserted;

        /// <summary>
        /// Item has been inserted.
        /// </summary>
        public event RemoveEventHandler OnItemRemoved;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ListBoxList()
        {
            _alObjects = new ArrayList();
            _alObjectsInfo = new ArrayList();
        }


        /// <summary>
        /// Add an item.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Add(Object obj)
        {
            int index = _alObjects.Add(obj);
            _alObjectsInfo.Add(new ItemInfo(obj));
            OnAdd();
            return index;
        }


        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            _alObjects.Clear();
        }


        /// <summary>
        /// Index of the object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int IndexOf(object obj)
        {
            return _alObjects.IndexOf(obj);
        }

        /// <summary>
		/// Returns more information (ItemInfo object) about an item.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public ItemInfo Info(int index)
        {
            return (ItemInfo)_alObjectsInfo[index];
        }

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pmea"></param>
        public void Insert(int index, object obj)
        {
            _alObjects.Insert(index, obj);
            _alObjectsInfo.Add(new ItemInfo(obj));
            OnInsert(index);
        }

        public void Remove(object obj)
        {
            int _index = _alObjects.IndexOf(obj);
            if (_index > -1)
            {
                _alObjects.RemoveAt(_index);
                _alObjectsInfo.RemoveAt(_index);
                OnRemove(_index);
            }
        }

        public void Remove(int index)
        {
            _alObjects.RemoveAt(index);
            _alObjectsInfo.RemoveAt(index);
            OnRemove(index);
        }

        /// <summary>
        /// Raises the 'OnItemAdded' event.
        /// </summary>
        private void OnAdd()
        {
            if (OnItemAdded != null)
                OnItemAdded();
        }

        /// <summary>
        /// Raises the 'OnItemInserted' event.
        /// </summary>
        /// <param name="index"></param>
        private void OnInsert(int index)
        {
            if (OnItemInserted != null)
                OnItemInserted(index);
        }

        /// <summary>
        /// Raises the 'OnItemRemoved' event.
        /// </summary>
        /// <param name="index"></param>
        private void OnRemove(int index)
        {
            if (OnItemRemoved != null)
                OnItemRemoved(index);
        }

        public bool Contains(object value)
        {
            return ((IList)_alObjects).Contains(value);
        }

        public void RemoveAt(int index)
        {
            ((IList)_alObjects).RemoveAt(index);
        }

        public void CopyTo(Array array, int index)
        {
            ((IList)_alObjects).CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IList)_alObjects).GetEnumerator();
        }
    }
}
