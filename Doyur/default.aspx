<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <style>
        a.card-link {
          text-decoration: none;
        }

        .r-card {
            
            background: #808080;
            border-radius: 5px;
            padding: 2px;
            margin: 1rem 20px;
            display: flex;
            flex-direction: row;          
            background-color: #ffffff;
            border: 1px solid #e1e1e1;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            border-radius: 3px;
            -moz-box-shadow: 0px 1px 2px 0px #eaeaea;
            -webkit-box-shadow: 0px 1px 2px 0px #eaeaea;
            box-shadow: 0px 1px 2px 0px #eaeaea;

        
        }
        .r-card img {
            width: 30rem;
            height: 10rem;
        }

        @media only screen and (max-width: 768px) {
          /* For mobile phones: */
          [class*="col-"] {
            width: 100%;
          }
        }
    </style>
    <%foreach (var restaurant in RestaurantList)
        { %>
        <a href="#" class="card-link">
            <div class="r-card">
       
                <img src="image/<%= restaurant.Image %>" />
                <div class="r-card-title">
                    <h2><%= restaurant.Name %> </h2>
                </div>
            </div>
        </a>    
    <%  } %>
    
    <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
</asp:Content>
