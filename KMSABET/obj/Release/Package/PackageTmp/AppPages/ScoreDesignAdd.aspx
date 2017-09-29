<%@ Page Title="Score Design" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ScoreDesignAdd.aspx.cs" Inherits="KMSABET.AppPages.ScoreDesignAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Score Design</h2>
    
    <table style="width:100%;">
        
            <tr>
                <th><label>Program :</label></th>
                <td><asp:DropDownList ID="programs" AutoPostBack="true" runat="server" OnSelectedIndexChanged="programs_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr>
                <th><label>Course :</label></th>
                <td><asp:DropDownList ID="course" runat="server" AutoPostBack="True" OnSelectedIndexChanged="course_SelectedIndexChanged"/></td>

            </tr>
            
        <tr>
            <th><label>Acadmic Year :</label></th>
            <td><asp:DropDownList ID="Years" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Years_SelectedIndexChanged" /></td>
        </tr>
            
        <tr>
            <th><label>Semester :</label></th>
            <td><asp:DropDownList ID="Semester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Semester_SelectedIndexChanged"/></td>
        </tr>

        <tr>    
            <th><label>Assessment :</label></th>
            <td><asp:DropDownList ID="assessment" runat="server" OnSelectedIndexChanged="assessment_SelectedIndexChanged"/></td>
        </tr>

        <tr>
            <th><label>Assessment Score Value :</label></th>
            <td><asp:TextBox ID="ASC" runat="server" Enabled="false"/></td>
        </tr>

        <tr>
            <th><label>CLO :</label></th>
            <td><asp:DropDownList ID="CLO" runat="server" /></td>
        </tr>

        <tr>
            <th><label>Assessment Name :</label></th>
            <td><asp:TextBox ID="AN" runat="server"/></td>
        </tr>

        <tr>
            <th><label>Raw Score</label></th>
            <td><asp:TextBox ID="RS" runat="server" /></td>
        </tr>

        <tr>
            <th><label>Score Value</label></th>
            <td><asp:TextBox ID="SV" runat="server" /></td>
        </tr>

        </table>    

    <br />



    <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="Submit_Click"/>

</asp:Content>
