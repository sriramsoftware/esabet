<%@ Page Title="Ask for Improvement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImpAskSuggestionOne.aspx.cs" Inherits="KMSABET.KMSPages.ImpAskSuggestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Improvement Plan</h3>
    <table style="padding:4px;">
        <tr>   
            <th><asp:Label Text="Program: " runat="server"></asp:Label></th>
            <td><asp:DropDownList ID="DropDownList1" AutoPostBack="True"
                OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList1"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>   
            <th><asp:Label Text="Course: " runat="server"></asp:Label></th>
            <td><asp:DropDownList ID="DropDownList2" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="DropDownList2"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>   
            <th><asp:Label Text="CLO: " runat="server"></asp:Label></th>
            <td><asp:DropDownList ID="DropDownList3" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList3"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr><td><br /></td><td></td></tr>
    </table>
    <asp:Button id="Button1" CssClass="btn btn-default" Text="Next" onclick="Button1_Click" runat="server"/>
    <a href="ImpPlanMenu.aspx" class="btn btn-default">Close</a>
</asp:Content>
