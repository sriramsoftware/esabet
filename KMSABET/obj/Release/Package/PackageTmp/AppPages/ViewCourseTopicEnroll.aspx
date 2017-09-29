<%@ Page Title="Course Topic" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCourseTopicEnroll.aspx.cs" Inherits="KMSABET.AppPages.ViewCourseTopicEnroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Topic Details</h2>


    <table>
        <tr>
            <th><label>Program</label></th>
            <td><asp:TextBox ID="Program" runat="server" Enabled="false"/></td>
        </tr>
        <tr>
            <th><label>Course</label></th>
            <td><asp:TextBox ID="Course" runat="server" Enabled="false"/></td>
        </tr>
        <tr>
            <th><label>Acadmic Year</label></th>
            <td><asp:TextBox ID="Year" runat="server" Enabled="false"/></td>
        </tr>
        <tr>
            <th><label>Semster</label></th>
            <td><asp:TextBox ID="Semester" runat="server" Enabled="false"/></td>
        </tr>

    </table>

    <br />

    <h3>Course Topic List</h3>
    <hr />

    <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.App_Course_Topic_Enroll" Width="100%" ForeColor="#333333" GridLines="None">
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

        <EmptyDataTemplate>
            No Data Available.
        </EmptyDataTemplate>

        <Columns>
            
            
            <asp:BoundField DataField="CourseTopicID" HeaderText="Course Topic Statement"  SortExpression="CourseTopicID" />
            
            <asp:BoundField DataField="TopicSeqNo" HeaderText="Sequence No." SortExpression="TopicSeqNo" />

        </Columns>
        </asp:GridView>
    <br />

    <asp:Button ID="btn" CssClass="btn btn-default" runat="server" Text="Close" PostBackUrl="~/AppPages/Course.aspx"/>

</asp:Content>
