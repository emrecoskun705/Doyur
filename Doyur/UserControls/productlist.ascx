<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="productlist.ascx.cs" Inherits="Doyur.UserControls.RestaurantList" %>

<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
    <a href="">
        <main class="grid">
            <article>
                <img src="/image/<%# Eval("ImageUrl") %>" />
                <div class="text">
                    <h2><%#Eval("Name") %> </h2>
                </div>
            </article>

        </main>
    </a>
    </ItemTemplate>
</asp:Repeater>
