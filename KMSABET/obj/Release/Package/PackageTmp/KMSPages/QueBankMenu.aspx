<%@ Page Title="Question Bank Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueBankMenu.aspx.cs" Inherits="KMSABET.KMSPages.QueBankMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Instructor's Menu</h3>
    <ul>
        <li><a href="QueFavoriteListAddEdit.aspx"> Start New Assessment </a></li>
        <li><a href="QueQuestionList.aspx"> View All Questions </a></li>
        <li><a href="QueFavQuestionList.aspx"> View/Edit Assessments </a></li>
    </ul>
</asp:Content>
