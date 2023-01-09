<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="Doyur.order.payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/payment.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">


    <div class="box-out box box-address" style="margin-top: 60px;">
        <div class="box-top">
            Lütfen adres seçin
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="divr">
                <% if ( AddressCount < 5)
                   { %>
                    <asp:HyperLink CssClass="btn-xs platin-b" NavigateUrl="~/user/createaddress?redirect=payment" ID="addrLink" runat="server">Adres Ekle</asp:HyperLink>
                <%} else
                   { %>
                <p>En fazla 5 adrese sahip olabilirsin</p>
                <% } %>
            </div>
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gList_RowCommand">
                
                <Columns> 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="isChecked" runat="server" Checked='<%# ((bool)Eval("IsActive")) ? true : false %>' />
                            <asp:HiddenField ID="addressId" runat="server" Value='<%# Eval("AddressId") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                            <asp:Button ID="btnDelete" runat="server" Text="Sil" CssClass="btn-xs btn-red" CommandName="DeleteAddress" CommandArgument='<%# Eval("AddressId") %>' OnClientClick="return confirm('Adresi silmek istediğinize emin misiniz?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div>
    <div class="box-out box box-address">
        <div class="box-top">
            Ödeme Yöntemi
        </div>
        
            <div class="p-row">
              <div class="p-col-75">
                <div class="p-container">
                    <div class="p-row">
                      <div class="p-col-50">
                        <h3>Kredi Kartı</h3>
                        <div class="p-icon-container">
                          <i class="fa fa-cc-visa" style="color:navy;"></i>
                          <i class="fa fa-cc-amex" style="color:blue;"></i>
                          <i class="fa fa-cc-mastercard" style="color:red;"></i>
                          <i class="fa fa-cc-discover" style="color:orange;"></i>
                        </div>
                        <label for="cname">Kart üzerindeki isim</label>
                        <input type="text" id="cname" name="cardname" placeholder="İsim Soyisim">
                        <label for="ccnum">Kart numarası</label>
                        <input type="text" id="ccnum" name="cardnumber" placeholder="1111-2222-3333-4444">
                        <div class="p-row">
                          <div class="p-col-50">
                            <label for="expyear">Son Geçerlilik Tarihi</label>
                            <input type="text" id="expyear" name="expyear" placeholder="09/2025">
                          </div>
                          <div class="p-col-50">
                            <label for="cvv">CVV</label>
                            <input type="text" id="cvv" name="cvv" placeholder="352">
                          </div>
                        </div>
                      </div>

                    </div>
                    <label>
                      
                    </label>
                    <asp:Button ID="payBtn" OnClick="payBtn_Click" Text="Ödeme Yap" runat="server" CssClass="btn" Width="100%"  BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" />

                </div>
              </div>

              <div class="p-col-25">
                <div class="p-container">
                  <h4>Sepet
                    <span class="price" style="color:black">
                        <img src="/image/icons/shoppingcart.png" />
                      <b><%= CartQuantity.ToString() %></b>
                    </span>
                  </h4>
                    <asp:ListView ID="pList" runat="server">
                        <ItemTemplate>
                            <p><a class="pp-price" href="/product/?id=<%# Eval("Product.ProductId") %>"> <%# (Eval("Product.Name").ToString().Length > 10) ? (Eval("Product.Name").ToString().Substring(0, 10) + "...") : Eval("Product.Name")%></a> <span class="p-price"><%# (Convert.ToDecimal(Eval("Product.Price")) * Convert.ToInt32(Eval("OPInfo.ProductQuantity"))).ToString("#.##") %> TL</span></p>
                        </ItemTemplate>
                    </asp:ListView>
                 
                  <hr>
                  <p>Toplam <span class="p-price" style="color:black"><b><%= TotalPrice.ToString("#.##") %> TL</b></span></p>
                </div>
              </div>
            </div>
        
    </div>



</asp:Content>
