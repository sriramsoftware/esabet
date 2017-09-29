<%@ Page Title="Program" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProgramView.aspx.cs" Inherits="KMSABET.AppPages.ProgramView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <h3>Program Management</h3>

    <asp:Button ID="New" CssClass="btn btn-default" Text="Add Program" runat="server" PostBackUrl="~/AppPages/Programs.aspx" />
    <hr />

     <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.Program" Width="100%" ForeColor="#333333" GridLines="None">
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
            
            <asp:BoundField DataField="Program_Name" HeaderText="Program Name" ItemStyle-Width="90%"  SortExpression="Program_Name" />
            
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="Programs.aspx?ID=<%#:Item.ID %>&Update=true&Delete=false">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="Programs.aspx?ID=<%#:Item.ID %>&Update=false&Delete=true">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>


</asp:Content>
