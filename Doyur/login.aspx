<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Doyur.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
            input[type="text"], input[type="password"] {
                box-sizing: border-box;
                border: 1px solid #cccccc;
                background-color: #fafafa;
                height: 34px;
                font-size: 1.00em;
                width: 90%;
                padding: 2px 6px 0px 6px;
            }
    </style>
    <script src="/js/activationmodal.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="box-out box box-login" style="margin-top: 60px;">
        <div class="box-top">
            Giriş Yap
        </div>

        <div class="box-in">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
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
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator" ErrorMessage="Mail" runat="server" ControlToValidate="mail" Display="None"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regexmail" runat="server"           
                                ControlToValidate="mail"
                                ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                                ErrorMessage="Mail"
                                Display="None">
                            </asp:RegularExpressionValidator>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Şifre" ControlToValidate="password" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>

                    <tr>
                        <td>
                            <div style="text-align: right; padding: 5px; margin: 0 40px 0 0">
                                <asp:Button BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" OnClick="LoginButton_Click" ID="LoginButton" runat="server" Text="Giriş Yap" CssClass="btn"/>
                            </div>
                        </td>
                        
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
