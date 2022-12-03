<%@ Page Title="" Language="C#" MasterPageFile="~/user/Site.Master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="Doyur.user.user" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />

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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ad" ControlToValidate="firstname" Display="None"></asp:RequiredFieldValidator>
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
                        <td>Şifre</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                         <td>
                         </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="text-align: right; padding: 5px;">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>

                                    </ProgressTemplate>
                         
                                </asp:UpdateProgress>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" ID="UpdateButton" runat="server" Text="Değişiklikleri Kaydet" CssClass="btn" OnClick="UpdateButton_Click" />
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
