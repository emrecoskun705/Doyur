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

        public class TOrderResponse
        {
            public TOrderResponse(int responseId, string title)
            {
                ResponseId = responseId;
                Title = title;
            }

            public int ResponseId { get; set; }
            public string Title { get; set; }

        }


        public static List<TOrderResponse> GetOrderResponse()
        {
            List<TOrderResponse> list = new List<TOrderResponse>();
            list.Add(new TOrderResponse(0, "Not Complete"));
            list.Add(new TOrderResponse(1, "Success"));
            list.Add(new TOrderResponse(2, "Incorrect product amount"));
            list.Add(new TOrderResponse(3, "Fail"));

            return list;
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

    public class Address
    {
        public class TAddressType
        {

            public TAddressType(byte id, string name)
            {
                Id = id;
                Name = name;
            }

            public byte Id { get; set; }
            public string Name { get; set; }
        }

        public static List<TAddressType> GetAddressType()
        {
            List<TAddressType> list = new List<TAddressType>();
            list.Add(new TAddressType((byte)0, "Default"));
            list.Add(new TAddressType((byte)1, "Order"));

            return list;
        }
    }

}