<%@ Page Title="Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentView.aspx.cs" Inherits="KMSABET.AppPages.StudentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>View Students</h2>
    <br />

    <table>
        <tr>
            <td><label>Program : </label></td>
            <td><asp:DropDownList ID="AdSPro" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AdSPro_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label>Course : </label></td>
            <td><asp:DropDownList ID="AdSCour" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AdSCour_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label>Acadmic Year : </label></td>
            <td><asp:DropDownList ID="AdSAYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AdSAYear_SelectedIndexChanged"></asp:DropDownList>   </td>
        </tr>
        <tr>
            <td><label>Semester : </label></td>
            <td><asp:DropDownList ID="AdSSem" runat="server" AutoPostBack="True"></asp:DropDownList></td>
        </tr>

        <tr>
            <td><label>Student Roll Number : </label></td>
            <td><asp:TextBox ID="StudentrollTxt" runat="server" Width="194px" /></td>
        </tr>

        <tr>
            <td><label>Student Name : </label></td>
            <td><asp:TextBox ID="StudentNameTxt" runat="server" Width="260px" /></td>
        </tr>

    </table>

        <br />
        <br />
        
        <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Submit" OnClick="Button1_Click" />
        <asp:Button ID="Updates" runat="server" CssClass="btn btn-default" Text="Submit" OnClick="Update_Click" />
        <asp:Button runat="server" ID="Cancel" CssClass="btn btn-default" Text="Cancel" PostBackUrl="~/AppPages/Students.aspx"/>
    

</asp:Content>
