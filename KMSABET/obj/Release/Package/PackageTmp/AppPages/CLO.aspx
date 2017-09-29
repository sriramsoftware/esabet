<%@ Page Title="CLO" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CLO.aspx.cs" Inherits="KMSABET.AppPages.CLO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>CLO Management</h3>

    <asp:Button ID="addbtn" CssClass="btn btn-default" Text="Add CLO" runat="server" PostBackUrl="~/AppPages/addCLO.aspx"/>

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
    </table>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" ItemType="KMSABET.AppPages.ICLO" Width="100%" ForeColor="#333333" GridLines="None">
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
            
            <asp:BoundField DataField="Course" HeaderText="Course" ItemStyle-Width="25%"  SortExpression="Course" />
            <asp:BoundField DataField="Clo_statement" HeaderText="CLO Statement" ItemStyle-Width="65%"  SortExpression="Clo_statement" />
            
            <%--<asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <a href="addCLO.aspx?ID=<%#:Item.ID %>&Update=false&Delete=false&View=true">View</a>
                </ItemTemplate>
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="addCLO.aspx?ID=<%#:Item.ID %>&Update=true&Delete=false&View=false">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="addCLO.aspx?ID=<%#:Item.ID %>&Update=false&Delete=true&View=false">Delete</a>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
    </asp:GridView>


</asp:Content>
