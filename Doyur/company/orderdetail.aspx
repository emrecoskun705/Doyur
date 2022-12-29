<%@ Page Title="" Language="C#" MasterPageFile="~/company/Site1.Master" AutoEventWireup="true" CodeBehind="orderdetail.aspx.cs" Inherits="Doyur.company.orderdetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderjs" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.sidebar a').each(function () {
                console.log(this.href)
                if (this.href == "http://localhost:52803/company/orders") {

                    $(this).addClass('active');
                }
            });
        });
</script>
</asp:Content>
