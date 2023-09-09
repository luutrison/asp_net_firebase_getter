using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.CACHE_PIE;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.VTO;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.SINGLE
{

    public class CHECK
    {
        private readonly IMemoryCache _memoryCache;

        private readonly HttpContext _context;

        private readonly ICachePieOption _iPieOption;
        private readonly CACHE_PIE.CACHE_PIE _iCachePie;





        public CHECK(IMemoryCache cache, HttpContext context)
        {
            _memoryCache = cache;
            _context = context;

            _iPieOption = new ICachePieOption()
            {
                MemoryCache = _memoryCache,
                Setting = SETTING.ICACHE_PIE_SETTING

            };

            _iCachePie = new CACHE_PIE.CACHE_PIE(_iPieOption);
            _context = context;
        }



        public JsonResult HEAD(Func<JsonResult> func)
        {

            VTO_CHECKED CHECKED = VTO.VTO.CHECK(new List<VTOSTATUS>(), _context);

            if (!CHECKED.isGood)
            {
                return new JsonResult(new IResponse() { check = CHECKED });
            }
            else
            {
                return func();
            }
        }

        public static SessionCard ParseOrderSession(DocumentSnapshot snap)
        {
            var item = snap.ToDictionary();

            var outItem = new SessionCard()
            {
                sessionId = item[nameof(SessionCard.sessionId)].ToString(),
            };
            return outItem;

        }

        private static async Task<bool> DeleteTimeOutCard(Task<QuerySnapshot> task)
        {
            var item = task.Result;

            var listTask = new List<Task<QuerySnapshot>>();

            foreach (var document in item.Documents)
            {
                var oitem = ParseOrderSession(document);
                var sessId = oitem.sessionId;
                var collection = FIRESTORE_METHOD.ORDER_SESSION_COLLECTION()
                    .Document(FIRESTORE_VARIBALE.FD_SESSION_UO).Collection(FIRESTORE_VARIBALE.FC_ORDER)
                    .WhereEqualTo(FIRESTORE_VARIBALE.FF_USER_SESSION_ID, sessId).GetSnapshotAsync();
                listTask.Add(collection);
            }


            Task.WaitAll(listTask.ToArray());

            foreach (var taskItem in listTask)
            {
                foreach (var itemTask in taskItem.Result.Documents)
                {
                    itemTask.Reference.DeleteAsync();
                }
            }

            return true;
        }




        /**
         * INPUT là danh sách các (key-name) của SessionCard - SessionId
         * Trong danh sách bao gồm thời gian check - danh sách tên các item
         * 
         * 
         * Định dạng item: name - bool
         * **/



        /**
         * Thêm thông tin đơn hàng
         * **/

        public void AddMoreCard(NewOrder order)
        {
            try
            {
                bool isExis = true;
                var name = CACHEKEY.CACHE_INFO_TEMP_ORDER + order.sessionCard.sessionId;
                var cache = _memoryCache.GetOrCreate(name, entrie =>
                {
                    var iCachePieObject = new ICachePieObject<PieOrder>()
                    {
                        timestamp = BANBANH_METHOD.TimeStamp().ToString(),
                        pieObject = new PieOrder()
                        {
                            sessionId = order.sessionCard.sessionId,
                            listOrder = new List<SessionOrder>()
                            {
                                order.sessionOrder
                            }
                        }
                    };

                    var time = BANBANH_METHOD.TimeStamp().ToString();

                    _iCachePie.SetICachePieStatus(new ICachePieStatus()
                    {
                        isChange = true,
                        name = CACHEKEY.CACHE_STATUS_SESSION + order.sessionCard.sessionId,
                        timestampCreate = time,
                        timestampUpdate = time
                    });
                    entrie.SetValue(iCachePieObject);
                    isExis = false;
                    return iCachePieObject;
                });

                /**
                 * Trùng đơn hàng thì tăng số lượng
                 * Nếu không trùng đơn hàng thì tăng thêm sản phẩm trong list
                 * **/



                if (isExis)
                {

                    var itemMSP = cache.pieObject.listOrder.Where(x => x.msp == order.sessionOrder.msp).FirstOrDefault();

                    if (itemMSP != null && cache.pieObject.sessionId == order.sessionCard.sessionId)
                    {
                        itemMSP.number = order.sessionOrder.number;
                    }
                    else
                    {
                        cache.pieObject.listOrder.Add(order.sessionOrder);
                    }

                    _memoryCache.Set(name, cache);

                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        /**
         * Lấy thông tin toàn bộ sản phẩm đã được order
         * 
         **/


        /***
         * Xóa thông tin đơn hàng mà người dùng không muốn đặt
         * **/

        public void DeleteCard(OrderDelete delete)
        {

            try
            {
                var name = CACHEKEY.CACHE_INFO_TEMP_ORDER + delete.sessionId;

                var card = GetCard(delete.sessionId);

                var di = card.pieObject.listOrder.Where(x => x.msp == delete.msp).FirstOrDefault();

                card.pieObject.listOrder.Remove(di);

                _memoryCache.Set(name, card);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICachePieObject<PieOrder> GetCard(string sessionId)
        {
            try
            {
                var name = CACHEKEY.CACHE_INFO_TEMP_ORDER + sessionId;
                ICachePieObject<PieOrder> item = (ICachePieObject<PieOrder>)_memoryCache.Get(name);

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /**
         * Kiểm tra xem có item nào được update, nếu có update thì thay đổi lại.
         * Thay đổi được thực hiện định kì khi có lượt truy cập ở trong khoảng thời gian đặt trước. 
         **/


        public static class CONVERTER
        {
            public static List<Dictionary<string, dynamic>> CONVER_DISIONARY<T>(List<T> content)
            {
                var listKey = new List<string>();

                foreach (var item in content[0].GetType().GetProperties())
                {
                    listKey.Add(item.Name);
                }

                var listDi = new List<Dictionary<string, dynamic>>();

                foreach (var item in content)
                {
                    var newItem = new Dictionary<string, dynamic>();

                    foreach (var key in listKey)
                    {
                        var value = item.GetType().GetProperty(key).GetValue(item, null);
                        newItem.Add(key, value);
                    }

                    listDi.Add(newItem);

                }


                return listDi;
            }
        }

      

    }
}