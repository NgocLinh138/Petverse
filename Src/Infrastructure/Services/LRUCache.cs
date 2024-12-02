namespace BHEP.Infrastructure.Redis.Service;
public class LRUCache<TKey, TValue>
{
    private readonly int capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheItem>> cacheMap;
    private readonly LinkedList<CacheItem> queue;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        cacheMap = new Dictionary<TKey, LinkedListNode<CacheItem>>(capacity);
        queue = new LinkedList<CacheItem>();
    }

    public TValue Get(TKey key)
    {
        if (cacheMap.TryGetValue(key, out var node))
        {
            // Move accessed item to the front of the LRU list
            queue.Remove(node);
            queue.AddFirst(node);
            return node.Value.Value;
        }

        return default;
    }

    public void Put(TKey key, TValue value)
    {
        if (cacheMap.TryGetValue(key, out var node))
        {
            // Update existing item and move it to the front of the LRU list
            node.Value.Value = value;
            queue.Remove(node);
            queue.AddFirst(node);
        }
        else
        {
            if (cacheMap.Count >= capacity)
            {
                // Remove the least recently used item
                var lruItem = queue.Last;
                if (lruItem != null)
                {
                    cacheMap.Remove(lruItem.Value.Key);
                    queue.RemoveLast();
                }
            }

            // Add new item to the cache
            var cahceItem = new CacheItem { Key = key, Value = value };
            var newNode = new LinkedListNode<CacheItem>(cahceItem);
            queue.AddFirst(cahceItem);
            cacheMap[key] = newNode;
        }
    }

    private class CacheItem
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}

