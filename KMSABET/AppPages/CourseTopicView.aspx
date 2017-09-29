<%@ Page Title="Course Topic" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseTopicView.aspx.cs" Inherits="KMSABET.AppPages.CourseTopicView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />

    <h3>Course Topic Details</h3>

    <br />


    <table>

        <tr>
            <td><label>Program :</label></td>
            <td><asp:DropDownList ID="Program" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="Program_SelectedIndexChanegd"></asp:DropDownList></td>
        </tr>

        <tr>
            <td><label>Course :</label></td>
            <td><asp:DropDownList ID="Course" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Course_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        
        <tr>
            <td>
                <label>CLO :</label>
            </td>
            <td><asp:DropDownList ID="CLO" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CLO_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        <tr>
            <td><label>Topic Statement :</label></td>
            <td><asp:TextBox ID="TopicStatement" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox></td>
        </tr>

        <tr>
            <td><label>Lab Hours :</label></td>
            <td><asp:DropDownList ID="Lab_Hours" runat="server" Height="28px" Width="142px">                    
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td> <label>Lecture Hours :</label></td>
            <td> <asp:DropDownList ID="Lecture" runat="server" Height="28px" Width="142px">                    
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td><asp:Button ID="button" runat="server" Width="102px" Text="Sumbit" CssClass="btn btn-default" OnClick="button_Click"/>
    <asp:Button ID="Update" runat="server" Width="102px" Text="Submit" CssClass="btn btn-default" OnClick="Update_Click"/>
    
    <asp:Button Text="Cancel" runat="server" CssClass="btn btn-default" CausesValidation="false" PostBackUrl="~/AppPages/CourseTopic.aspx"/>
    </td>
            <td><asp:RequiredFieldValidator ErrorMessage="* Requried" ForeColor="Red" runat="server" ControlToValidate="TopicStatement" /></td>
        </tr>

</table>    
</asp:Content>
