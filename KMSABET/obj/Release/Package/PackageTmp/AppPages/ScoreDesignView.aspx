<%@ Page Title="Score Design" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ScoreDesignView.aspx.cs" Inherits="KMSABET.AppPages.ScoreDesignView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Score Desgin View</h2>
    <br />
    <asp:Button ID="btn" Text="Add Score Design" CssClass="btn btn-default" PostBackUrl="~/AppPages/ScoreDesignAdd.aspx" runat="server"/>
    <br />
    <br />
    <table style="width:100%;">
        
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

        <tr>
            <th><label>Assessment :</label></th>
            <td><asp:DropDownList ID="Assessments" runat="server"/></td>
        </tr>

    </table>

    <br />
    <h4>Score Desgin List</h4>
    <hr />

    <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="MainGrid_PageIndexChanging" ItemType="KMSABET.AppPages.ScoreDesignDataView"  Width="100%" ForeColor="#333333" GridLines="None">
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

            <asp:BoundField DataField="Assessment_Type" HeaderText="Assessment Type" InsertVisible="False" ReadOnly="True" SortExpression="Assessment_Type" />
            <asp:BoundField DataField="Assessment" HeaderText="Assessment" ItemStyle-Width="35%"  SortExpression="Assessment" />
            
            <asp:BoundField DataField="CLO_Statement" HeaderText="CLO Statement" SortExpression="CLO_Statement" />

            <asp:BoundField DataField="Raw_Score" HeaderText="Raw Score" SortExpression="Raw_Score" />
            <asp:BoundField DataField="Score_Value" HeaderText="Score Value" SortExpression="Score_Value" />
            <asp:BoundField DataField="Total_Score" HeaderText="Total Score" SortExpression="Total_Score" />
            
            

        </Columns>
        </asp:GridView>
    
    <asp:Button ID="close" CssClass="btn btn-default" Text="Close" runat="server" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx"/>

</asp:Content>
