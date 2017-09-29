<%@ Page Title="Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="KMSABET.AppPages.Students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap.css" />
    <br />

    <h2>Students</h2>
    <br />
    <br />

    <asp:Button ID="NewRecord" Text="Add New Student" runat="server" CssClass="btn btn-default" OnClick="NewRecord_Click" />
    <asp:Button runat="server" CssClass="btn btn-default" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx" Text="Close" />
    <br />

    <h4>Fillter</h4>
    
    <table>
        <tr>
            <td><label>Program : </label></td>
            <td><asp:DropDownList ID="Program" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Program_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label>Course : </label></td>
            <td><asp:DropDownList ID="Course" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Course_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label>Acadmic Year : </label></td>
            <td><asp:DropDownList ID="AcadmicYears" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AcadmicYears_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label>Semester : </label></td>
            <td><asp:DropDownList ID="ViewSemester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ViewSemester_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
    </table>
    
    <br /><br />
    
    <h3>Student List</h3>

    <asp:GridView ID="Student" runat="server" OnLoad="Student_Load" AutoGenerateColumns="false" ItemType="KMSABET.AppPages.App_Student" Width="100%" ForeColor="#333333" GridLines="None">
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
            <asp:BoundField DataField="StudentRoll" HeaderText="Student Roll Number" SortExpression="StudentRoll" />
                        <asp:BoundField DataField="StudentName" HeaderText="Student Name" SortExpression="StudentName" />
                        <asp:BoundField DataField="CourseID" HeaderText="Course ID" SortExpression="CourseID" />                        
            
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <a href="StudentView.aspx?ID=<%#: Item.StudentID %>&Delete=false&Update=false&Edit=true"> View </a>
                            </ItemTemplate>
                        </asp:TemplateField>
            
                        <asp:TemplateField HeaderText="Edits">
                            <ItemTemplate>
                                <a href="StudentView.aspx?ID=<%#: Item.StudentID %>&Delete=false&Update=true&Edit=false"> Edit </a>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <a href="StudentView.aspx?ID=<%#: Item.StudentID %>&Delete=true&Update=false&Edit=false"> Delete </a>
                            </ItemTemplate>
                        </asp:TemplateField>

        </Columns>

    </asp:GridView>


</asp:Content>
