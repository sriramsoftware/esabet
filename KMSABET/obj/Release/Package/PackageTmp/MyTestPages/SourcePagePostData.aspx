<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SourcePagePostData.aspx.cs" Inherits="KMSABET.MyTestPages.SourcePagePostData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox runat="server" ID="textBox1"></asp:TextBox>
    <asp:Button 
        ID="Button2" 
        OnClick="Button2_Click"
        runat="server"
        Text="ButtonClickEvent" />
    <asp:Button 
        ID="Button1" 
        PostBackUrl="TargetPagePostData.aspx"
        runat="server"
        Text="PostBackURL" />
    <asp:HyperLink runat="server" Text="Redirect" NavigateUrl="~/MyTestPages/TargetPagePostData.aspx?id=12" ></asp:HyperLink>
</asp:Content>
