using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.VTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

    public class OrderDelete
    {
        public string msp { get; set; }
        public string sessionId { get; set; }
    }

    public class IResponse
    {
        public string? response { get; set; }
        public VTO_CHECKED? check { get; set; }
    }


    public class OrderLsAdd
    {
        public int number { get; set; }
        public string msp { get; set; }
    }

    public class AddOrder
    {
        [Required(ErrorMessage = "Name is empty")]
        [MinLength(1, ErrorMessage = "MinLength is invalid")]
        [MaxLength(50, ErrorMessage = "MaxLength is invalid")]
        public string name { get; set; }
        [Required(ErrorMessage = "Phonenumber is empty")]
        [Phone(ErrorMessage = "Phonenumber is invalid")]
        [MaxLength(20, ErrorMessage = "MaxLength is invalid")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Address is empty")]
        [MinLength(1, ErrorMessage = "MinLength is invalid")]
        public string address { get; set; }
        [Required(ErrorMessage = "Orderls is invalid")]
        public List<OrderLsAdd> orderls { get; set; }

    }

}
