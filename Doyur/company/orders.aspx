<%@ Page Title="" Language="C#" MasterPageFile="~/company/Site1.Master" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="Doyur.company.orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>

    <style>


    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
<br />
<br />
    <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="OrderId" OnRowDataBound="gList_RowDataBound" OnRowCommand="gList_RowCommand">
                
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="5px">
                <ItemTemplate>
                    <a href="JavaScript:shrinkandgrow('div<%# Eval("OrderId") %>');">
                        <img alt = "" style="cursor: pointer" src="/image/plus.png" id="imgdiv<%# Eval("OrderId") %>" />
                    </a>
                    <div id="div<%# Eval("OrderId") %>" style="display: none;">
                        <asp:GridView ID="gSubList" runat="server" AutoGenerateColumns="false" CssClass = "mGrid" DataKeyNames="ProductId">
                            <Columns>
                                <asp:TemplateField HeaderText="Ürün" ItemStyle-Width="10px">
                                    <ItemTemplate>
                                        <img src="<%# "/image/" + Eval("ImageUrl") %>" width="20" height="20" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProductId" HeaderText="Ürün Numarası" />
                                <asp:BoundField DataField="Name" HeaderText="Ürün Adı" />
                                <asp:BoundField DataField="Price" HeaderText="Ürün Fiyatı" />
                                <asp:BoundField DataField="ProductQuantity" HeaderText="Adet" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField ItemStyle-Width="150px" DataField="OrderId" HeaderText="Sipariş Numarası" />
            <asp:BoundField ItemStyle-Width="150px" DataField="Status" HeaderText="Sipariş Durumu" />
        </Columns>
    </asp:GridView>

    <script type="text/javascript">
        function shrinkandgrow(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "/image/plus.png") {
                $("#" + displayIcon).closest("tr")
                    .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
                        .html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "/image/minus.png");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "/image/plus.png");
            }
        }
    </script>

</asp:Content>
