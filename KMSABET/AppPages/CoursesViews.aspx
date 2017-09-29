<%@ Page Title="Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoursesViews.aspx.cs" Inherits="KMSABET.AppPages.CoursesViews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Course Management</h3>

    <asp:button runat="server" id="Add" CssClass="btn btn-default" PostBackUrl="~/AppPages/CourseOption.aspx" Text="Add Course"/>
    
    <hr />
    <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.Courses" Width="100%" ForeColor="#333333" GridLines="None">
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
            
            
            <asp:BoundField DataField="PN" HeaderText="Program Name"  SortExpression="PN" />
            <asp:BoundField DataField="CN" HeaderText="Course Name"   SortExpression="CN" />
            <asp:BoundField DataField="CNU" HeaderText="Course Number"  SortExpression="CNU" />
            <asp:BoundField DataField="CT" HeaderText="Course Type"  SortExpression="CT" />
            
            
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="CourseOption.aspx?ID=<%#:Item.ID %>&Update=true&Delete=false">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="CourseOption.aspx?ID=<%#:Item.ID %>&Update=false&Delete=true">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>



</asp:Content>
