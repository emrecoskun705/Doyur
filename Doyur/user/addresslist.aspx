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
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                <Columns>
                    <asp:BoundField HeaderText="Adres Adı" DataField="Name" />
                    <asp:BoundField HeaderText="İl" DataField="Town" />
                    <asp:BoundField HeaderText="İlçe" DataField="District" />
                    <asp:BoundField HeaderText="Telefon" DataField="Phone" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Düzenle" CssClass="btn-xs btn-green" />
                            <asp:Button ID="btnDelete" runat="server" Text="Sil" CssClass="btn-xs btn-red"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
</asp:Content>
