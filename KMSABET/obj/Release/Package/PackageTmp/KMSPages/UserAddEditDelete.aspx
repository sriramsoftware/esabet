<%@ Page Title="Add a User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAddEditDelete.aspx.cs" Inherits="KMSABET.KMSPages.UserAddEditDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>User Details</h3>
    <table>
        <tr>
            <th>
                <asp:Label ID="usernameId" runat="server" Text="Username: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                <asp:TextBox ID="usernameTb" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="usernameTb"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:Label ID="usernameError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="passwordId" runat="server" Text="Password: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                <asp:TextBox ID="passwordTb" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="passwordTb"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="confirmPwdId" runat="server" Text="Confirm Password: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                <asp:TextBox ID="ConfirmPwdTb" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ConfirmPwdTb"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:Label ID="confirmPwdError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="fullNameId" runat="server" Text="Full Name: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                <asp:TextBox ID="fullNameTb" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="fullNameTb"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="emailId" runat="server" Text="Email: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label5" runat="server" Text="*"></asp:Label>
                <asp:TextBox ID="emailTb" runat="server" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="emailTb"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="moduleTypeId" runat="server" Text="Module Type: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                <asp:DropDownList ID="moduleTypeDdl" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="moduleTypeDdl"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="userTypeId" runat="server" Text="User Type: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label7" runat="server" Text="*"></asp:Label>
                <asp:DropDownList ID="userTypeDdl" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="userTypeDdl"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="courseId" runat="server" Text="Course: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="Label8" runat="server" Text="*"></asp:Label>
                <asp:DropDownList ID="courseDdl" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="courseDdl"
                        ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <br/>
    <asp:Button ID="b" runat="server" OnClick="b_Click" class="btn btn-default" Text="Add User"/>
    <asp:Button runat="server" CssClass="btn btn-default" Text="Cancel" ID="cancelBtn" CausesValidation="false" PostBackUrl="~/KMSPages/UserManagement.aspx"/>

</asp:Content>
