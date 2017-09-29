<%@ Page Title="University" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUniversity.aspx.cs" Inherits="KMSABET.AppPages.ViewUniversity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>University</h2>

    <br />
    
    <table>
        <tr>
            <td><label>University Name : </label></td>
            <td><asp:TextBox ID="uniname" runat="server"/></td>
        </tr>

        <tr>
            <td><label>University Address : </label></td>
            <td><asp:TextBox ID="Address" runat="server" /></td>
        </tr>

        <tr>
            <td><label>Unviversity Verify</label></td>
            <td><asp:CheckBox ID="isv" runat="server"/></td>
        </tr>

    </table>

    <asp:Button ID="btn" CssClass="btn btn-default" runat="server" Text="Submit" OnClick="btn_Click"/>

</asp:Content>
