using System;
using System.Collections.Generic;
using System.Text;

namespace Caching
{
    public class CacheNode
    {
        public int Key { get; set; }

        public int Value { get; set; }

        public CacheNode Next { get; set; }

        public CacheNode Previous { get; set; }

        public CacheNode(int key, int value)
        {
            Key = key;
            Value = value;
            Next = null;
            Previous = null;
        }
    }
}
