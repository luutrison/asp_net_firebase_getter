using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.SINGLE;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.i.order
{

    [Route("i/order/[controller]/[action]")]
    public class order : ControllerBase
    {

        private readonly IMemoryCache _memoryCache;

        public order(IMemoryCache memoryCache)
        {
            try
            {
                _memoryCache = memoryCache;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        public JsonResult GetOrder()
        {
            try
            {
                CHECK ICHECK = new CHECK(_memoryCache, HttpContext);

                return ICHECK.HEAD(() =>
                {
                    string id = HttpContext.Request.Headers["Session-id"];
                    var item = ICHECK.GetCard(id);
                    return new JsonResult(new IResponse()
                    {
                        response = JsonConvert.SerializeObject(item)
                    });
                });

            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public JsonResult PostOrder([FromBody] NewOrder? order)
        {
            try
            {
                CHECK ICHECK = new CHECK(_memoryCache, HttpContext);


                return ICHECK.HEAD(() =>
                {
                    ICHECK.AddMoreCard(order);
                    return IRESPONSE.DEFAULT_RESPONSE;
                });
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult DeleteOrder([FromBody] OrderDelete odl)
        {
            try
            {
                CHECK ICHECK = new CHECK(_memoryCache, HttpContext);

                return ICHECK.HEAD(() =>
                {
                    ICHECK.DeleteCard(odl);
                    return IRESPONSE.DEFAULT_RESPONSE;
                });
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult AddOrder([FromBody] AddOrder order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CHECK ICHECK = new CHECK(_memoryCache, HttpContext);

                    return ICHECK.HEAD(() =>
                    {


                        var dicOrderls = new List<Dictionary<string, object>>();

                        foreach (var item in order.orderls)
                        {
                            dicOrderls.Add(new Dictionary<string, object>() { { "number", item.number }, { "msp", item.msp } });
                        }

                        var dic = new Dictionary<string, object>()
                        {
                            {"name", order.name},
                            {"orderls", dicOrderls},
                            {"phoneNumber", order.phoneNumber},
                            {"address", order.address},
                        };

                        var db = FIRESTORE_METHOD.ORDER_ADDED_COLLECTION().AddAsync(dic).Result;

                        if (db != null)
                        {
                            return IRESPONSE.DEFAULT_RESPONSE;

                        }
                        else
                        {
                            return IRESPONSE.BAD_RESPONSE;

                        }

                    });


                }
                else
                {
                    return IRESPONSE.BAD_RESPONSE;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }




}
