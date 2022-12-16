<%@ Page Title="" Language="C#" MasterPageFile="~/company/Site1.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="Doyur.company.edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out box" style="margin-top: 60px;">
        <div class="box-top">
            Ürün Düzenleme
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
            <table>
                <tbody>
                    <tr>
                        <td>Ürün Adı</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="pName" runat="server"></asp:TextBox>
                        </td>
                         <td>                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Ürün Adı" runat="server" ControlToValidate="pName" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Fiyat</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="pPrice" runat="server"></asp:TextBox>
                        </td>
                         <td>                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Fiyat" ControlToValidate="pPrice" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Stok</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="pStock" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Stok" ControlToValidate="pStock" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Aktif Ürün</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="IsActive" runat="server" />
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Stok" ControlToValidate="pStock" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td>Fotoğraf</td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <input id="oFile" type="file" runat="server" NAME="oFile" />
                        </td>
                         <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Stok" ControlToValidate="pStock" Display="None"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <asp:Repeater ID="parentR" runat="server" OnItemDataBound="parentR_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Name") %></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Repeater ID="childR" runat="server">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cBoxId" Text='<%# Eval("Name") %>' runat="server" Checked='<%# SelectedFeatureIds.Contains(Convert.ToInt32(Eval("FeatureId"))) ? true : false%>' />
                                            <asp:HiddenField ID="hdnId" Value='<%# Eval("FeatureId") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td>
                            <hr />
                        </td>

                    </tr>
                    <tr>
                         <td>Ürün Açıklaması</td>
                    </tr>
                    <tr>
                         <td>
                             <textarea id="productContent" runat="server"></textarea>
                         </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="text-align: right; padding: 5px;">
                                <asp:Button BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff" ID="ctgryId" runat="server" Text="Kaydet" CssClass="btn" OnClick="SaveBtn_Click" />
                            </div>
                        </td>    
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        CKEDITOR.replace('ContentPlaceHolder1_productContent');
    </script>
</asp:Content>
