<%@ Page Title="CLOSO" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClOSO.aspx.cs" Inherits="KMSABET.AppPages.ClOSO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div runat="server" id="div">


    </div>

    <hr />

    <label>Insert New CLOSO MAP</label>
    <br /><br />

    <label> Program : </label>
    <asp:DropDownList ID="programs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="programs_SelectedIndexChanged"></asp:DropDownList>

    <br />

    <label> Course : </label>
    <asp:DropDownList ID="Course" runat="server" AutoPostBack="true" OnSelectedIndexChanged="course_SelectedIndexChanged" ></asp:DropDownList>
    <br />
    <label> Year : </label>
    <asp:DropDownList ID="Year" AutoPostBack="true" runat="server" OnSelectedIndexChanged="Year_SelectedIndexChanged"></asp:DropDownList>
    <br />
    <label> Semster : </label>
    <asp:DropDownList ID="Sem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Sem_SelectedIndexChanged"></asp:DropDownList>
    <br />
    
    <br /><br />
    <label>CLOSO ID</label><br />
    <asp:TextBox ID="CLOSOID" runat="server" Enabled="false" Width="158px"></asp:TextBox>
    <br /><br />

    <label>CLO'S</label>
    
    <asp:DropDownList ID="CLO" runat="server" />
        
    <br />

    <label>SO</label>
    <asp:DropDownList ID="SO" runat="server" Width="300px"  />
    
    <br />

    <asp:Button ID="SAVE" runat="server" Text ="SAVE" OnClick="SAVE_Click"/>


</asp:Content>
