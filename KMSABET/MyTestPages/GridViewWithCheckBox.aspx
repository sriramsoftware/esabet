<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GridViewWithCheckBox.aspx.cs" Inherits="KMSABET.MyTestPages.GridViewWithCheckBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="AttributeOptions" runat="server"
        AutoGenerateColumns="False"
        CellPadding="4">
        <Columns>
            <asp:TemplateField Visible="true" HeaderText="Select">
                <ItemTemplate>
                    <asp:Label DataField="questionStatement" Text="asdffasd" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="questionStatement" HeaderText="Question Statement"/>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick = "Print" />
</asp:Content>
