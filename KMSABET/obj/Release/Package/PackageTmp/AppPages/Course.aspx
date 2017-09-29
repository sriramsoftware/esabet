<%@ Page Language="C#" Title="Course Enrollment" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="~/AppPages/Course.aspx.cs" Inherits="KMSABET.AppPages.WebForm1" %>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="MainContent">
    <link href="/Content/bootstrap.css" rel="stylesheet" />


    <h3>Course Enrollment List</h3>

    <br />

    <asp:Button ID="Addnew" CssClass="btn btn-default" OnClick="Addnew_Click" runat="server" Text="Add Course Enrollment" />
    <asp:Button ID="Back" runat="server" Text="Close" CssClass="btn btn-default" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx" />
    <br />
    <br />
    <asp:GridView ID="grid" OnLoad="grid_Load" runat="server" AutoGenerateColumns="False" Width="100%" ItemType="KMSABET.AppPages.Course_Information" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <Columns>

            <asp:BoundField DataField="ACDEMIC_YEAR" HeaderText="Acadmic Year" SortExpression="ACDEMIC_YEAR" />
            <asp:BoundField DataField="SEMESTER" HeaderText="Semester" SortExpression="SEMESTER" />
            <asp:BoundField DataField="APP_COURSE_ID" HeaderText="Course Name" SortExpression="APP_COURSE_ID" />
            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <a href="CourseViewData.aspx?ID=<%#: Item.COURSE_ENROL_ID %>&Delete=false&Update=false&View=true">View </a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="CourseViewData.aspx?ID=<%#: Item.COURSE_ENROL_ID %>&Delete=false&Update=true&View=false">Edit </a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="CourseViewData.aspx?ID=<%#: Item.COURSE_ENROL_ID %>&Delete=true&Update=false&View=false">Delete </a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="View Topics">
                <ItemTemplate>
                    <a href="ViewCourseTopicEnroll.aspx?EID=<%#: Item.COURSE_ENROL_ID %>">View Topics</a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>


    <br />


</asp:Content>
