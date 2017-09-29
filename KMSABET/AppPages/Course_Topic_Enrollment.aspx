<%@ Page Title="Course Topic Enrollment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course_Topic_Enrollment.aspx.cs" Inherits="KMSABET.AppPages.Course_Topic_Enrollment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Topic Enrollment</h2>

    <br />


    <table>

        <tr>
            <th><label>Program : </label></th>
            <td><asp:DropDownList ID="program" runat="server" AutoPostBack="true" OnSelectedIndexChanged="program_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        <tr>
            <th><label>Course : </label></th>
            <td><asp:DropDownList ID="course" runat="server" AutoPostBack="true" OnSelectedIndexChanged="course_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        <tr>
            <th><label>Acadmic Year : </label></th>
            <td><asp:DropDownList ID="Acadmicyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Acadmicyear_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        <tr>
            <th><label>Semester : </label></th>
            <td><asp:DropDownList ID="semester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="semester_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

    </table>


    <hr />


    <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="MainGrid_PageIndexChanging" ItemType="KMSABET.AppPages.App_Course_Topic_Enroll" Width="100%" ForeColor="#333333" GridLines="None">
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
            
            <asp:BoundField DataField="CourseTopicID" HeaderText="Course Topic ID" ItemStyle-Width="55%"  SortExpression="CourseTopicID" />
            
            <asp:BoundField DataField="CourseEnrolID" HeaderText="Course Name" SortExpression="CourseEnrolID" />

            <asp:BoundField DataField="TopicSeqNo" HeaderText="Topic Sequence No" SortExpression="TopicSeqNo" />

            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <a href="CourseTopicEnrollView.aspx?ID=<%#:Item.CourseTopicEnrID %>&Update=false&Delete=false&View=true">View</a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="CourseTopicEnrollView.aspx?ID=<%#:Item.CourseTopicEnrID %>&Update=true&Delete=false&View=true">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="CourseTopicEnrollView.aspx?ID=<%#:Item.CourseTopicEnrID %>&Update=false&Delete=true&View=true">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
        </asp:GridView>


</asp:Content>
