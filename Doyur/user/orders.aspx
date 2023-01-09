<%@ Page Title="" Language="C#" MasterPageFile="~/user/Site.Master" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="Doyur.user.orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/userorder.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <div class="box-out" style="margin-top: 60px;">
        <div class="box-top">
            Siparişlerim
        </div>
        <div class="box-in" style="margin-right:30px">
            <br />
                <asp:ListView ID="orderList" runat="server" >
                    <ItemTemplate>
                        <div class="order">
                            <div class="o-header">
                                <div class="o-header-info">
                                    Sipariş Tarihi
                                    <b class="o-title2"><%# Eval("CreateDate") %></b>
                                </div>
                                <div class="o-header-info">
                                    Sipariş Durumu
                                    <b class="o-title2"><%# Eval("Status") %></b>
                                </div>
                                <div class="o-header-info">
                                    Toplam Tutar
                                    <b class="o-title2"><%# Eval("TotalCost", "{0:0.00}") %> TL</b>
                                </div>
                                <%--<asp:HyperLink CssClass="btn-xs btn-green" ID="ordeDetailLink" runat="server">Detay</asp:HyperLink>--%>
                            </div>
                            <div class="o-list">
                                <asp:ListView class="mGrid" ID="productList" runat="server" DataSource='<%# Eval("MyOrders") %>'>
                                    <ItemTemplate>
                                        <div class="o-item">
                                            <div class="o-item-status">
                                                <img src="<%# "/image/" + Eval("Product.ImageUrl") %>" width="40" height="40" />
                                            </div>
                                            <div class="o-item-status">
                                                <span><%# Types.OrderProduct.GetOrderPStatus()[Convert.ToInt32(Eval("OPInfo.Status"))].Title %></span>
                                            </div>
                                            <div class="o-item-status">
                                                <span class= "p-name"><%# Eval("Product.Name") %></span>
                                                <span class="p-quantity">Adet: <%# Eval("OPInfo.ProductQuantity") %></span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

        </div>
    </div>

</asp:Content>
