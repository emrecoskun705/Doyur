<%@ Page Title="" Language="C#" MasterPageFile="~/company/Site1.Master" AutoEventWireup="true" CodeBehind="selectcategory.aspx.cs" Inherits="Doyur.company.selectcategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box-out box" style="margin-top: 60px;">
        <div class="box-top">
            Kategori Seç
        </div>
        <div id="alert-message"></div>
        <div class="box-in">
            <div class="row">
                <asp:Repeater ID="parentR" runat="server" OnItemDataBound="parentR_ItemDataBound">
                    <ItemTemplate>    
                    <ul>
                        <asp:Repeater ID="childR" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:Label ID="cValue" runat="server" Text='<%# Eval("CategoryId") %>' Visible="false"></asp:Label>
                                    <asp:Button ID="btn" runat="server" Text='<%# Eval("Name") %>' BorderStyle="None" OnClick="btn_Click" />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
