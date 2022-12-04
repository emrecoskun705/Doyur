<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RestaurantList.ascx.cs" Inherits="Doyur.UserControls.RestaurantList" %>

<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <a href="/restaurant.aspx?id=<%# Eval("RestaurantId")%>" class="card-link">
            <div class="r-card">
       
                <img src="image/<%# Eval("Image") %>" />
                <div class="r-card-title">
                    <h2><%#Eval("Name") %> </h2>
                </div>
            </div>
        </a>  
    </ItemTemplate>
</asp:Repeater>
