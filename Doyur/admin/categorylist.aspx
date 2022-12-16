<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="categorylist.aspx.cs" Inherits="Doyur.admin.categorylist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out box box-login" style="margin-top: 60px">
        <div class="box-top">
            Kategori Listesi
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="divr">
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <a href="/admin/categorylist.aspx">Başlangıç</a> >
                                <asp:Repeater ID="pathRepeater" runat="server">
                                    <ItemTemplate>
                                        <a href="/admin/categorylist.aspx?id=<%# Eval("CategoryId") %>"><%# Eval("Name") %></a> >
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>

                            <td><br />Kategori Ekle</td>
                             <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="newCategoryLbl" runat="server"></asp:TextBox>
                            </td>
                             <td>                            
                                 <asp:RequiredFieldValidator ValidationGroup="group1" ID="RequiredFieldValidator3" ErrorMessage="Özellik Adı" runat="server" ControlToValidate="newCategoryLbl" Display="None"></asp:RequiredFieldValidator>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ValidationGroup="group1" CssClass="btn-xs btn-green" ID="saveBtn" Text="Ekle" runat="server" OnClick="saveBtn_Click"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gList_RowCommand">
                
                <Columns>
                    <asp:BoundField HeaderText="Kategori Numarası" DataField="CategoryId" />
                    <asp:BoundField HeaderText="Kategoriler" DataField="Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Düzenle" CssClass="btn-xs btn-green" CommandName="EditCategory" CommandArgument='<%# Eval("CategoryId") %>' />
                            <asp:Button ID="Button1" runat="server" Text="Özellik Düzenle" CssClass="btn-xs btn-blue" CommandName="EditFeature" CommandArgument='<%# Eval("CategoryId") %>' />
                            <asp:Button ID="btnDelete" runat="server" Text="Sil" CssClass="btn-xs btn-red" CommandName="DeleteFeature" CommandArgument='<%# Eval("CategoryId") %>' OnClientClick="return confirm('Adresi silmek istediğinize emin misiniz?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ValidationGroup="group1" ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div> 
</asp:Content>
