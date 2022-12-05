<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/SiteRestaurant.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur.restaurant._default" %>

<%@ Register Src="~/UserControls/ProductRestaurantList.ascx" TagPrefix="uc1" TagName="ProductRestaurantList" %>

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
                            <div class="content-section">
                                <uc1:ProductRestaurantList runat="server" ID="ProductRestaurantList" />
                             </div>
                        </div>

                        <div class="col-lg-4 col-md-3">
                            <div class="sidebar-section sticky">
                                <div class="sidebar-item"> 
                                    <div class="sidebar-content">
                                        <div class="basket-panel">
                                            <h1>Sipariş Listesi</h1>
                                            <div class="row">
                                                <div class="col-6">
                                                    <h2>
                                                        Bol malzemeli pizza
                                                    </h2>
                                                </div>
                                                <div class="col-3">
                                                    <h3>
                                                        14.56TL
                                                    </h3>
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
