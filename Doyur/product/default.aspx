<%@ Page Title="" Language="C#" MasterPageFile="~/product/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur.product._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        main {
            display: block;
        }

        #product-main .product-d-container {
            width: 1200px;
            margin: 20px auto;
        }

        #product-main .flex-container {
            display: flex;
        }

        #product-main .product-container {
            display: block;
            width: 956px;
            border: solid 1px #e6e6e6;
            background-color: #fff;
            box-shadow: 0 1px 4px #0000000d;
            border-radius: 6px;
            margin-bottom: 30px;
            padding: 20px;
            box-sizing: border-box;
        }

        #product-main .image-container {
            width: 400px;
            position: relative;
        }

        #product-main .image-container .product-image-container {
            width: 100%;
            height: 400px;
            position: relative;
        }

        #product-main .image-container div {
            height: 100%;
        }

        #product-main .absolute {
            position: absolute;
        }

        #product-main .right-content {
            margin: 0 5px 0 25px;
            width: 53%;
        }

        #product-main .product-container>div {
            display: flex;
        }

        #product-main .product-container .p-data>div>div:last-child {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
        }

        .bottom-btn {
        }

    </style>

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
                            <h1><%= Product.Name %></h1>
                            <h3> Son <%= Product.Stock %> ürün</h3><br /><br /><br />
                            <h2><%= Product.Price %> TL</h2>
                            <br /><br /><br /><br /><br />
                            <asp:Button ID="orderBtn" runat="server" Text="Sepete Ekle" CssClass="btn"  BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" />
                        </div>
                    </div>
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
