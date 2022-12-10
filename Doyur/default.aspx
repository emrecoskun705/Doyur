<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur._default" %>

<%@ Register Src="~/UserControls/productlist.ascx" TagPrefix="uc1" TagName="productlist" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <uc1:productlist runat="server" ID="productlist" />
</asp:Content>
