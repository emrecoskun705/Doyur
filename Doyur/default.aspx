<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Doyur._default" %>

<%@ Register Src="~/UserControls/RestaurantList.ascx" TagPrefix="uc1" TagName="RestaurantList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <uc1:RestaurantList runat="server" id="RestaurantList" />
</asp:Content>
