<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Restaurant.aspx.cs" Inherits="Doyur.restaurant.Restaurant" %>

<%@ Register Src="~/UserControls/ProductRestaurantList.ascx" TagPrefix="uc1" TagName="ProductRestaurantList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

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
                                <uc1:ProductRestaurantList runat="server" id="ProductRestaurantList" />
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
