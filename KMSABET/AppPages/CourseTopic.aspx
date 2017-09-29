<%@ Page Title="Course Topic" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseTopic.aspx.cs" Inherits="KMSABET.AppPages.CourseTopic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap.css" />
    
    <h3>Course Topic Management</h3>
    <asp:Button ID="AddNew" Text="Add Course Topic" runat="server" CssClass="btn btn-default" OnClick="AddNew_Click"/>
    <hr />
    
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
            <td><label>CLO : </label></td>
            <td><asp:DropDownList ID="CLO" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CLO_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
    </table>

    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" ItemType="KMSABET.AppPages.APP_CourseTopic" Width="100%" ForeColor="#333333" GridLines="None" EmptyDataText="No records Found">
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
            
            <asp:BoundField DataField="TOPIC_STATEMENT" HeaderText="Topic Statement" ItemStyle-Width="90%"  SortExpression="TOPIC_STATEMENT" />
            
            <%--<asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <a href="CourseTopicView.aspx?ID=<%#:Item.TOPIC_ID %>&Update=false&Delete=false&View=true">View</a>
                </ItemTemplate>
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="CourseTopicView.aspx?ID=<%#:Item.TOPIC_ID %>&Update=true&Delete=false&View=false">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="CourseTopicView.aspx?ID=<%#:Item.TOPIC_ID %>&Update=false&Delete=true&View=false">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
    </asp:GridView>
    <br />

</asp:Content>
