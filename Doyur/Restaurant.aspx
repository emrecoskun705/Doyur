<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Restaurant.aspx.cs" Inherits="Doyur.restaurant.Restaurant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

        <style>
            * {
            box-sizing: border-box;
            }


            .r-card-title {
                font-size: 2.4vh;
                padding: 0px 2px;
            }


            .r-card-right {
                margin-left: auto;
                font-size: 2vh;
                font-weight: bold;
                padding: 0px;
                position: relative;
            }

            .r-card-right-bottom {
                position: absolute;
                  bottom: 0;
            }
            .pl-0 {
              padding-left: 0px;  
            }

            .basket-panel {
                border-radius: 5px;
                background-color: #ffffff;
                border: 1px solid #ff6a00;
                -moz-border-radius: 3px;
                -webkit-border-radius: 3px;
                border-radius: 3px;
                -moz-box-shadow: 0px 1px 2px 0px #eaeaea;
                -webkit-box-shadow: 0px 1px 2px 0px #eaeaea;
                box-shadow: 0px 1px 2px 0px #eaeaea;
            }

        </style>

        <div>

        

        <div class="r-card">
       
            <img src="image/<%= RestaurantObj.Image %>" />
            <div class="r-card-title">
                <h2><%= RestaurantObj.Name %> </h2>
            </div>
        </div>

        <article>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-8 col-md-9 col-sm-12 pl-0">
                        <div class="content-section">
                        <% foreach (var product in ProductList)
                            { %>     
                                <div class="r-card">
                                    <img src="../image/<%= product.ImageUrl %>" />
                                    <div class="r-card-title"><%= product.Name %></div>
                                    <div class="r-card-right">
                                        <div class=""><%= product.Price %>TL</div>

                                        <div class="r-card-right-bottom "> asd</div>
                                    </div>    
                                </div>

                        <%  } %>
                         </div>
                    </div>

                    <div class="col-lg-4 col-md-3">
                        <div class="sidebar-section sticky">
                            <div class="sidebar-item"> 
                                <div class="sidebar-content">
                                    <div class="basket-panel">
                                    <h1>Sipariş vermek için sepetinize ürün ekleyin.</h1>
                                    <img src="image/basket.png" />

                                    </div>
                              </div>
                            </div>
                        </div>
                    </div>

        </div>
            </div>
        </article>
        </div>       
</asp:Content>
