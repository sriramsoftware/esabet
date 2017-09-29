<%@ Page Title="Authentication" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/AppPages/Authentication.aspx.cs" Inherits="KMSABET.AppPages.Authentication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Sign in to Continue</h1>
    
    <br />
    <asp:label ID="TextBox1" runat="server" ForeColor="Red"/>
    <table>
        <tr>
            <th>
                <label>Username</label><br />
            </th>
            <td>
                <label>*</label>
                <asp:TextBox ID="username" runat="server"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="username" 
                    ErrorMessage="Username is required" SetFocusOnError="true" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                <label>Password</label><br />
            </th>
            <td>
                <asp:Label ID="Label2" runat="server" Text="*" />
                <asp:TextBox TextMode="Password" ID="pass" runat="server" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="pass" 
                    ErrorMessage="Password is required" SetFocusOnError="true" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <br />


    <asp:Button id="btnlogin" runat="server" CssClass="btn btn-default" Text="Login" OnClick="btnlogin_Click"/>

</asp:Content>
