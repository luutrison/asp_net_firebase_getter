namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model
{
    public class ItemCheck
    {
        public string id { get; set; }

        public int timeOut { get; set; }
    }

   public class ListItemCheck
    {
        public List<ItemCheck> item { get; set; }
        public int timestamp { get; set; }
    }
}
