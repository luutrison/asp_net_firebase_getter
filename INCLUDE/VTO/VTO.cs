using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.VTO;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.ENV;
using Microsoft.Win32;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Google.Api;
using Microsoft.AspNetCore.Mvc;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.SINGLE;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.VTO
{
   

    public static class VTO
    {

        /***
         * Hàm này để kiểm tra xem có bao nhiêu truy vấn không hợp pháp 
         * truy vấn đến các phương thức điền thông tin vào database
         * **/
        public static List<Func<List<VTOSTATUS>, HttpContext, List<VTOSTATUS>>> REGISTER()
        {
            var register = new List<Func<List<VTOSTATUS>, HttpContext, List<VTOSTATUS>>>()
            {
                IsVTO,
                IsHavenHidenKey
            };
            return register;
        }



        public static VTO_CHECKED CHECK(List<VTOSTATUS> ls, HttpContext context)
        {


            var check = new VTO_CHECKED();

            foreach (var reg in REGISTER())
            {
                var lsp = ls;
                ls = reg(lsp, context);
            }


            if (ls.Count > 0)
            {
                /***
                 * Gặp vấn đề liên quan đến xác thực truy vấn từ nguồn không xác định
                 * => đưa thông báo đến dash
                 ***/
                check.isGood = false;
            }
            else
            {
                check.isGood = true;
            }

            check.ls = ls;

            return check;
        }






        /***
         * Hàm này để kiểm tra xem có phải truy vấn đến từ địa chỉ ip và domain đã được cho phép không
         * **/

        public static List<VTOSTATUS> IsVTO(List<VTOSTATUS> ls, HttpContext context)
        {


            if (!ENV.ENV_THIS.IS_DEV())
            {
                var ip = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();


                if (!ENV_VARIBLE.GET_ENV_VARIBLE().ALLOW_IP.Contains(ip) && VTO_SETTING.IS_STATIC_IP)
                {
                    ls.Add(new VTOSTATUS { code = 400, note = "IP IS INVALID" });
                }
            }

            return ls;

        }

        /***
        * Hàm này là bước thứ hai để kiểm tra các key ẩn phía header của truy vấn
        * **/
        public static List<VTOSTATUS> IsHavenHidenKey(List<VTOSTATUS> ls, HttpContext _context)
        {
            var HI = _context.Request.Headers[SETTING.HI_HIGHT];

            if (string.Compare(HI, SETTING.REALY_HI) != 0)
            {
                _context.Response.Headers.Add("Server", "Hi Hide");
                _context.Response.StatusCode = 302;


                ls.Add(new VTOSTATUS { code = 400, note = "HI HIDEN" });
            }

            return ls;
        }

    }
}
