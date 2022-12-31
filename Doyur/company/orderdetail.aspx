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
            <div class="divr">
                Seçili siparişleri güncelle
                <asp:DropDownList ID="ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                    <asp:ListItem disabled="disabled" Selected="True" Value="-1">Seçiniz</asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />
            <asp:GridView ID="gList" runat="server" Da AutoGenerateColumns="False" CssClass="mGrid">
                
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" Checked="true" runat="server" onclick = "checkAll(this);" Text="Hepsini seç" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRow" Checked="true" onclick = "Check_Click(this)" runat="server" />
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
                            <asp:Label ID="opstatus" runat="server" Text='<%# Types.OrderProduct.GetOrderPStatus()[Convert.ToInt32(Eval("OPInfo.Status"))].Title %>'></asp:Label>
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

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows


                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        inputList[i].checked = false;

                    }

                }

            }

        }

        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;




            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;
        }
    </script>
</asp:Content>
