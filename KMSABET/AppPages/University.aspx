<%@ Page Title="University" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="University.aspx.cs" Inherits="KMSABET.AppPages.University" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>University</h2>

    <br /><br />

    <asp:Button ID="btn" runat="server" Text="Add University" CssClass="btn btn-default" PostBackUrl="~/AppPages/ViewUniversity.aspx"/>
    <asp:Button ID="Close" runat="server" Text="Close" CssClass="btn btn-default" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx"/>

    <br />

    <h3>University List</h3>

    <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.Universities" Width="100%" ForeColor="#333333" GridLines="None">
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
            
           
            <asp:BoundField DataField="UN" HeaderText="University Name" SortExpression="UN" />
            <asp:BoundField DataField="UA" HeaderText="University Address" SortExpression="UA" />
            <asp:BoundField DataField="V" HeaderText="University Verify" SortExpression="V" />
            

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="ViewUniversity.aspx?ID=<%#:Item.ID %>&Update=true&Delete=false">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="ViewUniversity.aspx?ID=<%#:Item.ID %>&Update=false&Delete=true">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>


</asp:Content>
