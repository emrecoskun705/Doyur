﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="featurelist.aspx.cs" Inherits="Doyur.admin.featurelist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out box box-login" style="margin-top: 60px">
        <div class="box-top">
            Özellik Listesi
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="divr">
                <table>
                    <tbody>
                        <tr>
                            <td>Yeni Ana Özellik</td>
                             <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="newFeatureLbl" runat="server"></asp:TextBox>
                            </td>
                             <td>                            
                                 <asp:RequiredFieldValidator ValidationGroup="group1" ID="RequiredFieldValidator3" ErrorMessage="Özellik Adı" runat="server" ControlToValidate="newFeatureLbl" Display="None"></asp:RequiredFieldValidator>
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
                    <asp:BoundField HeaderText="Özellik Numarası" DataField="FeatureId" />
                    <asp:BoundField HeaderText="Ana Özellikler" DataField="Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Düzenle" CssClass="btn-xs btn-green" CommandName="EditFeature" CommandArgument='<%# Eval("FeatureId") %>' />
                            <asp:Button ID="btnDelete" runat="server" Text="Sil" CssClass="btn-xs btn-red" CommandName="DeleteFeature" CommandArgument='<%# Eval("FeatureId") %>' OnClientClick="return confirm('Adresi silmek istediğinize emin misiniz?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div> 
</asp:Content>
