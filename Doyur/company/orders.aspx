<%@ Page Title="" Language="C#" MasterPageFile="~/company/Site1.Master" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="Doyur.company.orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out" style="margin-top: 60px; margin-left: 50px; margin-right: 100px;">
        <div class="box-top">
            Sipariş Listesi
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                
                <Columns>
                    <asp:BoundField HeaderText="Ürün Numarası" DataField="ProductId" />
                    <asp:BoundField HeaderText="Ürün Adı" DataField="Name" />
                    <asp:BoundField HeaderText="Fiyat" DataField="Price" />
                    <asp:BoundField HeaderText="Stok" DataField="Stock" />
                    <asp:TemplateField HeaderText="Durum" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# (Types.OrderProduct.GetOrderPStatus()[Convert.ToInt32(Eval("Status"))]).Title %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btndetails" runat="server" Text="İncele" CssClass="btn-xs btn-green" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div> 
</asp:Content>
