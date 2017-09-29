<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructionMethod.aspx.cs" Inherits="KMSABET.AppPages.InstructionMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Instruction Method</h2>


    <br />
    <br />

    <table>
        <tr>
            <td>Program : </td>
            <td>
                <asp:DropDownList ID="program" runat="server" OnSelectedIndexChanged="program_SelectedIndexChanged" AutoPostBack="true" />
            </td>

        </tr>
        <tr>
            <td>Course : </td>
            <td>
                <asp:DropDownList ID="Course" runat="server" OnSelectedIndexChanged="Course_SelectedIndexChanged" />
            </td>

        </tr>
        <tr>
            <td>Acadmic Year : </td>
            <td>
                <asp:DropDownList ID="Year" runat="server" OnSelectedIndexChanged="Year_SelectedIndexChanged" />
            </td>

        </tr>

        <tr>
            <td>Semester : </td>
            <td>
                <asp:DropDownList ID="Semsester" runat="server" OnSelectedIndexChanged="Semsester_SelectedIndexChanged" />
            </td>

        </tr>
        <tr>
            <td>CLO : </td>
            <td>
                <asp:DropDownList ID="Clo" runat="server" OnSelectedIndexChanged="Clo_SelectedIndexChanged" />
            </td>

        </tr>
        <tr>
            <td>Instructor Method : </td>
            <td>
                <asp:DropDownList ID="insMethod" runat="server" />
            </td>

        </tr>
    </table>


    <br />

    <br />

    <asp:Button ID="btn" Text="Submit" CssClass="btn btn-default" runat="server" OnClick="btn_Click" />

</asp:Content>
