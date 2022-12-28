<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="Doyur.order.payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .box-address {
            max-width: 1200px;
            padding: 10px;
            margin: 0 auto;
        }

        .sticky {
            top: 20px;
            position: -webkit-sticky;
            position: sticky;
            align-self: flex-start;
        }

        .pb-summary {
            margin-left: 20px;
        }

        .pb-summary-box {
            width: 100%;
            padding: 20px;
            border-radius: 6px;
            box-shadow: 0 1px 4px 0 rgb(0 0 0 / 5%);
            border: solid 1px #e6e6e6;
            box-sizing: border-box;
            background-color: #ffffff;
            line-height: 24px;
        }
        
        .pb-summary span {
            display: inline-block;
            max-width: 130px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .pb-summary-box-prices li {
            display: flex;
            justify-content: space-between;
        }

        ul {
            padding-inline-start: 0;
        }

        .pb-summary ul>li>p {
            display: flex;
            justify-content: space-between;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <div class="row">
    <div class="col-lg-9 col-sm-12">

    <div class="box-out box box-address" style="margin-top: 60px;">
        <div class="box-top">
            Lütfen adres seçin
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="divr">
                <asp:HyperLink CssClass="btn-xs platin-b" NavigateUrl="~/user/createaddress?redirect=payment" ID="addrLink" runat="server">Adres Ekle</asp:HyperLink>
            </div>
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gList_RowCommand">
                
                <Columns> 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="isChecked" runat="server" Checked='<%# ((bool)Eval("IsActive")) ? true : false %>' />
                            <asp:HiddenField ID="addressId" runat="server" Value='<%# Eval("AddressId") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Adres Adı" DataField="Name" />
                    <asp:BoundField HeaderText="İl" DataField="Town" />
                    <asp:BoundField HeaderText="İlçe" DataField="District" />
                    <asp:BoundField HeaderText="Telefon" DataField="Phone" />
                    <asp:TemplateField HeaderText="Aktif" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <span id="dsf" class='<%# ((bool)Eval("IsActive")) ? "st-active" : "st-notactive" %>' runat="server">
                                <%# ((bool)Eval("IsActive")) ? "Aktif" : "Pasif" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Düzenle" CssClass="btn-xs btn-green" CommandName="Edit" CommandArgument='<%# Eval("AddressId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div>
    <div class="box-out box box-address">
        <div class="box-top">
            Ödeme Yöntemi
        </div>
        <div class="box-in">
            <h2>Burada kart bilgileri girilecek</h2>
            <h2>Burada kart bilgileri girilecek</h2>
            <h2>Burada kart bilgileri girilecek</h2>
        </div>
    </div>
    <div class="box-bottom"></div>

    </div>
    <div class="col-lg-3">
        <aside class="sticky" style="margin-top:40px;">
            <div class="pb-summary" style="width: 250px;">
                <div class="pb-summary-box">
                    <h1>Sipariş Özeti</h1>
                    <ul class="pb-summary-box-prices">
                        <li>
                            <span>Ödenecek Tutar</span>
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
                <br />
                <asp:Button ID="payBtn" OnClick="payBtn_Click" Text="Ödeme Yap" runat="server" CssClass="btn" Width="100%"  BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" />
            </div>
        </aside>
    </div>
    </div>
</asp:Content>
