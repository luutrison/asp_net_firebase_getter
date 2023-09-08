using Microsoft.Extensions.Caching.Memory;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.CACHE_PIE
{
    public class ICachePieObject<T>
    {
        public string timestamp { get; set; }
        public T pieObject { get; set; }

    }

    public class ICachePieSetting
    {
        public string iCachePieName { get; set; }
    }

    public class ICachePieStatus
    {
        public bool isChange { get; set; }
        public string name { get; set; }
        public string timestampUpdate { get; set; }
        public string timestampCreate { get; set; }
    }

    public class ICachePieOption
    {
        public IMemoryCache MemoryCache { get; set; }
        public ICachePieSetting Setting { get; set; }

    }
}
