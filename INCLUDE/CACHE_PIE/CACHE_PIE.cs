using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Microsoft.Extensions.Caching.Memory;
using System.IO.Pipes;

/**
 * From nhà văn trẻ Đông Du - SuperManCute
 * Github: https://github.com/coder-der
 * **/


namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.CACHE_PIE
{
    /**
     * Các Model Của ICachePiece
     * 
     * **/

  


    public class CACHE_PIE
    {
        private readonly ICachePieOption _option;
        public CACHE_PIE(ICachePieOption option)
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

                var listStatus = GetICachePieStatus();

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
