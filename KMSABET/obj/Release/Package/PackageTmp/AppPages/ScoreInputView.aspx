<%@ Page Title="Score Input View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ScoreInputView.aspx.cs" Inherits="KMSABET.AppPages.ScoreInputView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Score Input View</h2>

    <br />

    <asp:Button CssClass="btn btn-default" runat="server" Text="Add Score Input" PostBackUrl="~/AppPages/Score_Input.aspx" /><br />
    <br />

    <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.Scoreinput" Width="100%" ForeColor="#333333" GridLines="None">
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
            
            <asp:BoundField DataField="Student_Name" HeaderText="Student Name" ReadOnly="True"  />
            <asp:BoundField DataField="Assessment_Name" HeaderText="Assessment Name" />
            
            <asp:BoundField DataField="Marks" HeaderText="Marks" />

        </Columns>
        </asp:GridView>

    <br />

    <br />

    <asp:Button ID="btn" runat="server" CssClass="btn btn-default" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx" Text="Close"/>

</asp:Content>
