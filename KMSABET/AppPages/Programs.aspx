<%@ Page Title="Program" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Programs.aspx.cs" Inherits="KMSABET.AppPages.Programs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Program</h2>

    <br />

    <label>Program : </label>  <asp:TextBox ID="Program" runat="server" ValidateRequestMode="Enabled"/>
    <br /><br />
    <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-default" Text="Submit" OnClick="btnAdd_Click" />
    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
    
</asp:Content>
