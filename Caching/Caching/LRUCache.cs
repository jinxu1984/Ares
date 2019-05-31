using System;
using System.Collections.Generic;
using System.Threading;

namespace Caching
{
    public class LRUCache
    {
        private readonly int capacity;
        private readonly Dictionary<int, CacheNode> cachedItems;
        private readonly object lockObj = new object();

        private CacheNode head = null;
        private CacheNode tail = null;

        public LRUCache(int capacity)
        {
            if (capacity < 1)
            {
                throw new InvalidCacheSizeException(string.Format("Cache size {0} is invalid", capacity));
            }

            this.capacity = capacity;
            cachedItems = new Dictionary<int, CacheNode>();
        }

        public int Get(int key)
        {
            lock (lockObj)
            {
                if (!cachedItems.ContainsKey(key)) { return -1; }

                MakeMostRecentlyUsed(cachedItems[key]);

                return cachedItems[key].Value;
            }        
        }

        public void Put(int key, int value)
        {
            lock (lockObj)
            {
                if (cachedItems.ContainsKey(key))
                {
                    cachedItems[key].Value = value;
                    MakeMostRecentlyUsed(cachedItems[key]);
                }
                else
                {
                    if (cachedItems.Count == this.capacity) { RemoveLeastRecentlyUsed(); }

                    CacheNode newItem = new CacheNode(key, value);

                    if (head == null)
                    {
                        head = newItem;
                        tail = newItem;
                    }
                    else { MakeMostRecentlyUsed(newItem); }
                    
                    cachedItems.Add(key, newItem);
                }
            }      
        }

        private void MakeMostRecentlyUsed(CacheNode foundItem)
        {
            if (foundItem.Next == null && foundItem.Previous == null
                    && foundItem.Key != head.Key)
                // add entry to the head when it's new
            {
                foundItem.Next = head;
                head.Previous = foundItem;
                if (head.Next == null) tail = head;
                head = foundItem;
            }
            else if (foundItem.Next == null && foundItem.Previous != null)
                // move entry from tail to the head
            {
                foundItem.Previous.Next = null;
                tail = foundItem.Previous;
                foundItem.Next = head;
                head.Previous = foundItem;
                head = foundItem;
            }
            else if (foundItem.Next != null && foundItem.Previous != null)
                // move entry from middle to the head
            {
                foundItem.Previous.Next = foundItem.Next;
                foundItem.Next.Previous = foundItem.Previous;
                foundItem.Next = head;
                head.Previous = foundItem;
                head = foundItem;
            }
        }

        private void RemoveLeastRecentlyUsed()
        {
            cachedItems.Remove(tail.Key);
            tail.Previous.Next = null;
            tail = tail.Previous;
        }
    }
}
