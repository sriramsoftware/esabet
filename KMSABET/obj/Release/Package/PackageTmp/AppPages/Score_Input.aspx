<%@ Page Title="Score Input" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Score_Input.aspx.cs" Inherits="KMSABET.AppPages.Score_Input" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Score Input</h2>

    <hr />

    <br />
    
    <table>
        
        <tr>
            <th><label>Program :</label> </th>
            <td><asp:DropDownList ID="Program" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Program_SelectedIndexChanged" /></td>
        </tr>

        <tr>
            <th><label>Course :</label></th>
            <td><asp:DropDownList ID="Course" runat="server" OnSelectedIndexChanged="Course_SelectedIndexChanged" AutoPostBack="true" /><br /></td>
        </tr>


        <tr>
            <th><label>Acadmic Year</label></th>
            <td><asp:DropDownList ID="Year" runat="server" OnSelectedIndexChanged="Year_SelectedIndexChanged" AutoPostBack="true"/></td>
        </tr>

        <tr>
            <th><label>Semester</label></th>
            <td><asp:DropDownList ID="Semester" runat="server" OnSelectedIndexChanged="Semester_SelectedIndexChanged" AutoPostBack="true"/></td>
        </tr>

        <tr>
            <th><label>Assessment Type :</label></th>
            <td><asp:DropDownList ID="assessmenttype" runat="server" OnSelectedIndexChanged="assessmenttype_SelectedIndexChanged" AutoPostBack="true"/></td>
        </tr>

        <tr>
            <th><label>Assessment Values :</label></th>
            <td><asp:TextBox runat="server" ID="text" Enabled="false"></asp:TextBox></td>
        </tr>


        <tr>
            <th><label>Assessment :</label></th>
            <td><asp:DropDownList ID="assessemnt" runat="server" OnSelectedIndexChanged="assessemnt_SelectedIndexChanged" AutoPostBack="true"/></td>
        </tr>

        <tr>
            <th><label>Raw Score :</label></th>
            <td><asp:TextBox runat="server" ID="text2" Enabled="false"></asp:TextBox></td>
        </tr>


        <tr>
            <th><label>Student</label></th>
            <td><asp:DropDownList ID="Student" runat="server" AutoPostBack="True" /></td>
        </tr>

        <tr>
            <th><label>Marks</label></th>
            <td><asp:TextBox ID="Marks" runat="server"/></td>
        </tr>


    </table>

    <br />
    

    <asp:Button ID="Savebtn" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="Savebtn_Click"/>
    <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-default" PostBackUrl="~/AppPages/ScoreInputView.aspx" />
</asp:Content>
