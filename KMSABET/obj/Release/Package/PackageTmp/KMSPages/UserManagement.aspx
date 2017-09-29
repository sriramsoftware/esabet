<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="KMSABET.KMSPages.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>User Management</h3>
    <a href="UserAddEditDelete.aspx" class="btn btn-default">Add User</a>
    <hr />
    <asp:GridView ID="usersList" runat="server"
        AutoGenerateColumns="False" Width="100%"
        AllowPaging="true" OnPageIndexChanging="usersList_PageIndexChanging"
        ItemType="KMSABET.MyPocos.AppUser"
        OnRowCreated="usersList_RowCreated"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
                <asp:TemplateField HeaderText="Sr. No."> 
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>       
                    </ItemTemplate>
                    <ItemStyle Width="60px" />                                                          
                </asp:TemplateField>
            <asp:BoundField DataField="fullName" HeaderText="Full Name"/>
            <asp:BoundField DataField="username" HeaderText="Username"/>
            <asp:BoundField DataField="email" HeaderText="Email"/>
            <asp:BoundField DataField="userType" HeaderText="User Type"/>
            <asp:BoundField DataField="course" HeaderText="Course"/>
            <asp:BoundField DataField="module" HeaderText="Module"/>
        </Columns>
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
    </asp:GridView>
</asp:Content>
