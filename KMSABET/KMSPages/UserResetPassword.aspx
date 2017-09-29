<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserResetPassword.aspx.cs" Inherits="KMSABET.KMSPages.UserResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Reset Password</h3>
    <asp:Label ID="noteId" runat="server" Text="Note: You must change your password before using the system."
        ForeColor="Red"></asp:Label>
    <br/><br/>
    <table>
        <tr>
            <th>
                <asp:Label ID="usernameId" runat="server" Text="Username: "></asp:Label>
            </th>
            <td>
                <asp:Label ID="usernameTb" runat="server"></asp:Label>
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
    </table>
    
    <br/>
    <asp:Button ID="b" runat="server" OnClick="b_Click" class="btn btn-default" Text="Add User"/>
    <asp:Button runat="server" CssClass="btn btn-default" Text="Cancel" ID="cancelBtn" CausesValidation="false" PostBackUrl="~/KMSPages/PageRedirection.aspx"/>

</asp:Content>
