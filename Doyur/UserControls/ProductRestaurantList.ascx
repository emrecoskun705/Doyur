<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductRestaurantList.ascx.cs" Inherits="Doyur.UserControls.ProductRestaurantList" %>


<asp:Repeater ID="productRepeater" runat="server" OnItemCommand="productRepeater_ItemCommand">
    <ItemTemplate>
        <div class="r-card">
            <img src="../image/<%# Eval("ImageUrl") %>" />
            <div class="r-card-title"><%# Eval("Name") %></div>
            <div class="r-card-right">
                <div class=""><%# Eval("Price") %>TL</div>

                <div class="r-card-right-bottom ">
                    <asp:Button ID="AddToOrder" runat="server" Text="Ekle" 
                        BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" 
                        ForeColor="#ffffff" cssClass="btn"
                        CommandName="AddToOrderCmnd" 
                        CommandArgument='<%#Eval("ProductId")%>' 
                        />
                </div>
            </div>    
        </div>
    </ItemTemplate>
</asp:Repeater>
