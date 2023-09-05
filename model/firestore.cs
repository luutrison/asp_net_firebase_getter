namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model
{
    public class SessionCard
    {
        public int addCardTimestamp { get; set; }
        public string sessionId { get; set; }
        public bool? isChange { get; set; }

    }


    public class SessionOrder
    {
        public string msp { get; set; }
        public int number { get; set; }
    }

    public class NewOrder
    {
        public SessionCard sessionCard { get; set; }
        public SessionOrder sessionOrder { get; set;}

    }
    public class ListOrder
    {
        public SessionCard sessionCard { get; set;}
        public List<SessionOrder> ListSessionOrder { get; set; }
    }



    public class PieOrder
    {
        public string sessionId { get; set; }
        public  List<SessionOrder> listOrder { get; set; }
    }

}
