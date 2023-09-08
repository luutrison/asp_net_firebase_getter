using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.CACHE_PIE;
using Microsoft.AspNetCore.Mvc;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model
{
    public static class SETTING
    {
        public static readonly string PATH_CREDENTIALS = "./config/google-api/coder-writer-be0810473956.json";
        public static readonly string GOOGLE_APPLICATION_CREDENTIALS = "GOOGLE_APPLICATION_CREDENTIALS";
        public static readonly string FIRESTORE_PROJECT_ID = "coder-writer";
        public static readonly string ERROR_LOGS_PATH = "./err_log.txt";
        public static readonly int MAX_ERROR_LOGS_SIZE = 3 * 1024 * 1024;
        public static readonly int DEFAULT_TIME_OUT_TO_DELETE_RECORD_ON_FIRESTORE = 8;

        public static readonly string HI_HIGHT = "HI-HIGHT-HIDE";
        public static readonly string REALY_HI = "22-2-222-22222222-2-222-2222";

        public static readonly ICachePieSetting ICACHE_PIE_SETTING = new ICachePieSetting()
        {
            iCachePieName = "CACHE_PIE_"
        };
    }

    public static class IRESPONSE
    {
        public static readonly int GET_HIGHT_FAIL = 999;

        public static readonly int GET_HIGHT_FEEL = 666;

        public static readonly JsonResult DEFAULT_RESPONSE = new JsonResult(new IResponse()
        {
            check = new INCLUDE.VTO.VTO_CHECKED()
            {
                isGood = true,
            }
        });
        public static readonly JsonResult BAD_RESPONSE = new JsonResult(new IResponse()
        {
            check = new INCLUDE.VTO.VTO_CHECKED()
            {
                isGood = false,
            }
        });
    }

    public static class IMESSAGE
    {
        public static readonly string NON_HIGHT = "A-NON-HIGHT";
        public static readonly string TWO_HIGHT = "TWO-HIGHT";
    }

    public static class FIRESTORE_VARIBALE
    {
        public static readonly string FC_ORDER_NC = "BANBANH_ORDER_NOT_CASH";
        public static readonly string BAN_BANH_ORDER = "BAN_BANH_ORDER";
        public static readonly string FD_SESSION_UO = "SESSION_USER_ORDER";
        public static readonly string FC_ORDER = "ORDER";
        public static readonly string FC_USER = "USER";
        public static readonly string FF_ADD_CARD_TIMESTAMP = "addCardTimestamp";
        public static readonly string FF_USER_SESSION_ID = "sessionId";
        public static readonly string FF_SESSION_ID = "sessionId";
        public static readonly string FF_ORDER_MSP = "msp";

    }

    public static class CACHEKEY
    {
        public static readonly string GALLERY_IMAGE = "gallery_image";
        public static readonly string SAME_PRODUCT = "same_product";
        public static readonly string CHECK_CARD = "check_card";
        public static string CACHE_INFO_TEMP_ORDER = "CACHE__";


        public static string CACHE_STATUS_SESSION = "cache_ss_";
        public static string CHECK_STEP_ONE = "cak_step_one";
    }
}
