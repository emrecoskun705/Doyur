<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Doyur.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="box-out box box-login" style="margin-top: 60px;">
        <div class="box-top">
            Kayıt Ol
        </div>
        <div id="alert-message"></div>

        <div class="box-in">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <table>
                <tbody>
                    <tr>
                        <td>mail</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="mail" runat="server"></asp:TextBox>
                        </td>
                         <td>                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Mail" runat="server" ControlToValidate="mail" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Kullanıcı Adı</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="username" runat="server"></asp:TextBox>
                        </td>
                         <td>                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Kullanıcı Adı" ControlToValidate="username" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Ad</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="firstname" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ad" ControlToValidate="lastname" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Soyad</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="lastname" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Soyad" ControlToValidate="lastname" Display="None"></asp:RequiredFieldValidator>
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
                        <td>GSM</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="gsm" runat="server"></asp:TextBox>
                        </td>
                         <td>                             
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="GSM" ControlToValidate="gsm" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Cinsiyet</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="gender" runat="server" Width="120px">
                                <asp:ListItem Value="0">Not Selected</asp:ListItem>
                                <asp:ListItem Value="1">Male</asp:ListItem>
                                <asp:ListItem Value="2">Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Birth Date</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="birthdatetime" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Doğum Tarihi" ControlToValidate="birthdatetime" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Şifre</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="password1" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Şifre" ControlToValidate="password1" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Şifreyi tekrar giriniz</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="password2" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                         <td>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Şifreyi Tekrar Giriniz" ControlToValidate="password2" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="text-align: right; padding: 5px;">
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" ID="CreateButton" runat="server" Text="Kayıt ol" CssClass="btn" OnClick="CreateButton_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                        
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>
