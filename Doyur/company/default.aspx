<%@ Page Title="" Language="C#" MasterPageFile="~/company/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur.company._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out" style="margin-top: 60px; margin-left: 50px; margin-right: 100px;">
        <div class="box-top">
            Ürün Listesi
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="divr">
                <asp:HyperLink CssClass="btn-xs platin-b" NavigateUrl="~/company/selectcategory.aspx" ID="addrLink" runat="server">Ürün Ekle</asp:HyperLink>
            </div>
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gList_RowCommand">
                
                <Columns>
                    <asp:BoundField HeaderText="Ürün Numarası" DataField="ProductId" />
                    <asp:BoundField HeaderText="Ürün Adı" DataField="Name" />
                    <asp:BoundField HeaderText="Kategorisi" DataField="CName" />
                    <asp:BoundField HeaderText="Fiyat" DataField="Price" />
                    <asp:BoundField HeaderText="Stok" DataField="Stock" />
                    <asp:TemplateField HeaderText="Aktif" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <span id="dsf" class='<%# ((bool)Eval("IsActive")) ? "st-active" : "st-notactive" %>' runat="server">
                                <%# ((bool)Eval("IsActive")) ? "Aktif" : "Pasif" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Düzenle" CssClass="btn-xs btn-green" CommandName="Edit" CommandArgument='<%# Eval("ProductId") %>' />
                            <asp:Button ID="btnDelete" runat="server" Text="Sil" CssClass="btn-xs btn-red" CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductId") %>' OnClientClick="return confirm('Adresi silmek istediğinize emin misiniz?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div> 
</asp:Content>
