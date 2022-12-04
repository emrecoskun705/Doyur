<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductRestaurantList.ascx.cs" Inherits="Doyur.UserControls.ProductRestaurantList" %>


<asp:Repeater ID="productRepeater" runat="server">
    <ItemTemplate>
        <div class="r-card">
            <img src="../image/<%# Eval("ImageUrl") %>" />
            <div class="r-card-title"><%# Eval("Name") %></div>
            <div class="r-card-right">
                <div class=""><%# Eval("Price") %>TL</div>

                <div class="r-card-right-bottom "> asd</div>
            </div>    
        </div>
    </ItemTemplate>
</asp:Repeater>
