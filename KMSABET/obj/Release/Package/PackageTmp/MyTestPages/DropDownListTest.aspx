﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DropDownListTest.aspx.cs" Inherits="KMSABET.MyTestPages.DropDownListTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="DropDownList1" AutoPostBack="True"
                OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" runat="server" />
    <asp:DropDownList ID="DropDownList2" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" runat="server" />
</asp:Content>
