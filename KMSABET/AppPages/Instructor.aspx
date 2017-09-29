
<%@ Page Title="Instrutor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Instructor.aspx.cs" Inherits="KMSABET.AppPages.Instructor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" href="../Content/bootstrap.css" />
    
    <br />

    <h2>Instructor</h2>

    <asp:Button ID="Insert_New_Record" runat="server" CssClass="btn btn-default" OnClick="Insert_New_Record_Click" Text="Add Instructor"/>
    <asp:Button runat="server" CssClass="btn btn-default" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx" Text="Close" />
    <br />
    <br />

    <asp:GridView ID="Instructorgrid" OnLoad="Instructorgrid_Load" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.App_Instructor"  Width="100%" ForeColor="#333333" GridLines="None">
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
            
            <asp:BoundField DataField="instructor_name" HeaderText="Instructor Name" SortExpression="instructor_name" />
            <asp:BoundField DataField="Full_Name" HeaderText="First Name" SortExpression="FIRST_NAME" />                        
            <asp:BoundField DataField="EMAIL" HeaderText="Email Address" SortExpression="EMAIL" />
            <asp:BoundField DataField="CELL_PHONE_NUM" HeaderText="Cell Phone No" SortExpression="CELL_PHONE_NUM" />            
            <asp:BoundField DataField="UNI_ID" HeaderText="University Name" SortExpression="UNI_ID" />

            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <a href="InstructorView.aspx?ID=<%#: Item.instructor_id %>&Update=false&Delete=false&Edit=true">View</a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="InstructorView.aspx?ID=<%#: Item.instructor_id %>&Update=true&Delete=false&Edit=true">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="InstructorView.aspx?ID=<%#: Item.instructor_id %>&Update=false&Delete=true&Edit=true">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        
    </asp:GridView>


</asp:Content>
