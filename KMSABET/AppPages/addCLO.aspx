<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addCLO.aspx.cs" Inherits="KMSABET.AppPages.addCLO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>CLO View</h2>

    <br /><br />
    
    <table>
        <tr>
            <td><label>Program : </label></td>
            <td><asp:DropDownList ID="Program" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Program_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>            <td><label>Course : </label></td>

            <td><asp:DropDownList ID="Course" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Course_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label>CLO Statement : </label></td>
            <td><asp:TextBox ID="clos" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox></td>
        </tr>
          
    </table>

    <br />

    <asp:Button ID="btnadd" runat="server" CssClass="btn btn-default" Text="Submit" OnClick="btnadd_Click"/>
    <asp:Button ID="Close" runat="server" CssClass="btn btn-default" Text="Close" PostBackUrl="~/AppPages/CLO.aspx" />


</asp:Content>
