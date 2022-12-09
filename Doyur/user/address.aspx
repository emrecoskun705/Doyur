<%@ Page Title="" Language="C#" MasterPageFile="~/user/Site.Master" AutoEventWireup="true" CodeBehind="address.aspx.cs" Inherits="Doyur.user.address" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <div class="box-out box box-login" style="margin-top: 60px;">
        <div class="box-top">
            Adres Seç veya Yeni Adres Ekle
        </div>
        <div id="alert-message"></div>

        <div class="box-in">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
            <table>
                <tbody>
                    <tr>
                        <td>Kayıtlı adresler</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="addressDL" runat="server" Width="120px" OnSelectedIndexChanged="addressDL_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Adres Adı</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="aName" runat="server"></asp:TextBox>
                        </td>
                         <td>                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Adres Adı" runat="server" ControlToValidate="aName" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>İl</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="aTown" runat="server"></asp:TextBox>
                        </td>
                         <td>                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="İl" ControlToValidate="aTown" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>İlçe</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="aDistrict" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="İlçe" ControlToValidate="aDistrict" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Adres tarifi</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="aDescription" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Adres Tarifi" ControlToValidate="aDescription" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Telefon</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="phone" runat="server"></asp:TextBox>
                        </td>
                         <td>                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Telefon" ControlToValidate="phone" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="text-align: right; padding: 5px;">
                                <asp:Button BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" ID="SaveBtn" runat="server" Text="Kaydet" CssClass="btn" OnClick="SaveBtn_Click"/>
                            </div>
                        </td>
                        
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
