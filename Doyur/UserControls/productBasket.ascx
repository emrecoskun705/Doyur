<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="productBasket.ascx.cs" Inherits="Doyur.UserControls.productBasket" %>
<asp:Repeater ID="orderRepeater" runat="server" OnItemCreated="orderRepeater_ItemCreated" >
            
    <ItemTemplate>
        <hr />
        <div class="row">
            <div class="col-6">
                <h2 class="text-left" ><%# Eval("Name") %></h2>
            </div>
            <div class="col-3">
                <h3><%# String.Format("{0:0.00} x {1} = {2:0.00}", Convert.ToDecimal(Eval("Price") ), Convert.ToDecimal(Eval("ProductQuantity")), Convert.ToDecimal(Eval("Price")) * Convert.ToDecimal(Eval("ProductQuantity")))%></h3>
            </div>
            <asp:Label ID="productlbl" runat="server" Text='<%# Eval("ProductId") %>' Visible="false"></asp:Label>
            <div class="col-3">
                <asp:DropDownList  ID="quantityDropdown" runat="server" AutoPostBack="true" SelectedValue='<%# Bind("ProductQuantity") %>'
                    >
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
        
    </ItemTemplate>
</asp:Repeater>
<hr />
<div id="noOrderRecords" runat="server">
    <h2>Sepetiniz boş</h2>
    <img src="../image/basket.png" />
</div>

