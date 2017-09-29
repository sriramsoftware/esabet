<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="insmedview.aspx.cs" Inherits="KMSABET.AppPages.insmedview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Instructor Method View</h2>

    <br />
    <br />

    <asp:Button ID="btnadd" runat="server" Text="Add Instructor Method" CssClass="btn btn-default" PostBackUrl="~/AppPages/InstructionMethod.aspx"/>

    <br />
    <br />
    <table>
        <tr>
            <td>Program : </td>
            <td>
                <asp:DropDownList ID="program" runat="server" OnSelectedIndexChanged="program_SelectedIndexChanged" AutoPostBack="true" />
            </td>

        </tr>
        <tr>
            <td>Course : </td>
            <td>
                <asp:DropDownList ID="Course" runat="server" OnSelectedIndexChanged="Course_SelectedIndexChanged" AutoPostBack="true" />
            </td>

        </tr>
        <tr>
            <td>Acadmic Year : </td>
            <td>
                <asp:DropDownList ID="Year" runat="server" OnSelectedIndexChanged="Year_SelectedIndexChanged" AutoPostBack="true" />
            </td>

        </tr>

        <tr>
            <td>Semester : </td>
            <td>
                <asp:DropDownList ID="Semsester" runat="server" OnSelectedIndexChanged="Semsester_SelectedIndexChanged" AutoPostBack="true" />
            </td>

        </tr>
        <tr>
            <td>CLO : </td>
            <td>
                <asp:DropDownList ID="Clo" runat="server" OnSelectedIndexChanged="Clo_SelectedIndexChanged" AutoPostBack="true" />
            </td>

        </tr>

    </table>

    <br />
    <br />


    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" Width="100%" ItemType="KMSABET.AppPages.insMethod" ForeColor="#333333" GridLines="None">
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

            <asp:BoundField DataField="CID" HeaderText="Course Enroll ID" />
            <asp:BoundField DataField="IID" HeaderText="Instruction Type" />
            <asp:BoundField DataField="Clo" HeaderText="CLO Statement" />

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <a href="InstructionMethod.aspx?ID=<%#: Item.CID %>&Delete=false&Update=true&U=<%#: Item.ID %>">Edit </a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <a href="InstructionMethod.aspx?ID=<%#: Item.ID %>&Delete=true&Update=false">Delete </a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
