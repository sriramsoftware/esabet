<%@ Page Title="Question Bank Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueBankMenuAdmin.aspx.cs" Inherits="KMSABET.KMSPages.QueBankMenuAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Admin's Menu</h3>
    <ul>
        <li><a href="QueQuestionAddEditView.aspx"> Add Question </a></li>
        <li><a href="QueQuestionAddUpload.aspx"> Bulk Add Questions via Upload </a></li>
        <li><a href="QueAttributeList.aspx"> Question Attributes </a></li>
    </ul>
</asp:Content>
