<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/SiteRestaurant.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur.restaurant._default" viewStateEncryptionMode="Never" %>

<%@ Register Src="~/UserControls/ProductRestaurantList.ascx" TagPrefix="uc1" TagName="ProductRestaurantList" %>
<%@ Register Src="~/UserControls/productBasket.ascx" TagPrefix="uc1" TagName="productBasket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
            <div class="r-card">
                <div class="headImg">
                    <img src="/image/<%= RestaurantObj.Image %>" class="headImg"  />
                </div>
                <div class="r-card-title">
                    <h2><%= RestaurantObj.Name %> </h2>
                </div>
                <div class="r-card-right">
                    rating part
                </div>
            </div>

            <article>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-8 col-md-9 col-sm-12 pl-0">
                            <div>
                                <uc1:ProductRestaurantList runat="server" ID="ProductRestaurantList" />
                             </div>
                        </div>

                        <div class="col-lg-4 col-md-3">
                            <div class="sidebar-section sticky">
                                <div class="sidebar-item"> 
                                    <div class="sidebar-content">
                                        <div class="basket-panel">
                                            <h1>Sipariş Listesi</h1>
                                            <uc1:productBasket runat="server" id="productBasket" AutoPostBack="true" />
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
