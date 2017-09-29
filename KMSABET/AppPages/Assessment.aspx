<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Assessment.aspx.cs" Inherits="KMSABET.AppPages.Assessment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="../Content/bootstrap.css" />

    <table border="1">
        <tr>
            <td colspan="7" style="text-align:center;">Assessment ID : 1 Assessment Name : </td>

        </tr>
        <tr><td colspan="7" style="text-align:center;">Total Raw Score For All Question Sets Of This Assessment = 0</td></tr>
        <tr><td colspan="7" style="text-align:center;">Contibution of This Assessment Of The Final Grade = 0 % Sum of Contirbution of all assessments (entered so far) = 0</td></tr>
        <tr><td colspan="7"><br /></td></tr>
        
        
        </table>

    <br />

    <asp:DropDownList ID="Program" runat="server" OnSelectedIndexChanged="Program_SelectedIndexChanged" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="program_name" DataValueField="program_name">
        <asp:ListItem>Select Progam</asp:ListItem>
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KMSABETDBConnectionString %>" SelectCommand="SELECT [program_name] FROM [App_Program]"></asp:SqlDataSource>

    <asp:DropDownList ID="Course" runat="server">
        <asp:ListItem>Select Coruse</asp:ListItem>
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>

    <asp:DropDownList ID="assessments" runat="server">
        <asp:ListItem>Select Assessment</asp:ListItem>
        <asp:ListItem>Quizes</asp:ListItem>
        <asp:ListItem>Homework Assignments</asp:ListItem>
        <asp:ListItem>Term-project</asp:ListItem>
        <asp:ListItem>Attendance</asp:ListItem>
        <asp:ListItem>Presentation</asp:ListItem>
        <asp:ListItem>Life-long Learning Assignment</asp:ListItem>
        <asp:ListItem>Contemporary Issues Knowledge test</asp:ListItem>
        <asp:ListItem>Lab. Reports</asp:ListItem>
        <asp:ListItem Value="Lab. Examination">Lab. Examination</asp:ListItem>
        <asp:ListItem>Mid-term Examination</asp:ListItem>
        <asp:ListItem>Exam 1</asp:ListItem>
        <asp:ListItem>Exam 2</asp:ListItem>
        <asp:ListItem>Final Examination</asp:ListItem>
        <asp:ListItem>Other Assessments</asp:ListItem>
    </asp:DropDownList>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KMSABETDBConnectionString %>" SelectCommand="SELECT [STUDENT_NAME] FROM [APP_STUDENT]"></asp:SqlDataSource>

    Total Marks
    <asp:TextBox ID="Marks" runat="server" AutoPostBack="true"></asp:TextBox>

    <asp:Button ID="Save" class="btn btn-default" runat="server" Text="Save" OnClick="Save_Click"/>

</asp:Content>
