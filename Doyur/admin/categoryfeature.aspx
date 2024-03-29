﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="categoryfeature.aspx.cs" Inherits="Doyur.admin.categoryfeature" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out box box-login" style="margin-top: 60px">
        <div class="box-top">
            <asp:Label runat="server" ID="parentLbl" Text="Label"></asp:Label>
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="divr">
            </div>
            <br />
            <asp:GridView ID="gList" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowCommand="gList_RowCommand" OnRowDataBound="gList_RowDataBound">
                
                <Columns>
                    <asp:BoundField HeaderText="Özellik Numarası" DataField="FeatureId" />
                    <asp:BoundField HeaderText="Adı" DataField="Name" />
                    <asp:TemplateField HeaderText="Select Data">  
                        <ItemTemplate>  
                            <asp:CheckBox ID="isChecked" runat="server" />  
                        </ItemTemplate>  
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>
            <div style="text-align: right; padding: 5px;">
                <asp:Button BorderStyle="None" BackColor="#10980f" BorderColor="#10980f" ForeColor="#ffffff"  ID="saveBtn" OnClick="saveBtn_Click" runat="server" Text="Kaydet" CssClass="btn"/>
            </div>


            <asp:ValidationSummary ValidationGroup="group1" ID="ValidationSummary1" runat="server" CssClass="error-danger" />
           
        </div>
    </div>
    <div class="box-bottom"></div> 
</asp:Content>
