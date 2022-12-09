<%@ Page Title="" Language="C#" MasterPageFile="~/user/Site.Master" AutoEventWireup="true" CodeBehind="createaddress.aspx.cs" Inherits="Doyur.user.createaddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <asp:FormView runat="server">
        <ItemTemplate>
            <div class="box-out box box-login" style="margin-top: 60px;">
        <div class="box-top">
            Adresi Düzenle
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <asp:Panel ID="Panel1" runat="server">
                
            </asp:Panel>
        </div>
    </div>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
