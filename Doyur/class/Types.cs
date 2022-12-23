using System.Collections.Generic;
using static Types.Order;

namespace Types
{
    public class Order
    {
        public class TOrderStatus
        {
            public TOrderStatus(byte id, string title) {
                StatusId= id;
                Title= title;
            }
            public byte StatusId { get; set; }
            public string Title { get; set; }
        }

        public static List<TOrderStatus> GetOrderStatus()
        {
            List<TOrderStatus> list = new List<TOrderStatus>();
            list.Add(new TOrderStatus((byte)0, "Created"));
            list.Add(new TOrderStatus((byte)1, "Paid"));
            list.Add(new TOrderStatus((byte)2, "Delivered"));

            return list;
        }

    }

    public class OrderProduct
    {
        public class TOrderPStatus
        {
            public TOrderPStatus(byte id, string title)
            {
                StatusId = id;
                Title = title;
            }
            public byte StatusId { get; set; }
            public string Title { get; set; }
        }

        public static List<TOrderPStatus> GetOrderPStatus()
        {
            List<TOrderPStatus> list = new List<TOrderPStatus>();
            list.Add(new TOrderPStatus((byte)0, "Added"));
            list.Add(new TOrderPStatus((byte)1, "Paid"));
            list.Add(new TOrderPStatus((byte)2, "Delivered"));

            return list;
        }
    }

}