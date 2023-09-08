namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.VTO
{
    public class VTOSTATUS
    {
        public int code { get; set; }
        public string note { get; set; }
    }

    public class VTO_CHECKED
    {
        public bool isGood { get; set; }

        public List<VTOSTATUS> ls { get; set; }
    }
}
