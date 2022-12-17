<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="productlist.ascx.cs" Inherits="Doyur.UserControls.RestaurantList" %>
<main class="grid">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
        <a href="/product/default.aspx?id=<%# Eval("ProductId") %>"">
            <article>
                <img src="/image/<%# Eval("ImageUrl") %>" />
                <div class="text">
                    <h2 style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden;"><%#Eval("Name") %> </h2>
                </div>
            </article>      
        </a>
        </ItemTemplate>
    </asp:Repeater>
</main>
