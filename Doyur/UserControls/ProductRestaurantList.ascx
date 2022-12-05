<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductRestaurantList.ascx.cs" Inherits="Doyur.UserControls.ProductRestaurantList" %>


<asp:Repeater ID="productRepeater" OnItemDataBound="productRepeater_ItemDataBound" runat="server" OnItemCommand="productRepeater_ItemCommand">
    <ItemTemplate>

        <div class="row r-product">
            <div class="col">
                <img src="/image/<%# Eval("ImageUrl") %>"/>
            </div>
            <div class="col">
                <h2><%# Eval("Name") %></h2>
            </div>
            <div class="col">
                <div class="row">
                    <div class="col"></div>
                    <div class="col text-right"><%# Eval("Price", "{0:0.00}") %>TL</div>
                </div>
                <div class="row">
                    <div class="col"></div>
                    <div class="col text-right align-bottom">
                        <asp:Button ID="addOrderBtn" runat="server" Text="Ekle" 
                        BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" 
                        ForeColor="#ffffff" cssClass="btn"
                        CommandName="AddToOrderCmnd" 
                        CommandArgument='<%#Eval("ProductId")%>' 
                        />

                    </div>
                </div>
            </div>
            
        </div>
    </ItemTemplate>
</asp:Repeater>

<script src="/js/alert.js"></script>
