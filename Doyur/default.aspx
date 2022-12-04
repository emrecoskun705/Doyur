<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <%foreach (var restaurant in RestaurantList)
        { %>
        <a href="/restaurant.aspx?id=<%= restaurant.RestaurantId %>" class="card-link">
            <div class="r-card">
       
                <img src="image/<%= restaurant.Image %>" />
                <div class="r-card-title">
                    <h2><%= restaurant.Name %> </h2>
                </div>
            </div>
        </a>    
    <%  } %>
    
</asp:Content>
