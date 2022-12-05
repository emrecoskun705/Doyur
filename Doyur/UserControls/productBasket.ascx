<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="productBasket.ascx.cs" Inherits="Doyur.UserControls.productBasket" %>


<asp:Repeater ID="orderRepeater" OnItemCreated="orderRepeater_ItemCreated" runat="server">
    <ItemTemplate>
        <hr />
        <div class="row">
            <div class="col-6">
                <h2 class="text-left" ><%# Eval("Name") %></h2>
            </div>
            <div class="col-3">
                <h3><%# Eval("Price") %></h3>
            </div>
            <div class="col-3">
                <asp:DropDownList ID="DropDownList1" runat="server">
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
