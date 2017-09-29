<%@ Page Title="Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseOption.aspx.cs" Inherits="KMSABET.AppPages.CourseOption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course</h2>

    <br />

    <table>

        <tr>
            <td><label>Program : </label></td>
            <td><asp:DropDownList ID="Program" runat="server"></asp:DropDownList></td>    
        </tr>

        <tr>
            <td><label>Course Number : </label></td>
            <td><asp:TextBox ID="CNU" runat="server" /></td>
        </tr>

        <tr>
            <td><label>Course : </label></td>
            <td><asp:TextBox ID="Course" runat="server" /></td>
        </tr>

        <tr>
            <td><label>Course Type : </label></td>
            <td><asp:DropDownList ID="CT" runat="server"><asp:ListItem>Core</asp:ListItem><asp:ListItem>Elective</asp:ListItem></asp:DropDownList></td>
        </tr>

        <tr>
            <td><label>Theory Credit Hours : </label></td>
            <td><asp:TextBox ID="TCRH" runat="server" TextMode="Number" Text="0"/></td>
        </tr>

        <tr>
            <td><label>Theory Contact Hours : </label></td>
            <td><asp:TextBox ID="TCH" runat="server" TextMode="Number" Text="0" /></td>
        </tr>

        <tr>
            <td><label>Lab Credit Hours : </label></td>
            <td><asp:TextBox ID="LCRH" runat="server" TextMode="Number" Text="0" /></td>
        </tr>

        <tr>
            <td><label>Lab Contact Hours : </label></td>
            <td><asp:TextBox ID="LCH" runat="server" TextMode="Number" Text="0" /></td>
        </tr>

    </table>

    <br />
    <asp:Button ID="btn" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="btn_Click" />
    <asp:Button ID="Cancel" runat="server" Text="Close" CssClass="btn btn-default" PostBackUrl="~/AppPages/CoursesViews.aspx" />

</asp:Content>
