//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Doyur.db
{
    using System;
    
    public partial class sp_GetOrderProducts_Result
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public byte ProductQuantity { get; set; }
        public int ProductId1 { get; set; }
        public int RestaurantId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public Nullable<byte> DiscountPercantage { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public int Expr1 { get; set; }
        public int UserId { get; set; }
    }
}