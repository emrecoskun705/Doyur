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



    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <div id="basket-app-container">
        <div id="pb-container">
            <div class="pb-wrapper">
                <div class="pb-header-wrapper">
                    <h1>Sepetim 3 ürün</h1>
                </div>
                <div class="pb-merchant-group">
                    <div class="pb-merchant">
                        <div class="pb-merchant-header">
                            <div class="pb-merchant-info">
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <div>
                                    Satıcı: Monster
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="pb-basket-item-wrapper">
                            <div class="pb-basket-item">
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                <img width="50" src="../image/pizza.jpg" style="margin-left: 10px;" />
                                <a href="#" style="margin-left: 10px;"  class="pb-basket-item-details"> Monster Abra Notebook 16GB RAMB 512GB SSD</a>
                                <div class="pb-basket-item-actions">
                                    <h3>18799,43 TL</h3>
                                    <img src="../image/icons8-trash-can-16.png" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
