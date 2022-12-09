<%@ Page Title="" Language="C#" MasterPageFile="~/user/Site.Master" AutoEventWireup="true" CodeBehind="addresslist.aspx.cs" Inherits="Doyur.user.addresslist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <div class="box-out box box-login" style="margin-top: 60px;">
        <div class="box-top">
            Adres Listesi
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="divr">
                <asp:HyperLink CssClass="btn-xs platin-b" NavigateUrl="~/user/createaddress.aspx" ID="addrLink" runat="server">Adres Ekle</asp:HyperLink>
            </div>
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gList_RowCommand">
                
                <Columns> 
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
                            <asp:Button ID="btnDelete" runat="server" Text="Sil" CssClass="btn-xs btn-red" CommandName="Delete" CommandArgument='<%# Eval("AddressId") %>' OnClientClick="return confirm('Adresi silmek istediğinize emin misiniz?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div>
</asp:Content>
