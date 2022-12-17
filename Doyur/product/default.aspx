<%@ Page Title="" Language="C#" MasterPageFile="~/product/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur.product._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/product.css" rel="stylesheet" />
    <link href="/js/ckeditor/contents.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="product-main">
        <div class="product-d-container">
            <div class="flex-container">
                <div class="product-container">
                    <div>
                        <div>
                            <div class="image-container">
                                <div class="product-image-container">
                                    <img class="absolute" width="500" height="400" src="/image/<%= Product.ImageUrl %>"/>
                                </div>
                            </div>
                        </div>
                        <div style="margin-left:120px">
                            <h1 style="font-weight: bold; font-size: 25px;"><%= Product.Name %></h1>
                            <h3 style="font-size: 15px;">Satıcı: <a href="#"><%= Company.Name %></a></h3>
                            <h3> Son <%= Product.Stock %> ürün</h3><br /><br /><br />
                            <h2 style="font-size: 25px; font-weight: bold;"><%= Product.Price.ToString("#.##") %> TL</h2>
                            <br /><br /><br /><hr /><br /><br />
                            <asp:Button ID="orderBtn" runat="server" Width="100%" Text="Sepete Ekle" CssClass="btn"  BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" />
                        </div>
                    </div>
                </div>
                <div>

                </div>
            </div>
            <table>
                <tbody>
                    <asp:Repeater ID="parentR" runat="server" OnItemDataBound="parentR_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <h3 style="font-weight: bold;"><%# Eval("Name") %></h3>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Repeater ID="childR" runat="server">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cBoxId" Text='<%# Eval("Name") %>' runat="server" Enabled="false" Checked='<%# SelectedFeatureIds.Contains(Convert.ToInt32(Eval("FeatureId"))) ? true : false%>' />
                                            <asp:HiddenField ID="hdnId" Value='<%# Eval("FeatureId") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <br /><br />
            <div class="description-border">
                <%= Product.Description %>
            </div>
            
        </div>

    </main>

<%--        <div class="row r-product">
            <div class="col">
                <img src="/image/<%= Product.ImageUrl %>"/>
            </div>
            <div class="col">
                <h2><%= Product.Name %></h2>
            </div>
            <div class="col">
                <div class="row">
                    <div class="col"></div>
                    <div class="col text-right"><%= Product.Price %>TL</div>
                </div>
                <div class="row">
                    <div class="col"></div>
                    <div class="col text-right align-bottom">
                        <asp:Button ID="addOrderBtn" runat="server" Text="Ekle" 
                        BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" 
                        ForeColor="#ffffff" cssClass="btn"
                        CommandName="AddToOrderCmnd" 
                        CommandArgument='<%= Product.ProductId%>' 
                        />

                    </div>
                </div>
            </div>
            
        </div>--%>
</asp:Content>
