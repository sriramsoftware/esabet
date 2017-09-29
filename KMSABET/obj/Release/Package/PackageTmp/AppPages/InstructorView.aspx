<%@ Page Title="Instructor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructorView.aspx.cs" Inherits="KMSABET.AppPages.InstructorView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Instructor View</h2>

    <table>

        <tr>
            <th><label>First Name :</label></th>
            <td><asp:TextBox ID="TextBox1" runat="server" style="width:250px;"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="req" runat="server" ErrorMessage="* Requried" ControlToValidate="TextBox1" ForeColor="Red"/></td>
        </tr>

        <tr>
            <th><label>Middle Name</label></th>
            <td><asp:TextBox ID="TextBox2" runat="server" style="width:250px; "></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="req" runat="server" ErrorMessage="* Requried" ControlToValidate="TextBox2" ForeColor="Red"/></td>
        </tr>

        <tr>
            <th><label>Last Name :</label></th>
            <td><asp:TextBox ID="TextBox3" runat="server" style="width:250px; "></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ValidationGroup="req" ErrorMessage="* Requried" ControlToValidate="TextBox3" ForeColor="Red"/></td>
        </tr>

        <tr>
            <th>Office Room No :</th>
            <td><asp:TextBox ID="TextBox4" runat="server" style="width:250px; "></asp:TextBox></td>
        </tr>

        <tr>
            <th>Building :</th>
            <td><asp:TextBox ID="TextBox5" runat="server" style="width:250px; "></asp:TextBox></td>
        </tr>

        <tr>
            <th>Office Phone Extension :</th>
            <td><asp:TextBox ID="TextBox6" runat="server" style="width:250px; "></asp:TextBox></td>
        </tr>

        <tr>
            <th>Email :</th>
            <td><asp:TextBox ID="TextBox7" runat="server" style="width:250px; "></asp:TextBox></td>
        </tr>

        <tr>
            <th>Cell Number :</th>
            <td><asp:TextBox ID="TextBox8" runat="server" style="width:250px; "></asp:TextBox></td>
        </tr>

        <tr>
            <th>Web Address :</th>
            <td><asp:TextBox ID="TextBox9" runat="server" style="width:250px; "></asp:TextBox></td>
        </tr>

        <tr>
            <th>University</th>
            <td>
                <asp:TextBox ID="uniss" runat="server" Visible="false" Enabled="false"/>
                <asp:DropDownList ID="unis" runat="server" DataSourceID="SqlDataSource2" DataTextField="UNI_NAME" DataValueField="UNI_NAME"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KMSABETDBConnectionString %>" SelectCommand="SELECT [UNI_NAME] FROM [APP_UNIVERSITY]"></asp:SqlDataSource>
            </td>
        </tr>

        <tr>
            <th>  <asp:Button ID="Button1" runat="server" class="btn btn-default"  ValidationGroup="req" OnClick="Button1_Click" Text="Submit" />
            <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" PostBackUrl="~/AppPages/Instructor.aspx" /></th>
            
        </tr>

    </table>


  

</asp:Content>
