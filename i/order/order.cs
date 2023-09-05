using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.method;
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

        private readonly IMemoryCache memoryCache;

        public order(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        [HttpGet]
        public JsonResult GetOrder()
        {

           string id = HttpContext.Request.Headers["Session-id"];
            var item = new CHECK(memoryCache).GetCard(id);
            return new JsonResult(item);
        }

        [HttpPost]
        public string PostOrder([FromBody]NewOrder? order)
        {
            //var request = HttpContext.Request;
            //var req = request.BodyReader.ToString();

            // NEWORDER.NEW(order.sessionOrder);

            new CHECK(memoryCache).AddMoreCard(order);

            return "item";
        }
    }




}
