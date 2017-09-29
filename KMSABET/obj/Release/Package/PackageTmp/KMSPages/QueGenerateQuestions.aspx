<%@ Page Title="Generate Question List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueGenerateQuestions.aspx.cs" Inherits="KMSABET.KMSPages.QueGenerateQuestions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>Generate Dummy Questions</h3>
    <asp:Label runat="server" Text="Following option will genearte the list of questions, list will be a combination of questions containing the attribute options for each attribute" ></asp:Label>
    <br/>
    <br/>
    <table>
        <tr>
            <th>
                <asp:Label runat="server" Font-Bold="true" Text="Number of Questions for each combination : " ></asp:Label>
            </th>
            <td>
                <asp:TextBox ID="numberQuestions" TextMode="Number" runat="server" Text="1" min="1" max="10" step="1" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button runat="server" Text="Generate Questions" CssClass="btn btn-default" OnClick="GenerateQuestions" />
            </td>
        </tr>
    </table>
    <br/>

</asp:Content>
