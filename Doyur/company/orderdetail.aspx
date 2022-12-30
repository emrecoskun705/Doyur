<%@ Page Title="" Language="C#" MasterPageFile="~/company/Site1.Master" AutoEventWireup="true" CodeBehind="orderdetail.aspx.cs" Inherits="Doyur.company.orderdetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/orderaddress.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out" style="margin-top: 60px;">
        <div class="box-top">
            Sipariş detayı
        </div>
        <div class="box-in">
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gList_RowCommand">
                
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" Text="Hepsini seç" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRow" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                        <ItemTemplate>
                            <img src="<%# "/image/" + Eval("Product.ImageUrl") %>" width="20" height="20" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Width="10px" DataField="Product.ProductId" HeaderText="Ürün Numarası" />
                    <asp:BoundField  DataField="Product.Name" HeaderText="Ürün Adı" />
                    <asp:TemplateField HeaderText="Ürün Durumu">
                        <ItemTemplate>
                            <%# Types.OrderProduct.GetOrderPStatus()[Convert.ToInt32(Eval("OPInfo.Status"))].Title %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Width="150px" DataField="Product.Price" HeaderText="Ürün Fiyatı" />
                    <asp:BoundField ItemStyle-Width="10px" DataField="OPInfo.ProductQuantity" HeaderText="Miktar" />

                </Columns>
            </asp:GridView>
        </div>

        <div style="display: flex; margin: 10px;"> 
            <div class="address-container">
                <div class="address">
                    Adres Bilgileri
                </div>
                <% if (Address != null)
                    { %>
                    <p class="title1">Alıcı: <%= Address.Firstname + " " + Address.Lastname %></p>
                    <p class="title2"><%= Address.Name %></p>
                    <p class="description"><%= Address.Description %></p>
                    <p class="description"><%= Address.District + "/" + Address.Town %></p>
                    <p class="title2">Telefon: <%= Address.Phone%></p>
                <% } %>
            </div>
        </div>
    </div>

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
