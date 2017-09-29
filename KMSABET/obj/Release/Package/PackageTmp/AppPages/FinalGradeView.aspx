<%@ Page Title="Score Distrubution" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalGradeView.aspx.cs" Inherits="KMSABET.AppPages.FinalGradeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Score Distrubution View</h2>
    
    <br />
    <br />

    <asp:Button ID="btnadd" runat="server" PostBackUrl="~/AppPages/FinalGrade.aspx" CssClass="btn btn-default" Text="Add Score Distrubution"/>
    <br />
    <table>
        
            <tr>
                <th><label>Program :</label></th>
                <td><asp:DropDownList ID="programs" AutoPostBack="true" runat="server" OnSelectedIndexChanged="programs_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr>
                <th><label>Course :</label></th>
                <td><asp:DropDownList ID="course" runat="server" AutoPostBack="True" OnSelectedIndexChanged="course_SelectedIndexChanged"/></td>

            </tr>
            
        <tr>
            <th><label>Acadmic Year :</label></th>
            <td><asp:DropDownList ID="Years" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Years_SelectedIndexChanged" /></td>
        </tr>
            
        <tr>
            <th><label>Semester :</label></th>
            <td><asp:DropDownList ID="Semester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Semester_SelectedIndexChanged"/></td>
        </tr>
        </table>    

    <br />
    <h3>Score Distribution List</h3>
    <hr />
    

        <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.Score_Distribution" Width="100%" ForeColor="#333333" GridLines="None">
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

        <EmptyDataTemplate>
            No Data Available.
        </EmptyDataTemplate>

        <Columns>
            
            <asp:BoundField DataField="Assessment_ID" HeaderText="Assessment Name"  SortExpression="Assessment_ID" />
            
            <asp:BoundField DataField="Score_Value" HeaderText="Score Value" SortExpression="Score_Value" />

        </Columns>
        </asp:GridView>

    <br />
    <asp:Button ID="Close" runat="server" Text="Close" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx" CssClass="btn btn-default"/>

</asp:Content>
