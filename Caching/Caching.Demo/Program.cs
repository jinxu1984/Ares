using System;

namespace Caching.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            LRUCache cache = new LRUCache(2);   /* capacity */

            cache.Put(1, 1);
            cache.Put(2, 2);
            var result = cache.Get(1);          // returns 1
            Console.WriteLine(result);
            cache.Put(3, 3);                    // evicts key 2
            result = cache.Get(2);              // returns -1 (not found)
            Console.WriteLine(result);
            cache.Put(4, 4);                    // evicts key 1
            result = cache.Get(1);              // returns -1 (not found)
            Console.WriteLine(result);
            result = cache.Get(3);              // returns 3
            Console.WriteLine(result);
            result = cache.Get(4);              // returns 4
            Console.WriteLine(result);
            cache.Put(4, 5);                    
            result = cache.Get(4);              // returns 5 
            Console.WriteLine(result);


            Console.ReadLine();
        }
    }
}
