<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur.order._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #basket-app-container {
            display: flex;
            justify-content: center;
            margin: 20px 0;
            position: relative;
            min-height: 600px;
        }

        #basket-app-container #pb-container {
            width: 1200px;
            display: flex;
            justify-content: space-between;
        }

        .pb-wrapper {
            font-size: 12px;
            font-family: source_sans_proregular;
            width: 928px;
        }

        .pb-wrapper .pb-header-wrapper {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
        }

        .pb-merchant-group {
            display: flex;
            flex-direction: column;
            background-color: #fff;
            border: 1px solid #e2e2e2;
            border-radius: 6px;
            margin-bottom: 15px;
            box-shadow: 0 1px 4px 0 rgb(0 0 0 / 5%);
        }

        .pb-merchant-group .pb-merchant {
            display: flex;
            flex-direction: column;
            padding: 15px;
            padding-left: 20px;
            justify-content: space-between;
            background-color: #fafafa;
            border-bottom: 1px solid #e2e2e2;
        }

        .pb-merchant-group .pb-merchant-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .pb-merchant-group .pb-merchant-header .pb-merchant-info {
            display: flex;
            justify-content: space-between;
        }

        .pb-basket-item-wrapper {
            display: flex;
            flex-flow: row wrap;
            align-items: center;
            border-bottom: 1px solid #e2e2e2;
            padding: 20px;
        }

        .pb-basket-item-wrapper .pb-basket-item {
            display: flex;
            align-items: center;
            flex: 1;
        }

        .pb-basket-item-wrapper .pb-basket-item:last-child {
            border-bottom: none;
        }

        .pb-basket-item-wrapper .pb-basket-item {
            display: flex;
            align-items: center;
            flex: 1;
        }

        .pb-basket-item-wrapper .pb-basket-item-actions {
            display: flex;
            flex: 1;
            justify-content: space-between;
            align-items: center;
        }

        .pb-basket-item-wrapper .pb-basket-item-details {
            display: flex;
            flex-direction: column;
            width: 495px;
            margin-left: 5px;
            margin-right: 15px;
        }

        #basket-app-container #pb-container .sticky {
            top: 20px;
            position: -webkit-sticky;
            position: sticky;
            align-self: flex-start;
        }
        #basket-app-container .pb-summary {
            margin-left: 20px;
        }

        #basket-app-container .pb-summary .pb-summary-box {
            width: 100%;
            padding: 20px;
            border-radius: 6px;
            box-shadow: 0 1px 4px 0 rgb(0 0 0 / 5%);
            border: solid 1px #e6e6e6;
            box-sizing: border-box;
            background-color: #ffffff;
            line-height: 24px;
        }

        #basket-app-container .pb-summary .pb-summary-box .pb-summary-box-prices li {
            display: flex;
            justify-content: space-between;
        }

        .numeric-counter {
            box-sizing: border-box;
            width: 90px;
            display: flex;
        }

        .numeric-counter .numeric-counter-button {
            border: 1px solid #e6e6e6;
            background-color: #fafafa;
            width: 24px;
            height: 33px;
            outline: none;
            display: flex;
            justify-content: center;
            align-items: center;
            cursor: pointer;
            transition: 0.3s;
        }

        .numeric-counter .counter-content {
            width: 40px;
            height: 33px;
            display: flex;
            justify-content: center;
            align-items: center;
            border: 1px solid #e6e6e6;
            color: #4a4a4a;
            background-color: #ffffff;
            outline: none;
            text-align: center;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <div id="basket-app-container">
        <div id="pb-container">
            <div class="pb-wrapper">
                <div class="pb-header-wrapper">
                    <h1>Sepetim</h1>
                </div>
                <asp:Repeater ID="parentR" runat="server" OnItemDataBound="parentR_ItemDataBound">
                    <ItemTemplate>
                        <div class="pb-merchant-group">
                            <div class="pb-merchant">
                                <div class="pb-merchant-header">
                                    <div class="pb-merchant-info">
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                        <asp:Label ID="CName" Font-Bold="true" runat="server" Text='<%# Eval("CName") %>'></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <asp:Repeater ID="childR" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <div class="pb-basket-item-wrapper">
                                            <div class="pb-basket-item">
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                                <img width="50" src="../image/<%# Eval("ImageUrl") %>" style="margin-left: 10px;" />
                                                <a href="/product/default.aspx?id=<%# Eval("ProductId") %>" style="margin-left: 10px;"  class="pb-basket-item-details"> <%#Eval("Name") %></a>
                                                <div class="pb-basket-item-actions">
                                                    <div class="numeric-counter">
                                                        <asp:Button Enabled='<%# Convert.ToInt32(Eval("ProductQuantity")) == 1 ? false : true %>' CssClass="numeric-counter-button" ID="decrementbtn" runat="server" Text="-"  OnClick="decrementbtn_Click"/>
                                                        <asp:Label CssClass="counter-content" ID="quantityId" runat="server" Text='<%# Eval("ProductQuantity") %>'></asp:Label>
                                                        <asp:Button Enabled='<%# Convert.ToInt32(Eval("Stock")) == Convert.ToInt32(Eval("ProductQuantity")) ? false : true %>' CssClass="numeric-counter-button" ID="incrementbtn" runat="server" Text="+" OnClick="incrementbtn_Click" />
                                                    </div>
                                                    <h3><%# Eval("Price", "{0:0.00}") %> TL</h3>
                                                    <asp:HiddenField ID="ProductId" Value='<%# Eval("ProductId") %>' runat="server" />
                                                    <asp:ImageButton ImageUrl="../image/icons8-trash-can-16.png" ID="trashBtn" runat="server" OnClick="trashBtn_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <aside class="sticky" style="margin-top:40px;">
                <div class="pb-summary" style="width: 250px;">
                    <div class="pb-summary-box">
                        <h1>Sipariş Özeti</h1>
                        <ul class="pb-summary-box-prices">
                            <li>
                                <span>Ürün Toplamı</span>
                                <strong><%= TotalPrice.ToString("#.##") %> TL</strong>
                            </li>
<%--                            <li>
                                <span>İndirim</span>
                                <strong>39TL</strong>
                            </li>--%>
                        </ul>
                        <hr />
                        <p style="text-align: right;"><%= TotalPrice.ToString("#.##") %> TL</p>
                    </div>
                    <div class="pb-coupon" style="margin-bottom: 10px;">
                        <div>
                            <h2>Kupon:</h2>
                            <asp:TextBox  ID="TextBox1" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/order/payment.aspx" runat="server" CssClass="btn" Width="100%"  BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff">Sepeti Onayla</asp:HyperLink>
                </div>
            </aside>
        </div>
    </div>
</asp:Content>
