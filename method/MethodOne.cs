

using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.method;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.method
{

    public class CHECK
    {
        private readonly IMemoryCache _memoryCache;

        private readonly ICachePieOption _iPieOption;
        private readonly ICachePie _iCachePie;
        public CHECK(IMemoryCache cache)
        {
            _memoryCache = cache;

            _iPieOption = new ICachePieOption()
            {
                MemoryCache = _memoryCache,
                Setting = SETTING.ICACHE_PIE_SETTING

            };

            _iCachePie = new ICachePie(_iPieOption);
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

        //public List<SessionOrder> GetSessionOrder(string sessionId)
        //{
        //    try
        //    {
        //        var collection = FIRESTORE_METHOD.ORDER_SESSION_COLLECTION()
        //            .Document(FIRESTORE_VARIBALE.FD_SESSION_UO).Collection(FIRESTORE_VARIBALE.FC_ORDER)
        //            .WhereEqualTo(FIRESTORE_VARIBALE.FF_USER_SESSION_ID, sessionId).GetSnapshotAsync();


        //        var listItem = new FirestoreParse().ListSessionOrder(collection).Result;
        //        return listItem;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public SessionCard GetSessionCard(string sessionId)
        //{
        //    var collection = FIRESTORE_METHOD.ORDER_SESSION_COLLECTION()
        //        .Document(FIRESTORE_VARIBALE.FD_SESSION_UO).Collection(FIRESTORE_VARIBALE.FC_USER)
        //        .WhereEqualTo(FIRESTORE_VARIBALE.FF_USER_SESSION_ID, sessionId).Limit(1).GetSnapshotAsync();

        //    var listItem = new FirestoreParse().ListSessionCard(collection).Result;

        //    return listItem.FirstOrDefault();

        //}

        //public ListOrder GetOrderList(string sessionId)
        //{
        //    try
        //    {
        //        var sessionCard = GetSessionCard(sessionId);
        //        var listOrder = GetSessionOrder(sessionId);

        //        var listOder = new ListOrder()
        //        {
        //            sessionCard = sessionCard,
        //            ListSessionOrder = listOrder
        //        };

        //        return listOder;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}





        /**
         * INPUT là danh sách các (key-name) của SessionCard - SessionId
         * Trong danh sách bao gồm thời gian check - danh sách tên các item
         * 
         * 
         * Định dạng item: name - bool
         * **/

        // Dùng để check xem là đã có ở trong cache chưa
        //private void CheckChange(NewOrder order)
        //{
        //    try
        //    {




        //        var cache = _memoryCache.GetOrCreate(CACHEKEY.CHECK_STEP_ONE, entrie =>
        //        {
        //            var item = new ListItemCheck()
        //            {
        //                item = new List<ItemCheck>() {
        //                 new ItemCheck()
        //                 {
        //                     id = order.sessionCard.sessionId,
        //                     timeOut = Convert.ToInt32(TimeSpan.FromHours(SETTING.DEFAULT_TIME_OUT_TO_DELETE_RECORD_ON_FIRESTORE).TotalSeconds)
        //                 }
        //                },
        //                timestamp = BANBANH_METHOD.TimeStamp()
        //            };
        //            entrie.SetValue(item);
        //            return item;
        //        });

        //        var currentTime = BANBANH_METHOD.TimeStamp();

        //        var listItemCheck = cache.item;

        //        foreach (var item in cache.item)
        //        {
        //            if (currentTime - item.timeOut > SETTING.DEFAULT_TIME_OUT_TO_DELETE_RECORD_ON_FIRESTORE)
        //            {
        //                var collection = FIRESTORE_METHOD.ORDER_SESSION_COLLECTION().Document(FIRESTORE_VARIBALE.FD_SESSION_UO).Collection(FIRESTORE_VARIBALE.FC_ORDER)
        //                                 .WhereLessThanOrEqualTo(FIRESTORE_VARIBALE.FF_ADD_CARD_TIMESTAMP, currentTime)
        //                                 .Select(new string[] { FIRESTORE_VARIBALE.FF_SESSION_ID }).GetSnapshotAsync();

        //                var delete = DeleteTimeOutCard(collection).Result;


        //                listItemCheck = listItemCheck.RemoveAll(x => x.id == item.id) as dynamic;

        //                _memoryCache.Remove(CACHEKEY.CACHE_CHECK_ITEM + order.sessionCard.sessionId);
        //            }
        //        }
        //        cache.item = listItemCheck;

        //        _memoryCache.Set(CACHEKEY.CHECK_STEP_ONE, cache);

        //    }
        //    catch (Exception ex) { }
        //}


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
                        name = CACHEKEY.CACHE_STATUS_SESSION+ order.sessionCard.sessionId,
                        timestampCreate = time,
                        timestampUpdate = time
                    });
                    entrie.SetValue(iCachePieObject);
                    isExis = false;
                    return iCachePieObject;
                });

                /**
                 * Trùng đơn hàng thì tăng số lượng
                 * **/



                if (isExis)
                {

                    var itemMSP = cache.pieObject.listOrder.Where(x => x.msp == order.sessionOrder.msp).FirstOrDefault();

                    if (itemMSP != null && cache.pieObject.sessionId == order.sessionCard.sessionId)
                    {
                        itemMSP.number += order.sessionOrder.number;
                    }
                    else
                    {
                        cache.pieObject.listOrder.Add(order.sessionOrder);
                    }
                }

                _memoryCache.Set(name, cache);

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

        //    public Task<bool> CARD()
        //    {



        //        var checkedCard = _memoryCache.GetOrCreate(CACHEKEY.CHECK_CARD, enchie =>
        //        {
        //            try
        //            {




        //                //enchie.SetValue(true);
        //                //enchie.SetAbsoluteExpiration(TimeSpan.FromSeconds(5));

        //                //var current = BANBANH_METHOD.TimeStamp();
        //                //var after = Convert.ToInt32(TimeSpan.FromHours(5).TotalSeconds);
        //                //current = current - after;

        //                //var collection = FIRESTORE_METHOD.ORDER_SESSION_COLLECTION().Document(FIRESTORE_VARIBALE.FD_SESSION_UO).Collection(FIRESTORE_VARIBALE.FC_ORDER)
        //                //.WhereLessThanOrEqualTo(FIRESTORE_VARIBALE.FF_ADD_CARD_TIMESTAMP, current)
        //                //.Select(new string[] { FIRESTORE_VARIBALE.FF_SESSION_ID }).GetSnapshotAsync();

        //                //return DeleteTimeOutCard(collection).Result;
        //            }
        //            catch (Exception err)
        //            {
        //                BANBANH_METHOD.LogsError(err.ToString());
        //                throw;
        //            }

        //        });
        //        return Task.FromResult(checkedCard);
        //    }


        //}

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

        //public static class NEWORDER
        //{
        //    public static void NEW(SessionOrder sessionOrder)
        //    {
        //        try
        //        {
        //            var db = FIRESTORE_METHOD.SESSION_USER_ORDER().Collection(FIRESTORE_VARIBALE.FC_ORDER);

        //            var dc = CONVERTER.CONVER_DISIONARY(new List<SessionOrder>() { sessionOrder });


        //            foreach (var item in dc)
        //            {
        //                //Check order before add
        //                CheckItem(item);
        //            }



        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //    }


        //    private static void CheckItem(Dictionary<string, dynamic> item)
        //    {
        //        try
        //        {
        //            var prop = new SessionOrder();
        //            var msp = item[nameof(prop.msp)] as string;
        //            var sessionId = item[nameof(prop.sessionId)] as string;
        //            var db = FIRESTORE_METHOD.SESSION_USER_ORDER().Collection(FIRESTORE_VARIBALE.FC_ORDER);


        //            var mathItem = db.Where(Filter.EqualTo(FIRESTORE_VARIBALE.FF_ORDER_MSP, msp)).Where(Filter.And(Filter.EqualTo(FIRESTORE_VARIBALE.FF_SESSION_ID, sessionId))).GetSnapshotAsync();


        //            if (mathItem.Result == null)
        //            {
        //                var added = db.AddAsync(item).Result;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }

        //    }
        //}

    }
}