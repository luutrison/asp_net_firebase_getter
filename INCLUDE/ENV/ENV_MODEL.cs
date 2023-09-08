namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.ENV
{
    public static class ENV_MODEL
    {
        public static readonly List<string> DEV_LS_IP = new List<string>() { "127.0.0.0" };

        public static readonly List<string> PRO_LS_IP = new List<string>() { "127.0.0.0" };
      
        public static readonly string CURRENT_ENV = "ENV_DEV";
        public static readonly string ENV_DEV = "ENV_DEV";
        public static readonly string ENV_PRO = "ENV_PRO";
    }
}
