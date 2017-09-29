<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SO_Assessment.aspx.cs" Inherits="KMSABET.AppPages.SO_Assessment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <br /><br /><br />

    <asp:DropDownList ID="Program" runat="server" AutoPostBack="true" DataSourceID="SqlDataSource2" DataTextField="program_name" DataValueField="program_name" OnSelectedIndexChanged="Program_SelectedIndexChanged">
        <asp:ListItem>Select Program</asp:ListItem>
    </asp:DropDownList>
    
     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KMSABETDBConnectionString %>" SelectCommand="SELECT [program_name] FROM [App_Program]"></asp:SqlDataSource>
    
    <asp:DropDownList ID="Course" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Course_SelectedIndexChanged"><asp:ListItem>Select Course</asp:ListItem></asp:DropDownList>

    <br />
    <br /><br />
     <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
