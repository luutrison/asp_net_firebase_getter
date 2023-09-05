using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Microsoft.Extensions.Caching.Memory;
using System.IO.Pipes;

/**
 * From nhà văn trẻ Đông Du - SuperManCute
 * Github: https://github.com/coder-der
 * **/


namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.method
{
    /**
     * Các Model Của ICachePiece
     * 
     * **/

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


    public class ICachePie
    {
        private readonly ICachePieOption _option;
        public ICachePie(ICachePieOption option)
        {
            _option = option;
        }

        //Phương thức dùng để kiểm tra xem là đã có item nào hết hạn chưa
        //Nếu mà có thì xóa rồi xóa luôn ở db






        /**
         * Phương thức dùng để xác định xem là item đấy có thay đổi chưa, nếu có thì update luôn ở db
         * **/


        public List<ICachePieStatus> GetICachePieStatus()
        {
            try
            {
                var ls = _option.MemoryCache.GetOrCreate(_option.Setting.iCachePieName, entrie =>
                {
                    var statusls = new List<ICachePieStatus>();
                    entrie.SetValue(statusls);
                    return statusls;
                });

                return ls;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool isNull(dynamic ob)
        {
            var isNull = true;
            try
            {
                if (ob != null)
                {
                    isNull = false;
                }
                return isNull;
            }
            catch (Exception)
            {
                return isNull;
            }
           
        }

        public void SetICachePieStatus(ICachePieStatus cachePieStatus)
        {
            try
            {

                var listStatus = (List<ICachePieStatus>)GetICachePieStatus();
             
                var status = listStatus.Where(x => x.name == cachePieStatus.name).FirstOrDefault();


                if (isNull(status))
                {
                     listStatus.Add(cachePieStatus);
                }
                else
                {
                    listStatus.Remove(status);
                    listStatus.Add(cachePieStatus as dynamic);
                }

                _option.MemoryCache.Set(_option.Setting.iCachePieName, listStatus);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
